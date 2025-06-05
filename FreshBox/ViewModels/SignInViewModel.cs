using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FreshBox.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshBox.ViewModels
{
    // 로그인 화면의 데이터와 로직을 담당하는 뷰모델
    // SignInView와 바인딩 되어있음
    public partial class SignInViewModel : ObservableObject
    {

        // 기본 생성자
        // XAML에서 DataContext를 뷰모델로 지정할 때 뷰모델 객체가 생성되면서 기본 생성자가 호출됨
        public SignInViewModel() { }


        /// <summary>
        /// SignUp 화면으로 네비게이션(화면 전환) 처리 메서드
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

    }
}
