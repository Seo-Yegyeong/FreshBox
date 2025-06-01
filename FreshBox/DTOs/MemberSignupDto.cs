using FreshBox.Enums;
using FreshBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshBox.DTOs
{
    // DTO : 데이터 전송용 클래스
    /*
     ViewModel (MemberSignupDto 생성, 유효성 검사)
        -> Service (DTO -> Model 변환, Repository 호출)
        -> Repository (DB Insert 실행)

        ViewModel에서 생성 및 유효성 검사 후 Service로 전달
        Service에서 DTO를 Entity로 변환하고 Repository 호출
     */
    public class MemberSignupDto
    {
        // 데이터 전송에 필요한 프로퍼티만 선언해서 사용함
        public string UserName { get; }
        public string Password { get; }
        public string MemberName { get; }
        public Role Role { get; }
        public string Phone { get; }
        public string Email { get; }
        public DateTime BirthDate { get; }
        public DateTime? HireDate { get; }


        // 모든 속성을 초기화 시키는 아규먼트가 있는 생성자(권한은 외부에서 값을 받지 않음)
        // // Role은 외부에서 입력받지 않고 Employee로 고정
        public MemberSignupDto(string userName, string password, string memberName,
            string phone, string email, DateTime birthDate, DateTime? hireDate)
        {
            UserName = userName;
            Password = password;
            MemberName = memberName;
            Role = Role.Employee; // 강제로 employee 지정해서 초기화
            // ㄴ 회원가입 시 권한이 무조건 직원(employee)이라서
            Phone = phone;
            Email = email;
            BirthDate = birthDate;
            HireDate = hireDate;
        }

        // DTO → Entity 변환 메서드(dto타입을 Member타입의 객체로 변환해서 리턴)
        // DB에 insert시킬 땐 Member타입으로 변환해서 넣음
        // C#에서는 빌더패턴 잘 안쓴다고 그래서 생성자 호출해서 초기화함
        public Member ToEntity()
        {
            return new Member
                (UserName, Password, MemberName, Role, Phone, Email, BirthDate, HireDate);
        }
    }
}
