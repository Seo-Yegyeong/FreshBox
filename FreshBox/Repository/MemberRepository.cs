using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FreshBox.Database; // MysqlDatabaseManager 싱글톤 인스턴스를 사용하기 위해 필요
using FreshBox.Models;
using MySql.Data.MySqlClient; // MySqlConnection, MySqlCommand 등 MySQL 데이터베이스 작업 관련 클래스 사용 위해 필요

namespace FreshBox.Repository
{
    public class MemberRepository // 외부 접근 가능하도록 public으로 변경
    {
        // MysqlDatabaseManager 싱글톤 인스턴스 저장용 변수
        private readonly MysqlDatabaseManager dbManager;
        // private readonly로 선언한 이유
        // private: 외부에서 직접 접근 못하게 하려고.
        // readonly: 생성자에서 한 번 초기화 후 변경하지 않으려고(불변 보장). 싱글톤으로 관리하는 객체라서
        // 즉, 이 클래스 내부에서만 쓰고, 한 번만 초기화해서 안전하게 관리하려는 의도
        public MemberRepository() // 레파지토리 객체 생성 될때 싱글턴 객체 얻음
        {
            // MysqlDatabaseManager 싱글톤 인스턴스 얻기 (DB 연결 관리 담당)
            dbManager = MysqlDatabaseManager.GetInstance();
        }

