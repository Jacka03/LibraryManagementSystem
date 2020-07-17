using System.Windows;

namespace LibraryManagementSystem
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)  // 学生登录入口
        {
            Student_Register student_Register = new Student_Register();
            Application.Current.MainWindow = student_Register;
            this.Close();
            student_Register.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)   // 管理员登录入口
        {
            Administrator_Register administrator_Register = new Administrator_Register();
            Application.Current.MainWindow = administrator_Register;
            this.Close();
            administrator_Register.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)  // 退出程序
        {
            Application.Current.Shutdown();
        }
    }
}
