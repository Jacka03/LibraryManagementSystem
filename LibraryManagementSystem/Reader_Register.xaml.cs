using Oracle.ManagedDataAccess.Client;
using System.Windows;


namespace LibraryManagementSystem
{
    /// <summary>
    /// Student_Register.xaml 的交互逻辑
    /// </summary>
    public partial class Student_Register : Window
    {
        public Student_Register()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // 登录
        {
            User user = new User(reader_Id.Text, reader_Password.Password);
            if (user.UserLogin())
            {
                Reader reader = new Reader(user);
                Application.Current.MainWindow = reader;
                this.Close();
                reader.Show();
            }
            else
            {
                MessageBox.Show("登陆失败！");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)  // 注册
        {

            User_register user_Register = new User_register();
            Application.Current.MainWindow = user_Register;
            user_Register.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)  // 返回
        {
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            this.Close();
            mainWindow.Show();
        }


        //修改用户
        public int UpdateUser(User user)
        {
            string connectionString = "Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS" +
                " = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)))" +
                " (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = orcl)));User ID = C##liang; Password=liang;";
            OracleConnection oracleConnection = new OracleConnection(connectionString);
            oracleConnection.Open();
            OracleCommand oracleCommand = oracleConnection.CreateCommand();
            string sql = @"UPDATE reader set userName='" + user.UserName + "',password='" + user.Password + "',email='" + user.Email + "',phoneNumber='" + user.PhoneNumber + "'WHERE id='" + user.Id + "'";
            oracleCommand.CommandText = sql;
            int result = oracleCommand.ExecuteNonQuery();
            oracleConnection.Close();
            return result;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }
    }
}
