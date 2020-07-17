using Oracle.ManagedDataAccess.Client;
using System;
using System.Windows;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Administrator.xaml 的交互逻辑
    /// </summary>
    public partial class Administrator : Window
    {
        AdminOpr adminOpr;
        public Administrator()
        {
            InitializeComponent();

        }
        public Administrator(AdminOpr adminOpr)
        {
            this.adminOpr = adminOpr;
            InitializeComponent();
            name.Text = adminOpr.AdminName;
            id.Text = adminOpr.Id;
            age.Text = adminOpr.Age.ToString();
            phone.Text = adminOpr.Phone;
            password.Password = adminOpr.Password;
            if (adminOpr.Sex == "男")
                boy.IsChecked = true;
            else
                girl.IsChecked = true;


        }



        private void Button_Click(object sender, RoutedEventArgs e)//读者管理
        {
            Admain_Opr_Books admain_Opr_Books = new Admain_Opr_Books();
            Application.Current.MainWindow = admain_Opr_Books;
            admain_Opr_Books.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)  //图书管理
        {
            Admin_Opr_Reader admin_Opr_Reader = new Admin_Opr_Reader(adminOpr);
            Application.Current.MainWindow = admin_Opr_Reader;
            admin_Opr_Reader.Show();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            this.Close();
            mainWindow.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) //修改信息
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
                string cmdStr = @"update manager set manager_name='" + name.Text + "', age='"
                    + age.Text + "' ,sex='" + adminOpr.Sex + "',telephone='" + phone.Text + "',password='"+password.Password+"' where manager_id='" + id.Text + "'";
                
                oracleCommand.CommandText = cmdStr;
                result = oracleCommand.ExecuteNonQuery();
            }
            catch(Exception es)
            {
                MessageBox.Show(es.ToString());
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

        private void boy_Checked(object sender, RoutedEventArgs e)
        {
            boy.IsChecked = true;
            girl.IsChecked = false;
            adminOpr.Sex = "男";

        }

        private void girl_Checked(object sender, RoutedEventArgs e)
        {
            boy.IsChecked = false;
            girl.IsChecked = true;
            adminOpr.Sex = "女";

        }
    }
}
