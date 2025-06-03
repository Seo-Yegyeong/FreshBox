using FreshBox.DTOs;
using FreshBox.Models;
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

        /// <summary>
        /// email 중복 검사 메서드
        /// 데이터베이스에 중복된 email이 저장되어 있는 지 검사합니다.
        /// </summary>
        /// <param name="email">검사할 email문자열</param>
        /// <returns>true(중복), false(중복되지 않음)</returns>
        public bool IsEmailDuplicate(string email)
        {
            bool isDuplicate; // 이해하기 쉬우라고 일부러 변수에 한번 더 담음

            try { 
                int result = memberRepo.FindByEmail(email);
                // 리턴값 : -1 예외발생, 0 중복없음(사용가능), 1 중복(사용불가)

                if (result == 0)
                {
                    isDuplicate = false; // 중복입니까? 아니요
                    return isDuplicate;
                }
                else if (result == 1) {
                    isDuplicate = true; // 중복입니까? 예
                    return isDuplicate;
                }
                else 
                {
                    throw new Exception($"[예외] [SignUpService.IsEmailDuplicate]" +
                        $" Unexpected DB result in FindByEmail: {result}");
                    // 호출부로 예외 던짐
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[예외 발생][SignUpService.IsEmailDuplicate()] {ex.Message}");
                throw; //호출했던 곳으로 예외를 던짐
            }
              
        }

        /// <summary>
        /// 회원가입 로직을 처리하는 서비스 메서드
        /// </summary>
        /// <param name="dto"> 유효성 검사를 마친 회원가입 정보를 담은 MemberSignUpDto 객체</param>
        /// <returns>
        /// 회원가입 성공 시 true, 실패 시 false를 반환
        /// </returns>
        public bool MemberSignUp(MemberSignUpDto dto) {
            try
            {
                //비밀번호 해시 처리 (BCrypt 사용)
                string hashedPwd = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                // ㄴ HashPassword(...): dto.Password를 BCrypt 해시로 변환
                /* 
                    BCrypt란?
                    해킹당해도 비밀번호 못 알아보게 만드는 일방향 암호화 알고리즘.
                    느리게 설계되어서 무차별 대입 공격에 강함
                    내부적으로 Salt를 자동 추가해서 같은 비밀번호라도 해시값이 매번 다르게 나옴
                    인증할 때는 CheckPassword()로 비교
                 */

                // dto.Password = hashedPwd; -> dto 속성은 읽기전용으로 선언했기 때문에 set이 없음
                // Entity타입으로 변환 시에 아규먼트로 넘겨서 Member객체 받기로 설정 

                // Dto → Entity 변환
                Member member = dto.ToEntity(hashedPwd);

                // 레퍼지토리로 넘겨서 DB에 insert
                int result = memberRepo.InsertMember(member);

                if (result == 1) // insert성공 시 실행
                {
                    return true; 
                }
                else if (result == 0) // insert실패 시 실행
                {
                    return false;

                }
                else
                { // result == -1 (예외 발생)
                    throw new Exception($"[예외] [SignUpService.MemberSignUp]");
                    // 호출부로 예외 던짐
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[예외] [SignUpService.MemberSignUp] {ex.Message}");
                throw; // 호출부로 예외던짐
            }

        }


    }
}
