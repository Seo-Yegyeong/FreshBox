using FreshBox.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FreshBox.Views
{
    /// <summary>
    /// SignUpView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SignUpView : UserControl
    {
        public SignUpView()
        {
            InitializeComponent();

            /*
             1. 회원가입 성공 시 뷰모델에서 ClearPasswords() 호출
                -> 비밀번호 초기화 요청 이벤트 발생
             2. ClearPasswordsRequested 이벤트 실행됨
                -> 뷰에 초기화 요청 신호 전달
             3. 뷰(SignUpView)는 이 이벤트를 구독하고 있음
                -> 이벤트가 발생하면 즉시 알게 됨
             4. 뷰에서 ClearPasswordBoxes() 메서드를 호출하여
                PasswordBox 두 개를 초기화(빈 문자열로 비움)
            */
            // DataContext가 뷰모델(SignUpViewModel)인지 확인
            if (DataContext is SignUpViewModel vm)
            {
                // 뷰모델의 이벤트에 구독 추가
                vm.ClearPasswordsRequested += () =>
                {
                    // 이벤트가 발생하면 여기 코드 실행
                    ClearPasswordBoxes(); // 실제 비밀번호 박스 초기화 함수 호출
                };
            }
        }

        // 뒷코드 쓰고 싶지 않았지만,, PasswordBox가 직접 바인딩 지원하지 않아서..
        private void PwdBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // DataContext가 SignUpViewModel 타입인지 확인
            if (DataContext is SignUpViewModel vm)
            {
                // PasswordBox에서 현재 입력된 비밀번호를 ViewModel의 Pwd 프로퍼티에 직접 할당
                vm.Pwd = ((PasswordBox)sender).Password;
            }
        }

        private void ConfirmPwdBox_PasswordChanged(object sender, RoutedEventArgs e) {
            // DataContext가 SignUpViewModel 타입인지 확인
            if (DataContext is SignUpViewModel vm)
            {
                // PasswordBox에서 현재 입력된 비밀번호를 ViewModel의 ConfirmPwd 프로퍼티에 직접 할당
                vm.ConfirmPwd = ((PasswordBox)sender).Password;
            }
        }

        public void ClearPasswordBoxes()
        {
            PwdBox.Password = string.Empty;           // PwdBox는 x:Name="PwdBox"로 네임 지정 필요
            ConfirmPwdBox.Password = string.Empty;    // x:Name= ConfirmPwdBox도 네임 지정 필요
        }
    }
}
