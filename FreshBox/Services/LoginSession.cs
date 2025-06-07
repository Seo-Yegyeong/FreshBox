using FreshBox.DTOs;
using FreshBox.Enums;
using FreshBox.Models;
using FreshBox.Repository;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshBox.Services
{
    /// <summary>
    /// 로그인 한 사용자의 정보를 애플리케이션 전역에서 관리하는 
    /// 싱글턴 패턴으로 구현된 서비스 계층의 클래스
    /// </summary>
    public class LoginSession
    {
        MemberRepository memberRepo = new MemberRepository();

        // 싱글톤 인스턴스를 보관할 private static 필드
        private static LoginSession? instance;
        // static이므로 프로그램 전체에서 전역적으로 사용할 수 있음
        // LoginSession 클래스에 대한 단 하나의 instance만 존재함(싱글턴으로 관리)
        // 외부에서 직접 접근하거나 수정하지 못하도록 private로 감춤
        // 사용은 public으로 선언한 읽기 전용 프로퍼티(속성)을 호출

        private LoginSession() { } // 기본 생성자 private로 감춤
        // 외부에서 new로 객체 생성 못하도록
        // 외부에서 new LoginSession() 호출 불가


        /// <summary>
        /// 싱글톤 인스턴스를 반환하는 public static 메서드    
        /// 최초 호출 시에만 new LoginSession()로 객체생성한 뒤
        /// 다음 호출 시 부터는 계속 동일한 인스턴스를 반환
        /// -> 애플리케이션 전역에서 동일한 로그인 상태를 유지
        /// </summary>
        /// <returns>최소 호출 시에 객체를 생성하여 반환하고 
        /// 그 다음부터는 한번 생성 했던 인스턴스를 동일하게 반환</returns>
        public static LoginSession GetInstance() {
            // 인스턴스가 아직 생성되지 않았다면
            if (instance == null)
            {
                instance = new LoginSession();
                return instance; // 생성된 인스턴스 반환
            }
            // 이미 생성된 인스턴스가 있으면 그대로 반환
                return instance;
        }

        // 로그인 상태 관리용 프로퍼티 

        /// <summary>
        /// 로그인된 사용자(Member)의 고유 식별자(PK)를 보관
        /// 기본값은 -1이며, 이는 "아직 로그인되지 않은 상태"를 의미
        /// 외부에서는 private set으로 설정되어 있어 직접 수정할 수 없음
        /// SetLoginInfo() 메서드를 통해서만 변경 가능
        /// </summary>
        public int LoggedInMemberId { get; private set; } = -1;
        // -1은 아직 로그인 되지 않음을 의미함
        // private set;으로 선언하여 외부에서 직접 수정 불가
        // 값을 바꾸려면 SetLoginInfo() 메서드를 호출 해야함

        /// <summary>
        /// 로그인에 사용된 사용자ID(UserName)을 보관
        /// 기본값은 빈 문자열("")이며, "로그인되지 않은 상태"를 나타냄
        /// 외부에서는 private set으로 설정되어 있어 직접 수정할 수 없고,
        /// SetLoginInfo() 메서드를 통해서만 변경 가능
        /// </summary>
        public string LoggedInUsername { get; private set; } = string.Empty;

        /// <summary>
        /// 추가 정보 조회 후 저장할 사용자의 실제 이름(또는 닉네임).
        /// </summary>
        public string LoggedInMemberName { get; private set; } = string.Empty;

        /// <summary>
        /// 추가 정보 조회 후 저장할 사용자의 권한/직책 등
        /// </summary>
        public Role LoggedInRole { get; private set; } = Role.None;


        /// <summary>
        /// 로그인 성공 시 호출: 최소 정보(ID, UserName) 저장
        /// 로그인 성공 시 ID와 Username을 세션에 저장하는 메서드
        /// 로그인에 성공했을 때 호출하여 세션에 사용자 ID와 사용자명을 저장
        /// </summary>
        /// <param name="id">DB의 Member 테이블 PK 값</param>
        /// <param name="username">로그인에 사용된 계정명</param>
        public void SetLoginInfo(int id, string username) { 
            LoggedInMemberId = id;
            LoggedInUsername = username;
        }

        /// <summary>
        /// 로그인된 사용자의 추가 정보를 DB에서 조회하여 화면 표시용 필드에 저장
        /// 필요한 정보만 추가 로딩
        /// </summary>
        public void LoadAdditionalInfo()
        {
            // 아직 로그인되지 않은 상태면 아무 작업도 하지 않음
            if (LoggedInMemberId == -1) return;

            try {
                // DB에서 Member 전체 정보를 가져옴
                Member? member = memberRepo.GetMemberById(LoggedInMemberId);
                if (member == null) {
                    throw new InvalidOperationException("회원 정보를 찾을 수 없습니다.");
                    // 예외 만들어서 호출부로 던짐
                }
                    
                MemberLoginSessionDto dto = 
                    MemberLoginSessionDto.FromEntity(member);
                // member객체를 dto타입으로 변환

                // dto에서 화면에 표시할 추가 정보만 골라서 저장
                LoggedInMemberName = dto.MemberName;
                LoggedInRole = dto.Role;
               
            }
            catch(Exception ex) { 
                Debug.WriteLine($"[예외][LoginSession.LoadAdditionalInfo]{ex.Message}");
                throw; // 호출부로 예외 던짐
            }
        }

        /// <summary>
        /// 로그아웃 시 호출하여 세션 초기화
        /// 로그아웃할 때 호출하여 세션에 저장된 모든 로그인 정보를 초기값으로 되돌림
        /// LoggedInMemberId를 -1로 설정하여 "비로그인" 상태로 변경
        /// LoggedInUsername을 빈 문자열로 설정
        /// </summary>
        public void Clear()
        {
            LoggedInMemberId = -1;
            LoggedInUsername = string.Empty;
            LoggedInMemberName = string.Empty;
            LoggedInRole = Role.None;
        }
    }
}
