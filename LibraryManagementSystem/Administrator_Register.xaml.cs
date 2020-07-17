using System.Windows;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Administrator_Register.xaml 的交互逻辑
    /// </summary>
    public partial class Administrator_Register : Window
    {
        public Administrator_Register()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)  // 登录
        {

            AdminOpr adminOpr = new AdminOpr(id.Text, password.Password);

            if(adminOpr.adminLogin())
            {
                //MessageBox.Show("登录成功");

                Administrator administrator = new Administrator(adminOpr);
                Application.Current.MainWindow = administrator;
                this.Close();
                administrator.Show();
            }
            else
            {
                MessageBox.Show("登录失败");

            }


         
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)  // 注册
        {


            Adaim_Register adaim_Register = new Adaim_Register();
            Application.Current.MainWindow = adaim_Register;
            adaim_Register.Show();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)  // 返回
        {
            //Administrator_Register
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            this.Close();
            mainWindow.Show();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }
    }



}
