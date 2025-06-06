using FreshBox.ViewModels;
using FreshBox.Views;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FreshBox.Services;


namespace FreshBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent(); // XAML 요소 로드

            // MainWindow.xaml에 꽂아넣은 ScreenHolderContentControl을
            // NavigationService에 연결
            //  ViewNavigationService 초기화 (ContentControl 지정)
            ViewNavigationService.Initialize(ScreenHolderContentControl);
            // ScreenHolderContentControl은 WPF MainWindow.xaml 파일에서
            // 이미 생성되어 있는 컨트롤 인스턴스
            // 그래서 직접 new로 만들 필요 없이 바로 참조해서 넘겨줄 수 있음

            //화면에 사용할 뷰를 등록
            ViewNavigationService.Instance.RegisterView("SignIn", new SignInView());
            // ㄴ SignIn이라는 이름으로 SignInView UserControl을 등록함

            ViewNavigationService.Instance.RegisterView("SignUp", new SignUpView());
            ViewNavigationService.Instance.RegisterView("MainVisual", new MainVisualView());
            ViewNavigationService.Instance.RegisterView("WorkMenu", new WorkMenuView());
            ViewNavigationService.Instance.RegisterView("CheckInOut", new CheckInOutView());
            ViewNavigationService.Instance.RegisterView("MyWorkLog", new MyWorkLogView());

            ViewNavigationService.Instance.RegisterView("MyOrder", new MyOrderView());
            ViewNavigationService.Instance.RegisterView("Test", new TestView());

            // 시작 화면 지정 - 등록한 뷰 이름으로 접근
            ViewNavigationService.Instance.NavigateTo("SignIn");


        }

    }
}