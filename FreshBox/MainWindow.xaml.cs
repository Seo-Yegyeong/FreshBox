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



namespace FreshBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        // DB 연결 테스트 버튼 클릭 이벤트 핸들러예요!
        private void TestDbConnection_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM member";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    var count = Convert.ToInt32(cmd.ExecuteScalar());
                    MessageBox.Show($"사용자 수: {count}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ DB 연결 실패: " + ex.Message);
            }
        }
    }
}