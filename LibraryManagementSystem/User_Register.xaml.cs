using Oracle.ManagedDataAccess.Client;
using System.Windows;

namespace LibraryManagementSystem
{
    /// <summary>
    /// user_register.xaml 的交互逻辑
    /// </summary>
    public partial class User_register : Window
    {
        User user;
        Users users;

        bool flag;
        public User_register()
        {
            user = new User();
            users = new Users();
            flag = true;
            InitializeComponent();
        }

        public User_register(Users users)
        {
            flag = false;
            this.users = users;
            user = new User();
            InitializeComponent();
            button1.Content = "修改";
            name.Text = users.Name;
            id.Text = users.Id;
            phone.Text = users.Phone;
            email.Text = users.Email;
            password.Text = users.Password;
            password1.Text = users.Password;

            id.IsEnabled = false;

            if (users.Sex == "男")
                boy.IsChecked = true;
            else
                girl.IsChecked = true;
            
        }

        public void Insert() //注册
        {
            if (user.SameUser(id.Text))
            {
                if (!password.Text.Equals(password1.Text))
                {
                    MessageBox.Show("两次输入的密码不相等，请重新输入");
                }
                else
                {
                    user.UserName = name.Text;
                    user.Id = id.Text;
                    user.PhoneNumber = phone.Text;
                    user.Email = email.Text;
                    user.Password = password.Text;

                    if (user.Register(user) > 0)
                    {
                        MessageBox.Show("注册成功");
                    }
                    else
                    {
                        MessageBox.Show("注册失败");
                    }
                }
            }
            else
            {
                MessageBox.Show("该用户已经存在咯，请换个账号吧！");

            }
        }


        public void Update() //修改信息
        {
            if (password.Text.Equals(password1.Text))
            {

                string connectionString = "Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS" +
               " = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)))" +
               " (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = orcl)));User ID = C##liang; Password=liang;";

                OracleConnection oracleConnection = new OracleConnection(connectionString);

                string cmdStr = @"update reader set reader_name ='" + name.Text + "',sex='" + users.Sex + "',telephone='"
                       + phone.Text + "', email='" + email.Text + "',password='" + password.Text + "'  WHERE reader_id='" + id.Text + "'";

                int result = 0;
                try
                {
                    oracleConnection.Open();
                    OracleCommand oracleCommand = oracleConnection.CreateCommand();
                    oracleCommand.CommandText = cmdStr;
                    result = oracleCommand.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("修改失败！");
                }
                finally
                {
                    oracleConnection.Close();
                }
                if (result == 1)
                {
                    MessageBox.Show("修改成功！");
                }
            }
            else
            {
                MessageBox.Show("密码不一致");
            }
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)  //注册
        {
            if(flag)
            {
                Insert();
            }
            else
            {
                Update();
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            users.Sex = "男";

            user.Sex = "男";
            girl.IsChecked = false;
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            users.Sex = "女";

            user.Sex = "女";
            boy.IsChecked = false;
        }
    }
}
