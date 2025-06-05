using FreshBox.Enums;
using FreshBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshBox.DTOs
{
    // DTO(Data Transfer Object) : 데이터 전송용 클래스
    // (중요) 데이터 전송에 필요한 프로퍼티만 선언해서 사용한다!!
    /*
     ViewModel (MemberSignUpDto 생성, 유효성 검사)
        -> Service (DTO -> Model 변환, Repository 호출)
        -> Repository (DB Insert 실행)

        ViewModel에서 생성 및 유효성 검사 후 Service로 전달
        Service에서 DTO를 Entity로 변환하고 Repository 호출
     */

    // DTO를 사용하는 이유
    /*
        1. 계층 간 의존성 분리
            ㄴ 필요한 데이터만 딱 정해서 전달하므로 느슨한 결합 유지됨.
        2. 불필요한 데이터 차단 (보안 + 성능)
            ㄴ Entity에 있는 비밀번호, 권한 같은 민감 정보를 클라이언트에 보내면 큰일
            ㄴ DTO에는 그런 민감 정보 안 넣음으로써 정보 노출 차단.
        3. 입력/출력 구조 명확히 분리
            ㄴ DB에 입력시에는 Dto -> Entity타입으로
            ㄴ DB에서 가져와서 사용할 때는 Entity -> Dto타입으로 변환해서 사용
        4. 유효성 검사를 ViewModel 따로 처리
            ㄴ MVVM 구조에서 ViewModel에서 DTO 생성 → 검증 → 서비스 전달까지 
        5. API 스펙 유지 용이
            ㄴ Entity가 바뀌어도 DTO는 그대로 유지할 수 있음 → API 깨지지 않음
     */
    public class MemberSignUpDto
    {
        // 데이터 전송에 필요한 프로퍼티만 선언해서 사용함
        // { get; } 읽기 전용 프로퍼티 -> 생성자에서만 값을 설정할 수 있음.
        public string Username { get; }
        public string Password { get; } 
        public string MemberName { get; }
        public Role Role { get; }
        public string Phone { get; }
        public string Email { get; }
        public DateTime BirthDate { get; }
        public DateTime? HireDate { get; }


        // 모든 속성을 초기화 시키는 아규먼트가 있는 생성자(권한은 외부에서 값을 받지 않음)
        // // Role은 외부에서 입력받지 않고 Employee로 고정
        public MemberSignUpDto(string username, string password, string memberName,
            string phone, string email, DateTime birthDate, DateTime? hireDate)
        {
            Username = username;
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
        public Member ToEntity(string hashedPwd)
        {
            // 서비스에서 DTO를 Entity로 변환할 때 암호화된 비밀번호로 넣어주기
            return new Member
            {
                Username = this.Username,
                Password = hashedPwd,
                MemberName = this.MemberName,
                Role = this.Role,
                Phone = this.Phone,
                Email = this.Email,
                BirthDate = this.BirthDate,
                HireDate = this.HireDate
            };
        }
    }
}
