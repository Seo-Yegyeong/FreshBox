using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FreshBox.Enums;
using FreshBox.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshBox.ViewModels
{
    // MainVisualView와 바인딩
    public partial class MainVisualViewModel : ObservableObject
    {
        // 로그인 성공 시에 뷰에 띄울 사용자 정보
        // LoginSession에서 저장된
        // 로그인 정보(사용자 이름, 실제 이름, 권한 등)를 뷰모델에 불러와 UI에 바인딩
        [ObservableProperty]
        private string loginUsername = string.Empty;

        [ObservableProperty]
        private string loginMemberName = string.Empty;

        [ObservableProperty]
        private string loginRole = string.Empty;

        /// <summary>
        /// LoginSession에 저장된 현재 로그인 사용자 정보를
        /// MainVisualViewModel의 프로퍼티에 복사하여
        /// UI에 표시할 준비를 하는 메서드
        /// </summary>
        public void LoadUserInfo()
        {
            LoginSession session = LoginSession.GetInstance();
            LoginUsername = session.LoggedInUsername;
            LoginMemberName = session.LoggedInMemberName;
            LoginRole = session.LoggedInRole.ToString();
        }


        [RelayCommand]
        private void GoToMyOrderArrival() {
            // 입고 페이지로 이동
            ViewNavigationService.Instance.NavigateTo("MyOrder");
        }

        /// <summary>
        /// MainVisualView에서 로그아웃 버튼 클릭 시 실행되는 메서드
        /// 로그아웃 처리 로직을 담당
        /// </summary>
        [RelayCommand]
        private void SignOut()
        {
            // 로그인 세션 초기화
            LoginSession.GetInstance().Clear();

            //로그인 페이지로 이동
            ViewNavigationService.Instance.NavigateTo("SignIn");
        }


        [RelayCommand]
        private void GotoProductArrival()
        {
            // 상품 등록 페이지로 이동 - 지금은 test로 이동하는데, 현승님이 페이지 push해주시면 그걸로 바꿔야 함.
            ViewNavigationService.Instance.NavigateTo("Test");
        }
    }
}
