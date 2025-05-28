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
        public MemberRepository() // 레파지토리 객체 생성 될때 싱글턴 객체 얻음
        {
            // MysqlDatabaseManager 싱글톤 인스턴스 얻기 (DB 연결 관리 담당)
            dbManager = MysqlDatabaseManager.GetInstance();
        }

        // DB 연결 객체를 담을 변수 (초기에는 null)
        MySqlConnection conn = null;
        // ㄴ MySqlConnection : MySQL 데이터베이스와 연결하는 C# 클래스
        // ㄴ MySQL 데이터베이스에 연결(connection)을 열고, 명령어를 보내고, 결과를 받는 데 쓰이는 객체
        // ㄴ MySql.Data.MySqlClient 네임스페이스에 있는 클래스이고,
        // ㄴ MySql.Data(NuGet 패키지로 설치,MySQL 공식 .NET 커넥터) 라이브러리를 설치해야 사용할 수 있다.

        /// <summary>
        /// 회원(가입)정보를 DB에 삽입, 매핑되어 있는 MEMBER테이블에 회원정보를 insert 
        /// </summary>
        /// <param name="member"></param>
        public int InsertMember(Member member) { 
        
        
        }



    }
}
