using FreshBox.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshBox.Services
{
    public class MemberService
    {

        MemberRepository memberRepo = new MemberRepository();

        /// <summary>
        /// 주어진 username의 중복 여부를 확인하는 서비스 메서드입니다.(동기 방식) TODO : 추후 비동기로 바꾸기
        /// </summary>
        /// <param name="username">사용자가 입력한 사용자 ID(username)</param>
        /// <returns>
        /// username 중복 검사 결과를 반환합니다
        /// 중복 시  : true반환
        /// 중복 없음(사용 가능) : false반환
        /// </returns>
        public bool IsUsernameDuplicate(string username) {

            try
            {
                int result = memberRepo.FindByUsername(username);

                if (result == 0) // 중복 없음
                {
                    return false;
                }
                else if (result == 1) // DB에 중복된 username 있음
                {
                    return true;
                }
                else
                    throw new Exception("알 수 없는 DB 결과"); //호출부로 예외 던짐
            }
            catch (Exception ex) {
                Debug.WriteLine($"Error: {ex.Message}");
                throw; // 호출부로 예외 던짐
            }

        }


    }
}
