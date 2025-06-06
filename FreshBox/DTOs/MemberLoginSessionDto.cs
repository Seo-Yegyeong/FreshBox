using FreshBox.Enums;
using FreshBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshBox.DTOs
{
    public class MemberLoginSessionDto
    {
        //set이 없는 읽기 전용 프로퍼티는 생성자에서만 초기화할 수 있음
        public int Id { get;}          // 회원 고유 ID
        public string Username { get; }    // 로그인 아이디
        public string MemberName { get; }  // 사용자 이름 또는 닉네임
        public Role Role { get; }               // 권한 (Enum)


        // 생성자
        public MemberLoginSessionDto(int id, string username, string memberName, Role role) {
            Id = id;
            Username = username;
            MemberName = memberName;
            Role = role;
        }

        // 엔터티 -> dto타입으로 변경하는 메서드
        //  DTO 생성 용도라 정적(static) 메서드로 만듬
        public static MemberLoginSessionDto FromEntity(Member member)
        {
            if (member == null) // 아규먼트로 넣은 member객체가 null이면 예외를 호출부로 던짐
                throw new ArgumentNullException(nameof(member));

            return new MemberLoginSessionDto(member.Id,member.Username,member.MemberName,member.Role);
        }
    }
}