        /// <summary>
        /// 사용자가 입력한 사용자 ID(username)이 DB에 있는지 확인
        /// 있으면 중복된 username이라서 다른거 쓰라고 처리 해야 함
        /// </summary>
        /// <param name="username">사용자가 입력한 ID</param>
        /// <returns>중복이면 1, 없으면 0, 에러 -1</returns>
        public int FindByUsername(string username) // TODO : 추후 비동기로 바꾸는 것이 좋다
        {
            // DB 연결 객체를 담을 변수 (초기에는 null)
            MySqlConnection? conn = null;
            // ㄴ MySqlConnection : MySQL 데이터베이스와 연결하는 C# 클래스
            // ㄴ MySQL 데이터베이스에 연결(connection)을 열고, 명령어를 보내고, 결과를 받는 데 쓰이는 객체
            // ㄴ MySql.Data.MySqlClient 네임스페이스에 있는 클래스이고,
            // ㄴ MySql.Data(NuGet 패키지로 설치,MySQL 공식 .NET 커넥터) 라이브러리를 설치해야 사용할 수 있다.

            // DB에 보낼 SQL 쿼리문
            string query = "SELECT EXISTS (SELECT 1 FROM member WHERE username = @username)";
            // @ : 파라미터 자리 표시자(@username에 해당하는 값을 나중에 따로 파라미터로 지정해서 넣을 수 있음)
            // 사용자 입력값을 안전하게 넣기 위한 변수 자리
            // conn DB연결, cmd SQL실행(명령 전달자)
            //SELECT EXISTS(...)는 쿼리 결과가 존재하는지 여부를 반환하는 SQL 함수
            //(...)안에 조건을 넣어서, 해당 조건을 만족하는 데이터가 있으면 1(true), 없으면 0(false)를 반환함
            //select username from member where username = @username; 보다 DB성능에 좋음
            // 이유 : SELECT EXISTS는 조건을 만족하는 데이터가 있는지만 확인하기 때문에,
            // 실제 데이터를 가져오지 않아서 더 빠르고 효율적임
            // 즉, 해당 username이 member 테이블에 존재하는지 여부를 확인하는 쿼리문임

            int result = -1; // 리턴값을 저장할 변수

            try
            {
                
                conn = dbManager.GetConnection(); // 연결 열기(주의 : 사용 후 닫아야 함) 
                                                  // new MySqlCommand(query, conn) : 쿼리 실행 객체 생성

                MySqlCommand cmd = new MySqlCommand(query, conn);
                // 이 객체를 통해 SQL 쿼리를 DB에 실행
                // MySqlCommand : SQL 쿼리를 날리는 역할을 하는 객체
                // SQL 명령문 실행기
                // 이 SQL 문장을 conn이라는 데이터베이스 연결로 실행해줘

                // SQL 인젝션 방지용 파라미터 추가
                cmd.Parameters.AddWithValue("@username", username);
                #region “@username”이라는 빈 공간에 username이라는 값을 넣겠다는 뜻.
                // cmd는 MySqlCommand 객체
                // 객체(인스턴스)는 클래스를 메모리 공간에 할당 시킨 것
                // MySqlCommand 클래스 안에 존재하는 필드나, 메서드를
                // 객체. 으로 접근해서 사용할 수가 있는데
                // MySqlCommand 클래스 안에 Parameters라는 **컬렉션(리스트 같은 것)**이 있음
                // 여기에 AddWithValue 메서드를 호출해서 파라미터를 하나씩 추가하는 것.
                // cmd는 MySqlCommand 객체
                // cmd.Parameters는 파라미터 목록
                // .AddWithValue()는 그 목록에 새 파라미터를 추가하는 메서드
                // 이걸 통해 SQL 쿼리문에서 @username에 실제 값을 안전하게 연결해줄 수 있음
                // 요약하면, AddWithValue는 MySqlCommand의 Parameters 컬렉션에 값을 넣는 메서드
                //  “@username”이라는 빈 공간에 username이라는 값을 넣겠다는 뜻.
                // SQL 쿼리에 직접 넣지 않고, 이렇게 하면 해킹 위험(SQL 인젝션)을 줄여줌

                // Parameters는 SQL 명령어(커맨드) 안에 변수를 안전하게 넣는 그릇 같은 것
                // 예를 들어, SQL 쿼리에 직접 글자를 넣으면 위험하니까, 변수 자리를 만들어서 거기에 값 넣는다
                // .AddWithValue("키", 값)
                #endregion

                // 쿼리 실행 후 결과 받아오기
                // ExecuteScalar() : 쿼리 실행 후 첫 번째 행의 첫 번째 열의 값을 반환
                // 이 경우, EXISTS 쿼리라서 true(1) 또는 false(0)를 반환함
                object queryResult = cmd.ExecuteScalar(); // 중복이면 1, 없으면 0
                                                          // 결과를 int로 변환 (EXISTS 결과는 1 또는 0)
                result = Convert.ToInt32(queryResult);
                // Convert.ToInt32() 문자열, 객체 등 → int로 변환할 때 사용
                // 다양한 타입(object, string, bool 등)의 값을
                // 32비트(4바이트) 정수형(int)으로 변환하는 정적(static)메서드
                // 이 메서드는 .NET의 System.Convert 클래스에 정의되어 있음.
                // null도 안전하게 처리(0으로), bool타입인 경우 true는 1, false는 0
                // 못 바꿀 경우(예: "abc" 같은 숫자 아님)에는 FormatException 발생

                #region [참고] Convert 클래스 주요 메서드
                // Convert.ToBoolean("true"); // 문자열을 논리값으로 변환
                // ToDouble() // 문자열 등 → double (소수점 있는 숫자)로
                // ToDecimal() // double보다 정밀한 실수형으로 변환할 때
                // ToInt64() // long형 정수로 변환할 때
                // ToDateTime() // 문자열 등을 DateTime으로 바꿀 때
                // ToChar() // 문자열 등에서 문자 한 개만 추출할 때
                #endregion

            }
            catch(Exception ex) { // try에서 예외 발생시 넘어옴, 예외 발생 없으면 실행 되지 않음
                // 예외 타입을 명시하지 않으면 모든 예외를 다 잡는다는 의미
                // 예외 타입 상관없이 여기로 다 들어옴
                // 어떤 예외가 발생해도 무조건 처리 가능 // 단점 : 어떤 예외인지 알 수 없어 디버깅 어려움
                Debug.WriteLine($"{ex.Message} MemberRepository - ReadUsername() 메서드 처리 중 오류 발생");
                result = -1; // 예외 발생 시 에러 신호 
                
            }
            finally { // 예외가 발생하든 안하든 무조건 실행됨
                if (conn != null) { // DB연결 객체(conn)이 null이 아니면 실행함
                    dbManager.CloseConnection(conn); // MySQL 데이터베이스와의 연결(Connection)을 닫는 메서드
                    // 2중 null 체크(호출부에서 null 검사 + 메서드 내부에서 null 검사)
                }
            }

            return result;

        }


        /// <summary>
        /// 사용자가 입력한 사용자 ID(username)가 DB에 존재하는지 비동기 방식으로 확인한다.
        /// 중복된 username이면 1, 없으면 0, 에러 발생 시 -1 반환.
        /// </summary>
        /// <param name="username">사용자가 입력한 ID</param>
        /// <returns>
        /// 1: 중복된 username 존재  
        /// 0: username 없음 (사용 가능)  
        /// -1: 처리 중 오류 발생  
        /// </returns>
        public async Task<int> FindByUsernameAsync(string username)
        {
            MySqlConnection? conn = null;

            // 사용자 이름 존재 여부를 확인하는 쿼리
            string query = "SELECT EXISTS (SELECT 1 FROM member WHERE username = @username)";
            int result = -1;// 기본 결과값: 에러 시 -1 반환

            try
            {
                // 비동기 방식으로 DB 연결을 열고 연결 객체 받기
                conn = await dbManager.GetConnectionAsync();

                // MySqlCommand 객체 생성, 쿼리와 연결 객체 할당
                using (var cmd = new MySqlCommand(query, conn))
                {
                    // SQL 파라미터에 사용자 입력값 할당 (SQL 인젝션 방지)
                    cmd.Parameters.AddWithValue("@username", username);

                    // 비동기 쿼리 실행, 단일 결과값 반환 (중복 여부)
                    object queryResult = await cmd.ExecuteScalarAsync();

                    // 결과값을 int로 변환 (0 또는 1)
                    result = Convert.ToInt32(queryResult);
                }
            }
            catch (Exception ex)
            {
                // 예외 발생 시 디버그 출력, 결과는 -1로 유지 (에러 상태)
                Debug.WriteLine($"{ex.Message} MemberRepository - ReadUsernameAsync() 처리 중 오류 발생");
                result = -1; // 오류 표시
            }
            finally
            {
                // DB 연결이 열려 있다면 반드시 비동기 방식으로 닫기
                if (conn != null)
                {
                    await dbManager.CloseConnectionAsync(conn);
                }
            }

            return result;// 중복 여부 또는 에러 코드 반환
        }



