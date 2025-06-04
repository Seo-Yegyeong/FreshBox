using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshBox.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {   //ObservableObject 상속해서 INotifyPropertyChanged 자동 구현

        // 현재 화면에 표시할 UserControl의 ViewModel을 저장하는 속성
        [ObservableProperty]
        private object currentView;
        // [ObservableProperty]로 속성 자동 생성 + 변경 알림
        // CurrentView 바뀌면 UI 자동 갱신 (ContentControl 바인딩 대상)

        // 생성자: 앱 시작 시 초기 화면을
        public MainWindowViewModel()
        {
            CurrentView = new SignInViewModel();  // 시작 화면 뷰모델 지정
        }

        // 화면 전환 메서드: 다른 화면(ViewModel)으로 바꾸고 싶을 때 호출
        public void ChangeView(object newViewModel)
        {
            CurrentView = newViewModel;
        }

    }
}
