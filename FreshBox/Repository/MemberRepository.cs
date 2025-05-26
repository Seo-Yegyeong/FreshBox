using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region // MySQL 쓰기 위한 밑 준비
using FreshBox.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Configuration; // MySQL 연결 문자열을 가져오기 위해 추가! 중요합니당!
#endregion

namespace FreshBox.Repository
{
    internal class MemberRepository
    {
        private readonly string connectionString;

        public MemberRepository()
        {
            // ConfigurationManager를 통해 MySQL 연결 문자열을 가져옵니다!
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            // DB 연결 성공 여부 테스트
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    Console.WriteLine("✅ DB 연결 성공!");
                    // 또는 로그, 메시지박스 등
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ DB 연결 실패: " + ex.Message);
            }
            // ======================
        }


        #region 예시 코드일 뿐입니다!
        public List<Member> GetAllMembers()
        {
            List<Member> Members = new List<Member>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Members";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Members.Add(new Member
                    {
                        Id = (int)reader["id"],
                        Name = reader["name"].ToString(),
                        Email = reader["email"].ToString()
                    });
                }
            }

            return Members;
        }
        #endregion
    }
}
