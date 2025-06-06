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
    /// WorkMenuView와 바인딩 된 뷰모델 클래스
    /// </summary>
    public partial class WorkMenuViewModel : ObservableObject
    {

        /// <summary>
        /// 뒤로가기 클릭 시 실행되는 메서드
        /// 이전에 보았던 화면으로 돌아감
        /// </summary>
        [RelayCommand]
        private void GoToBack() { 
            ViewNavigationService.Instance.GoBack();
        }
    }
}
