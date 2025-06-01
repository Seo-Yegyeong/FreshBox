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
    }
}
