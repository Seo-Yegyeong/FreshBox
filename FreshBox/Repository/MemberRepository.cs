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
        /// <returns></returns>
        public int ReadUsername(string username) {

            using (MySqlConnection conn = dbManager.GetConnection()) { // 연결 열기(주의 : 사용 후 닫아야 함) 
                // DB에 보낼 SQL 쿼리문
                string query = "SELECT EXISTS (SELECT 1 FROM member WHERE username = @username)";
                // @ : 파라미터 자리 표시자(@username에 해당하는 값을 나중에 따로 파라미터로 지정해서 넣을 수 있음)
                // 사용자 입력값을 안전하게 넣기 위한 변수 자리
                // conn DB연결, cmd SQL실행(명령 전달자)
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {   // new MySqlCommand(query, conn)
                    // 이 객체를 통해 SQL 쿼리를 DB에 실행
                    // MySqlCommand : SQL 쿼리를 날리는 역할을 하는 객체
                    // SQL 명령문 실행기
                    // 이 SQL 문장을 conn이라는 데이터베이스 연결로 실행해줘

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

                }

            }
                return 0;
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
