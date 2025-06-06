using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshBox.Models;

namespace FreshBox.DTOs
{
    public class MemberSignInDto
    {
        public int Id { get; }
        public string Username { get; }

        // public string Password { get; }
        //비밀번호 해시 비교는 서비스 내부에서만 수행하고, DTO로는 절대 노출하지 않도록 설계

        public MemberSignInDto(int id, string username)
        {
            Id = id;
            Username = username;
        }


        // Member -> MemberSignInDto으로 변환해주는 static메서드 
        public static MemberSignInDto FromEntity(Member member)
        {
            if (member == null) // 아규먼트로 넣은 member객체가 null이면 예외를 호출부로 던짐
                throw new ArgumentNullException(nameof(member));

            return new MemberSignInDto(member.Id, member.Username);
        }
    }
}