        /// <summary>
        /// phone 중복 체크
        /// member 테이블에서 전달된 전화번호가 존재하는지 확인합니다.
        /// 해당 번호가 존재하면 1, 존재하지 않으면 0을 반환합니다.
        /// </summary>
        /// <param name="phoneNum">유효성 검사를 마친 사용자 입력 휴대폰 번호</param>
        /// <returns>중복 여부를 나타내는 정수 (1: 존재함, 0: 존재하지 않음)</returns>
        public int FindByPhoneNum(string phoneNum) {
            int result = -1; // 리턴값을 저장할 변수

            //DB 연결 객체를 담을 변수(초기에는 null)
            MySqlConnection? conn = null;

            // DB로 보내서 실행 시킬 SQL문
            string query = "SELECT EXISTS (SELECT 1 FROM member WHERE phone = @phone)";

            try
            {
                conn = dbManager.GetConnection(); // 연결 열기
                MySqlCommand cmd = new MySqlCommand(query, conn);
                // ㄴ 커맨드 객체 생성(SQL 쿼리를 DB에 날리는 역할)

                cmd.Parameters.AddWithValue("@phone", phoneNum);
                // ㄴ @phone 파라미터 자리에 phoneNum값을 넣겠다는 뜻

                object queryResult = cmd.ExecuteScalar();
                // ㄴ 쿼리를 실행한 결과를 리턴 받아 저장
                // EXISTS 쿼리라서 true(1) 또는 false(0)를 반환함
                // 중복이면(행이 있으면) 1 , 중복 없으면 0 

                result = Convert.ToInt32(queryResult);
                // ㄴ 아규먼트로 넣은 값을 32비트(4바이트)의 int로 변환
                Debug.WriteLine($"[MemberRepository.FindByPhoneNum()] result = {result}");

            }
            catch (Exception ex)
            {
                // try 실행하다 예외 발생 시 catch문으로 넘어옴(정상 실행 시 catch문 실행 되지 않음)
                Debug.WriteLine($"{ex.Message}MemberRepository의 FindByPhoneNum()에서 예외 발생");
                result = -1;
            }
            finally { // 예외 발생 여부와 상관없이 무조건 실행됨

                if (conn != null) { // 연결 객체가 null일 땐 닫을 필요없으니까,
                    dbManager.CloseConnection(conn);
                }
            }
            return result;
        }

        /// <summary>
        /// email 중복 체크
        /// </summary>
        /// <param name="email">검사할 email 문자열</param>
        /// <returns>-1(예외), 1(중복), 0(중복되지 않음)</returns>
        public int FindByEmail(string email) {
            int result = -1; // 결과값 담을 변수

            //연결 객체 생성
            MySqlConnection? conn = null;// 이 변수는 null이 될 수 있다고 명시함

            // DB로 보낼 실행 시킬 쿼리문 작성
            string query = "SELECT EXISTS (SELECT 1 FROM member WHERE email = @email)";

            try {           
                conn = dbManager.GetConnection(); // 연결 열기

                //DB 명령문 실행시켜주는 커맨드 객체생성
                MySqlCommand cmd = new MySqlCommand(query, conn);

                // SQL 인젝션 방지용 파라미터 추가
                cmd.Parameters.AddWithValue("@email",email);
                // 쿼리문의 @email 자리에 email을 넣겠다는 뜻
                // AddWithValue()는 값에 따라 데이터 타입을 자동 추론
                // 자동 타입 추론이 틀릴때도 있으니 주의해서 사용하기!
                // 안전하게 쓰려면 Add() + 명시적 타입 지정 방식으로 사용
                // cmd.Parameters.Add(parameterName, MySqlDbType).Value = 실제값;
                // 예시: 가입일(join_date)는 날짜(DATETIME) 타입
                // cmd.Parameters.Add("@join_date", MySqlDbType.DateTime).Value = joinDate;
                // 여러 개 파라미터는? 그냥 파라미터별로 Add() 호출을 여러 번 하면 된다(한 줄씩 추가)

                // 쿼리 실행
                object queryResult = cmd.ExecuteScalar();

                // 결과값 변환
                result = Convert.ToInt32(queryResult);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[예외] [MemberRepository.FindByEmail] {ex.GetType().Name}: {ex.Message}");
                result = -1;
            }
            finally { // 예외 여부에 상관없이 항상 실행되는 코드
                // 열었던 연결 객체를 닫아줌 + 리소스 정리
                if (conn != null) { // null이면 닫아줄 필요 자체가 없기 때문에 
                    dbManager.CloseConnection(conn);
                }
            }

            return result;
        }

