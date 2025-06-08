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
    /// <summary>
    ///  MyWorkLogView와 바인딩 되어있는 클래스
    /// </summary>
    public partial class MyWorkLogViewModel : ObservableObject
    {
        // 화면 전환 메서드 선언 -----------------------------------

        /// <summary>
        /// 뒤로가기 버튼 클릭 시 호출됨
        /// 이전에 보았던 화면으로 돌아감
        /// </summary>
        [RelayCommand]
        private void GoToBack()
        {
            ViewNavigationService.Instance.GoBack();
        }
    }
}
