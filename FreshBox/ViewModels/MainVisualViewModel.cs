using CommunityToolkit.Mvvm.ComponentModel;
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

    }
}
