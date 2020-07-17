using System.Windows;
using Oracle.ManagedDataAccess.Client;


namespace LibraryManagementSystem
{
    /// <summary>
    /// student.xaml 的交互逻辑
    /// </summary>
    public partial class Reader : Window
    {

        User user;
        public Reader()
        {
            InitializeComponent();                  
        }
        public Reader(User user)
        {
            InitializeComponent();
            this.user = user;
            name.Text = user.UserName;
            id.Text = user.Id;
            password.Password = user.Password;
            phone.Text = user.PhoneNumber;
            email.Text = user.Email;
            if(user.Sex == "男")
                boy.IsChecked = true;
            else
                girl.IsChecked = true;

        }

        private void Button_Click(object sender, RoutedEventArgs e)  // 借书
        {
            Student_Borrow student_Borrow = new Student_Borrow(user);
            Application.Current.MainWindow = student_Borrow;
            student_Borrow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)  // 还书
        {
            Student_Return student_Return = new Student_Return(user);
            Application.Current.MainWindow = student_Return;
            student_Return.Show();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            this.Close();
            mainWindow.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)  //更改信息
        {
            string connectionString = "Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS" +
           " = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)))" +
           " (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = orcl)));User ID = C##liang; Password=liang;";
            OracleConnection oracleConnection = new OracleConnection(connectionString);
            int result = 0;
            try
            {
                oracleConnection.Open();
                OracleCommand oracleCommand = oracleConnection.CreateCommand();
                string cmdStr = @"update reader set reader_name='" + name.Text + "',sex='"+ user.Sex +"', telephone='" 
                    + phone.Text + "' ,email='" + email.Text + "',password='" + password.Password + "' where reader_id='"+id.Text+"'" ;
                oracleCommand.CommandText = cmdStr;
                result = oracleCommand.ExecuteNonQuery();
                oracleConnection.Close();
            }
            catch 
            {
                MessageBox.Show("修改失败！");

            }
            if(result == 1)
            {
                MessageBox.Show("修改成功！");
            }
        }

        private void boy_Checked(object sender, RoutedEventArgs e)
        {
            boy.IsChecked = true;
            girl.IsChecked = false;
            user.Sex = "男";

        }

        private void girl_Checked(object sender, RoutedEventArgs e)
        {
            boy.IsChecked = false;
            girl.IsChecked = true;
            user.Sex = "女";

        }
    }

}
