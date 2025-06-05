using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshBox.Enums; // Role enum이 정의된 네임스페이스

namespace FreshBox.Models
{
    /// <summary>
    /// 데이터베이스의 MEMBER 테이블과 매핑되는 멤버 데이터 모델 클래스
    /// </summary>
    public class Member
    {
        // public 속성만 정의
        //-> 컴파일러가 자동으로 내부에 private 필드를 생성

        public static int LoggedInMemberId { get; set; } = -1;  // 로그인 안 된 상태 초기값
        // -> 로그인 성공 시 LoggedInMemberId 저장

        #region (예시)
        // LoginService.cs 로그인 성공 시 LoggedInMemberId 저장 (예시)
        // 로그인 처리 메서드 예시
        /*
        public bool Login(string username, string password)
        {
            // DB에서 username, password 검사 후 멤버 정보 가져오기 (가정)
            Member loggedInMember = memberRepository.GetMemberByUsernameAndPassword(username, password);

            if (loggedInMember != null)
            {
                Member.LoggedInMemberId = loggedInMember.Id;  // 로그인 성공 시 static 변수 저장
                return true;
            }
            return false;
        }

        //입출고 로그 Insert 시 사용 예시
        public void InsertInventoryLog(int orderId, int productId, int quantity)
        {
            int memberId = Member.LoggedInMemberId;  // 로그인한 사원 ID 사용

            if (memberId == -1)
            {
                throw new InvalidOperationException("로그인하지 않은 상태에서 입출고 기록 불가");
            }

            // DB Insert 쿼리 예시
            string query = @"INSERT INTO INBOUND_LOG 
                   (order_id, product_id, quantity, member_id, inbound_at) 
                   VALUES 
                   (@orderId, @productId, @quantity, @memberId, NOW())";

            // DB 커넥션, 파라미터 세팅 후 실행 (생략)

            // 예를 들어, 파라미터에 memberId 넣기
        }*/
        #endregion

        /// <summary>
        /// id 컬럼과 매핑됨 (PK, Auto Increment)
        /// 타입: INT
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// username 컬럼과 매핑됨 (NN, UQ), 사용자 로그인 ID
        /// 타입: VARCHAR(50)
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// password 컬럼과 매핑됨 (NN), 암호화된 비밀번호
        /// 타입: VARCHAR(255)
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// member_name 컬럼과 매핑됨 (NN), 사용자 이름
        /// 타입: VARCHAR(30)
        /// </summary>
        public string MemberName { get; set; } = string.Empty;

        /// <summary>
        /// role 컬럼과 매핑됨 (NN), 사용자 권한 (employee 또는 admin)
        /// 타입: ENUM('employee', 'admin')
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// phone 컬럼과 매핑됨 (NN, NQ), 사용자 전화번호
        /// 타입: VARCHAR(20)
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// email 컬럼과 매핑됨 (NN, NQ), 사용자 이메일
        /// 타입: VARCHAR(100)
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// birth_date 컬럼과 매핑됨 (NN), 생년월일
        /// 타입: DATE
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// created_at 컬럼과 매핑됨, 회원가입일
        /// 타입: DATETIME, 기본값 CURRENT_TIMESTAMP
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// hire_date 컬럼과 매핑됨, 입사일 (nullable)
        /// 타입: DATE
        /// </summary>
        public DateTime? HireDate { get; set; }


        // C#에서는 빌더 패턴 잘 안쓴다고 그러네..
        public Member(string username, string password, string memberName, Role role,
              string phone, string email, DateTime birthDate,
              DateTime? hireDate = null)
        {
            this.Username = username;
            this.Password = password;
            this.MemberName = memberName;
            this.Role = role;
            this.Phone = phone;
            this.Email = email;
            this.BirthDate = birthDate;
            this.HireDate = hireDate;
        }

        public Member(int id, string username, string password) {
            this.Id = id;
            this.Username = username;
            this.Password = password;
        }
        public Member() { } // 기본 생성자

    }
}
