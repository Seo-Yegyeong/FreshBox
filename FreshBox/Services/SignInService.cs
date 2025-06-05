using FreshBox.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshBox.Models;
using System.Diagnostics;
using FreshBox.DTOs;

namespace FreshBox.Services
{
    public class SignInService
    {
        MemberRepository memberRepo = new MemberRepository();

        /// <summary>
        /// 사용자가 입력한 로그인 정보로 인증을 시도함
        /// </summary>
        /// <param name="inputUsername">입력한 사용자명</param>
        /// <param name="inputPassword">입력한 비밀번호</param>
        /// <returns>로그인 성공 시 MemberSignInDto 객체, 실패 시 null</returns>
        public MemberSignInDto? SignIn(string inputUserName, string inputPwd)
        {
            try
            {

                Member? member = memberRepo.GetMemberByUserName(inputUserName);

                if (member == null)
                { // DB에 해당되는 username없음
                    return null;
                }


                bool isPwdValid = BCrypt.Net.BCrypt.Verify(inputPwd, member.Password);
                // 사용자가 입력한 비밀번호(inputPwd)와 DB에 저장된 해시된 비밀번호(member.Password)를 비교하여
                // 일치하면 true, 아니면 false를 반환함 (로그인 인증 시 사용)

                if (!isPwdValid)
                {
                    return null; // 비밀번호 불일치
                }

                // 여기서는 member가 null아님(위의 검사마쳤으니까 예외 안남)
                MemberSignInDto dto = MemberSignInDto.FromEntity(member);

                return dto; // 위의 검사로직 통과 시에(해당 되는 username있고 비밀번호도 일치하는 경우)
                // 로그인 성공 처리
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[예외][SignInService.SignIn]{ex.Message}");
                throw; // 호출부로 예외 던짐
            }
        }

    }
}
