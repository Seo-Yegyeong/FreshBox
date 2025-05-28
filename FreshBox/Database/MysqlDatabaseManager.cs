using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshBox.Database
{

    //데이터베이스 연결을 담당하는 초보자용 도우미 클래스 정의(연결 열기/ 닫기/ 동기/ 비동기 지원/ 싱글톤으로 관리(멀티스레드 고려))
    /* 
        싱글톤으로 관리하는 이유?
           1. DB연결 관리 일관성 유지, 2.리소스 절약, 3. 전역 접근성 보장, 4. 동기화 및 관리 용이
            => DB 연결 관리를 ‘한 군데’에서 안전하고 효율적으로 하려고 싱글톤 패턴을 사용함
     */

    /// <summary>
    /// MySQL DB 연결과 관리를 싱글턴 패턴으로 처리하는 클래스(멀티스레드 고려함)
    /// 초보자용 코드
    /// </summary>
    public sealed class MysqlDatabaseManager //sealed 키워드로 상속 막음 -> 이 클래스를 상속하려 하면 컴파일오류
    {
        /// <summary>
        /// MySQL 서버에 접속하기 위한 정보(연결 문자열)
        /// 팀플용 계정 정보로 입력하면 됩니다.(Uid : spc , Pwd : spc1234!)
        /// - Server: DB 서버 주소 (localhost는 내 컴퓨터에서 돌고 있는 MySQL 서버)
        /// - Database: 사용할 데이터베이스 이름
        /// - Uid: 사용자 계정 (MySQL에서 생성한 사용자 ID)
        /// - Pwd: 위 계정의 비밀번호
        /// - Port: MySQL 기본 포트 3306
        /// - Charset: 문자 인코딩 (UTF-8로 설정)
        /// - Pooling=true :  MySQL 연결 문자열(Connection String) 옵션 중 하나
        ///    ㄴ 커넥션 풀링(Connection Pooling)을 사용하겠다는 뜻
        ///    ㄴ DB 연결을 매번 새로 만드는 대신, 이미 열려 있는 연결을 재사용해서 성능을 크게 개선하는 기법
        ///    ㄴ 주의 점 : 연결을 꼭 닫아야 풀에 제대로 반환됨, 커넥션을 쓴 후 CloseConnection() 또는 conn.Close() 꼭 호출해야 함 → 풀에 반환
        ///    ㄴ 요약 : Pooling=true;는 DB 연결을 미리 만들어두고 재사용해서 성능을 높여주는 옵션
        /// 실제 배포 시에는 보안을 위해 이 값은 appsettings나 환경변수 등 외부에서 관리하는 게 좋음!
        /// </summary>
        private readonly string connStr = "Server=localhost; Database=FreshBox; Uid=sqc; Pwd=spc1234!; Port=3306; Charset=utf8; Pooling=true;";
        // ㄴ DB 연결 정보 입력

        // 싱글턴 인스턴스(객체)를 저장할 static 변수 (처음에는 null)
        private static MysqlDatabaseManager instance = null;

        // 멀티스레드에서 동시에 접근하지 못하도록 잠금 객체 생성
        private static readonly object lockObject = new object();
        //동시성 제어용 락 (여러 스레드가 동시에 들어오는 걸 막음)

        // 생성자 private로 외부에서 new로 객체 생성 못하도록 막음
        private MysqlDatabaseManager() { }

        /// <summary>
        /// 싱글턴 인스턴스 반환
        /// 외부에서 이 메서드를 통해 싱글턴 인스턴스를 얻음
        /// Double-Check Locking 기법 사용(2번 체크함)
        /// </summary>
        public static MysqlDatabaseManager GetInstance()
        {
            // 인스턴스가 아직 생성되지 않았다면
            if (instance == null) // 1차 체크 (빠른 return 목적)
            {
                // 잠금 시작: 한 번에 하나의 스레드만 이 블록에 진입 가능
                lock (lockObject)
                {
                    // 다시 한 번 확인 (다른 스레드가 먼저 만들었을 수 있음)
                    if (instance == null) // 2차 체크
                    {
                        instance = new MysqlDatabaseManager(); // 객체 생성
                    }
                }
            }

            return instance; // 딱 1번만 생성되면 객체생성 하지 않음
        }

        /// <summary>
        /// DB 연결 객체 반환 (사용 후 반드시 conn.Close()로 닫아야 함)
        /// </summary>
        public MySqlConnection GetConnection()
        {   // ㄴ MySqlConnection은 C#에서 MySQL 데이터베이스 서버랑 '연결'을 해주는 객체(클래스)
            // ㄴ 내 프로그램과 MySQL 데이터베이스 사이에 통로를 만드는 역할
            try
            {
                var conn = new MySqlConnection(connStr); //연결 객체 생성, 아규먼트로 DB연결정보(연결문자열) 넣어서 생성자 호출
                conn.Open(); // 연결 시도, 이걸 해야 실제로 DB와 연결이 시작된다.
                return conn; // 연결 객체를 호출한 곳으로 반환
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"[DB 연결 실패] {ex.Message}");
                throw; // 나중에 필요하면 예외 래핑해서 재던지기(#region 참고)
            }
        } //-> 이 메서드 호출해서 사용한 뒤에 CloseConnection(conn)호출해서 DB연결 닫아주어야 함
          // 연결을 열고 닫아주어야 하는 이유?
        /*
         DB 서버는 제한된 연결 수만 허용
         연결을 계속 열어놓으면 서버가 과부하 걸려서 느려지고, 나중엔 더 이상 연결 못 할 수도 있다.
         그래서 필요한 순간에만 연결 열고, 끝나면 바로 닫는 게 기본 원칙.
         */

        /*
         *                                                       비유
            MySQL 서버           : 데이터 저장소 (주방)	      음식점 주방
            프로그램	            : DB에 데이터 요청하는 주체	      손님
            MySqlConnection	    : 프로그램과 DB 사이 연결 통로	 전화선 / 다리
            connectionString    : 연결에 필요한 설정 정보	     전화번호 (주소)
            Open()	            : 연결 시작	                 전화 받기 (통화 시작)
            Close()	            : 연결 종료	                 전화 끊기 (통화 종료) 
         */

        #region 
        /*
         * GetConnection() 내부에서 오류 → throw로 예외 던짐
         * try-catch로 그걸 직접 잡아서 처리
         * finally에서 연결을 닫든, 로그를 남기든 처리
         안잡으면? 예외는 계속 타고 올라가서 앱이 죽는다.
          ㄴ MysqlDatabaseManager 안에서는 가능하면 throw;로 넘기고,  
          ㄴ 사용하는 쪽에서 try-catch로 책임지고 잡게" 설계

         var db = MysqlDatabaseManager.GetInstance();
         MySqlConnection conn = null;

        try
        {
            conn = db.GetConnection(); // 여기서 예외 발생 가능
            // TODO: SQL 커맨드 수행
        }
        catch (MySqlException ex)
        {
            // 여기서 받은 예외 처리
            Console.WriteLine($"MySQL 연결 실패: {ex.Message}");
        }
        finally
        {
            db.CloseConnection(conn); // 무조건 닫기
        }     
         */
        #endregion

        /// <summary>
        /// 외부에서 받은 MySqlConnection을 안전하게 닫아주는 메서드
        /// </summary>
        public void CloseConnection(MySqlConnection conn)
        {
            if (conn != null && conn.State != System.Data.ConnectionState.Closed)
            {
                conn.Close(); // Close() 메서드를 호출해서 DB와 연결을 닫음
                // ㄴ 이걸로 연결이 끊어지고, 더 이상 DB와 통신하지 않는다.
                conn.Dispose(); // 연결 리소스 정리
                // ㄴ Dispose()는 닫은 연결에 할당된 메모리 자원이나 기타 시스템 자원을 해제하는 역할
                // ㄴ  쓰던 방을 청소하고 비워서 다음에 쓸 수 있게 만드는 것.
            }
        }
        /* if (conn != null && conn.State != System.Data.ConnectionState.Closed) 해석
         conn이 null이 아니고 (즉, 연결 객체가 존재하고)
         현재 연결 상태가 닫혀있지 않은 상태인지 체크
         만약 연결이 없거나 이미 닫혔으면 닫을 필요가 없으니 실행되지 않고 끝남
         */

        /*
         사용예시
            var db = MysqlDatabaseManager.GetInstance();
            var conn = db.GetConnection();
            // ... 사용 중 ...
            db.CloseConnection(conn); // 명시적으로 닫음
         
         */

        /// <summary>
        /// 비동기 작업 시 사용.(비동기 함수 안에서만 사용 가능)
        /// MySQL 데이터베이스에 비동기적으로 연결을 열고 연결 객체를 반환하는 메서드
        /// </summary>
        /// <returns>
        /// 연결이 성공적으로 열리면, 열려있는 MySqlConnection 객체를 Task 형태로 반환
        /// 연결 실패 시 MySqlException 예외가 throw 
        /// </returns>
        public async Task<MySqlConnection> GetConnectionAsync()
        {
            try
            {
                // MySqlConnection 객체 생성 (연결 문자열 사용)
                var conn = new MySqlConnection(connStr);
                await conn.OpenAsync();  // 비동기 방식으로 DB 연결 열기 (OpenAsync)
                return conn; // 연결 성공 시 열린 커넥션 반환
            }
            catch (MySqlException ex)
            {    // 연결 실패 시 예외 메시지를 디버그 출력
                Debug.WriteLine($"[DB 연결 실패] {ex.Message}");
                // 예외를 호출한 쪽으로 다시 던짐
                throw;
            }

        } // 이 메서드 호출 뒤 연결 닫아주는 것도 해야함
        // CloseConnection(conn)호출 하던지, await using 사용해서 닫음
    }
}