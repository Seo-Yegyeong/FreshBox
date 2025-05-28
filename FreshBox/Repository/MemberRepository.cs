using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public int ReadUsername(string username)
        {
            // DB 연결 객체를 담을 변수 (초기에는 null)
            MySqlConnection conn = null;
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
                Console.WriteLine("MemberRepository - ReadUsername() 메서드 처리 중 오류 발생" + ex.Message);
                result = -1; // 예외 발생 시 에러 신호 
                // TODO : 서비스에서 -1 리턴 받아서 return "서버 오류가 발생했습니다. 잠시 후 다시 시도해주세요.";
                // 처리해서 사용자 UI에 보여주기
            }
            finally { // 예외가 발생하든 안하든 무조건 실행됨
                if (conn != null) { // DB연결 객체(conn)이 null이 아니면 실행함
                    dbManager.CloseConnection(conn); // MySQL 데이터베이스와의 연결(Connection)을 닫는 메서드
                    // 2중 null 체크(호출부에서 null 검사 + 메서드 내부에서 null 검사)
                }
            }

            return result;

        }



        //phone 중복 체크

        //email 중복 체크


        /// <summary>
        /// 회원(가입)정보를 DB에 삽입, 매핑되어 있는 MEMBER테이블에 회원정보를 insert 
        /// </summary>
        /// <param name="member"></param>
        //public int InsertMember(Member member) { 
        // DB 연결 객체를 담을 변수 (초기에는 null)
        MySqlConnection conn = null;
        // ㄴ MySqlConnection : MySQL 데이터베이스와 연결하는 C# 클래스
        // ㄴ MySQL 데이터베이스에 연결(connection)을 열고, 명령어를 보내고, 결과를 받는 데 쓰이는 객체
        // ㄴ MySql.Data.MySqlClient 네임스페이스에 있는 클래스이고,
        // ㄴ MySql.Data(NuGet 패키지로 설치,MySQL 공식 .NET 커넥터) 라이브러리를 설치해야 사용할 수 있다.

        //}



    }
}
