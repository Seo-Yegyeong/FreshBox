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
using static MaterialDesignThemes.Wpf.Theme;

namespace FreshBox.Views
{
    /// <summary>
    /// Interaction logic for SignInView.xaml
    /// </summary>
    public partial class SignInView : UserControl
    {
        public SignInView()
        {
            InitializeComponent();
        }

        // SignInView.xaml.cs (UserControl 코드비하인드)
        // PasswordBox가 직접 바인딩이 안되어서 비하인드에서 작성함
        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            // UserName TextBox에서 텍스트 읽기
            string username = UserName.Text;

            // PasswordBox에서 비밀번호 읽기
            string password = PasswordBox.Password;

            // ViewModel의 로그인 서비스 메서드 직접 호출
            var vm = this.DataContext as SignInViewModel;
            if (vm != null)
            {
                vm.Authenticate(username, password);
            }
        }

    }

}

