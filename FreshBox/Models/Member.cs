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
    /// MEMBER 테이블과 매핑되는 멤버 데이터 모델 클래스
    /// </summary>
    public class Member
    {
        // public 속성만 정의
        //-> 컴파일러가 자동으로 내부에 private 필드를 생성

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
            /// name 컬럼과 매핑됨 (NN), 사용자 이름
            /// 타입: VARCHAR(100)
            /// </summary>
            public string Name { get; set; } = string.Empty;

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
        }
    }
