using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FreshBox.DTOs;
using FreshBox.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FreshBox.ViewModels
{
    // 로그인 화면의 데이터와 로직을 담당하는 뷰모델
    // SignInView와 바인딩 되어있음
    public partial class SignInViewModel : ObservableObject
    {
        // 필드 선언

        // 로그인 서비스 주입 또는 생성
        private readonly SignInService signInsvc = new SignInService();


        // 기본 생성자
        // XAML에서 DataContext를 뷰모델로 지정할 때 뷰모델 객체가 생성되면서 기본 생성자가 호출됨
        public SignInViewModel() { }


        /// <summary>
        /// SignUpView 화면으로 네비게이션(화면 전환) 처리하는 메서드
        /// 회원가입 버튼과 바인딩 되어있음. 그래서 회원가입버튼 클릭시 실행되는 메서드
        /// </summary>
        [RelayCommand]
        private void GoToSignUp()
        {
            // ViewNavigationService의 싱글톤 인스턴스를 통해
            // "SignUp"라고 등록한 뷰 화면으로 이동하도록 요청
            // 뷰 등록은 메인 윈도우에서 함(주의 -> 등록 안하면 예외 발생)
            ViewNavigationService.Instance.NavigateTo("SignUp");
        }

        /// <summary>
        /// 로그인 버튼 클릭 시 SignInView의 코드 비하인드에서 호출함
        /// 로그인 버튼 클릭 시 호출되는 인증 메서드.
        /// 사용자가 입력한 아이디와 비밀번호를 검증하고,
        /// 유효한 경우 로그인 처리를 진행함.
        /// </summary>
        /// <param name="inputUsername">입력한 사용자 아이디</param>
        /// <param name="inputPwd">입력한 비밀번호</param>
        public void Authenticate(string inputUsername, string inputPwd)
        {

            if (string.IsNullOrWhiteSpace(inputUsername)
                || string.IsNullOrWhiteSpace(inputPwd))
            {
                // username 또는 pwd를 입력 하지 않았을 때 실행
                MessageBox.Show("아이디 또는 비밀번호를 입력하세요.", "입력 확인");
                return;
            }

            try
            {
                // 서비스 호출 후 결과 처리
                MemberSignInDto? dto = signInsvc.SignIn(inputUsername, inputPwd);

                if (dto != null) // 로그인 성공 시 실행
                {
                    // 로그인 성공 시 LoginSession에 ID·UserName 저장
                    LoginSession.GetInstance().SetLoginInfo(dto.Id, dto.Username);

                    // 추가 사용자 정보 DB에서 읽어오기
                    LoginSession.GetInstance().LoadAdditionalInfo();

                    // 로그인 성공 후 처리 (예: 화면 이동)
                    ViewNavigationService.Instance.NavigateTo("MainVisual");
                }
                else
                {
                    // 로그인 실패 처리 (예: 메시지 띄우기)
                    MessageBox.Show(
                        "아이디 또는 비밀번호가 올바르지 않습니다.",
                        "입력 오류",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning
                        );
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[예외][SignInViewModel.Authenticate]{ex.Message}");
                MessageBox.Show(
                    "⚠️ Error : 서버 오류가 발생했습니다. 잠시 후 다시 시도해 주세요.",
                    "ERROR",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

        }

    }
}
