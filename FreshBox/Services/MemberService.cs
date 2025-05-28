using FreshBox.Repository;
using System;
using System.Collections.Generic;
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
        /// 중복 검사 결과에 따른 메시지를 반환합니다.
        /// - 중복이 없으면 "사용하실 수 있는 ID입니다."
        /// - 중복이 있으면 "사용할 수 없는 아이디입니다. 다른 아이디를 입력해 주세요."
        /// - 에러 발생 시 적절한 오류 메시지를 반환합니다.
        /// </returns>
        public string CheckUsernameDuplicate(string username) {

            int result = memberRepo.FindByUsername(username);

            if (result == 0) // 중복 없음
            {
                return "사용하실 수 있는 ID입니다.";
            }
            else if (result == 1) // DB에 중복된 username 있음
            {
                return "사용할 수 없는 아이디입니다. 다른 아이디를 입력해 주세요.";
            }
            else { // 에러 
                // 네트워크, DB 연결 문제 등 예외 상황에 대한 사용자 안내 메시지
                return "Error : 서버 오류가 발생했습니다. 잠시 후 다시 시도해 주세요.";
            }           
        }


    }
}
