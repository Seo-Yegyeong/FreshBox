using FreshBox.Repository;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshBox.Services
{
    public class SignUpService
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
                else {
                    throw new Exception("ERROR : DB 예외발생"); //호출부로 예외 던짐
                }
                    
            }
            catch (Exception ex) {
                Debug.WriteLine($"Error: {ex.Message}");
                throw; // 호출부로 예외 던짐
            }

        }

        /// <summary>
        /// phone 중복 검사 서비스 메서드
        /// </summary>
        /// <param name="phoneNum">유효성 검사가 완료된 사용자 입력값</param>
        /// <returns>true 중복된 번호(사용 불가), false 중복 없음(사용 가능)</returns>
        public bool IsPhoneDuplicate(string phoneNum) {
            
            bool isDuplicate; // 코드 이해 돕기위해 결과를 변수에 담음

            try {
                int result = memberRepo.FindByPhoneNum(phoneNum);
                // ㄴ 리턴 값 1이면 중복 있음 , 0이면 중복 없음

                if (result == 0)
                { // 중복 없음
                    isDuplicate = false; // 중복입니까? 아니요
                    return isDuplicate;
                }
                else if (result == 1)
                {
                    isDuplicate = true; // 중복 입니까? 네
                    return isDuplicate;
                }
                else {
                    throw new Exception($"[예외] [SignUpService.IsPhoneDuplicate]" +
                        $" Unexpected DB result in FindByPhoneNum: {result}"); 
                    // 호출부로 예외 던짐
                }
            }
            catch(Exception ex) {
                Debug.WriteLine($"[예외] [SignUpService.IsPhoneDuplicate] Error: {ex.Message}");
                throw; // 호출부로 예외 던짐
            }
        }


    }
}