        /// <summary>
        /// 회원(가입)정보를 DB에 삽입, 매핑되어 있는 member테이블에 회원정보를 insert 
        /// </summary>
        /// <param name="member">회원정보를 가지고 있는 member객체(비밀번호는 암호화 처리됨)</param>
        /// <returns>성공 시 1 , 실패 시 0, 예외 발생 시 -1을 리턴</returns>
        public int InsertMember(Member member)
        {
            int result = -1; // 리턴값을 저장할 변수

            //DB 연결 객체를 담을 변수(초기에는 null)
            MySqlConnection conn = null;
            // ㄴ MySqlConnection : MySQL 데이터베이스와 연결하는 C# 클래스
            // ㄴ MySQL 데이터베이스에 연결(connection)을 열고, 명령어를 보내고, 결과를 받는 데 쓰이는 객체
            // ㄴ MySql.Data.MySqlClient 네임스페이스에 있는 클래스이고,
            // ㄴ MySql.Data(NuGet 패키지로 설치, MySQL 공식.NET 커넥터) 라이브러리를 설치해야 사용할 수 있다.

            // DB로 보내서 실행 시킬 sql문
            string query = @"INSERT INTO member (username, password, member_name, role, 
                                    phone, email, birth_date, hire_date)
                                VALUES (@username, @password, @memberName, @role, 
                                    @phone, @email, @birthDate, @hireDate)";

            try {
                conn = dbManager.GetConnection(); // 연결 열기
                MySqlCommand cmd = new MySqlCommand(query, conn);
                // MySql DB에 쿼리 날려주는 커맨드 객체 생성

                // 파라미터 추가
                cmd.Parameters.Add("@username", MySqlDbType.VarChar).Value = member.Username;
                // ㄴ AddWithValue()는 값에 따라 데이터 타입을 자동 추론 방식이여서 틀릴 수도 있음
                // ㄴ add()메서드로 MySqlDbType을 명시적으로 지정
                // ㄴ @username 파라미터 타입은 DB에서 VarChar타입이라고 알려주는 것.
                // .Value = member.Username 객체에서 가져온 값(member.Username)을 이 파라미터에 대입

                cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = member.Password;
                cmd.Parameters.Add("@memberName", MySqlDbType.VarChar).Value = member.MemberName;
                cmd.Parameters.Add("@role", MySqlDbType.Enum).Value = member.Role.ToString().ToLower();
                // ㄴ MySQL의 ENUM은 문자열 기반, C#의 Enum은 내부적으로 정수값
                // ㄴ 그래서 문자열로 변환해서 넘겨야 에러가 안 생긴다!
                // ㄴ C#의 Enum을 .ToString().ToLower()로 문자열+소문자 변환해서 DB에 저장
                cmd.Parameters.Add("@phone", MySqlDbType.VarChar).Value = member.Phone;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = member.Email;
                cmd.Parameters.Add("@birthDate", MySqlDbType.Date).Value = member.BirthDate;
                cmd.Parameters.Add("@hireDate", MySqlDbType.Date).Value
                    = member.HireDate.HasValue ? member.HireDate.Value : DBNull.Value;
                // ㄴ C#의 null은 그냥 없는 것 이지만
                // ㄴ DB로 넘길 땐 명시적으로 DB용 null 객체로 줘야 MySQL이 이해한다고 함
                // 그래서 삼항 연산자를 사용해서 HireDate에 값이 들어 있으면 member.HireDate.Value
                // 없으면 DBNull.Value을 저장해서 DB로 보냄

                //쿼리 실행
                int queryResult = cmd.ExecuteNonQuery();

                result = queryResult;

                return result;

            }
            catch (Exception ex) {
                result = -1;
                Debug.WriteLine($"[예외][MemberRepository.InsertMember]{ex.Message}");
            }
            finally {
                if (conn != null) {
                    dbManager.CloseConnection(conn); // 연결 닫기 + 리소스 정리
                }
            }

            return result;
        }



    }
}
