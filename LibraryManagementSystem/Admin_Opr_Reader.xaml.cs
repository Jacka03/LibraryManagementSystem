using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Admin_Opr_Reader.xaml 的交互逻辑
    /// </summary>
    public partial class Admin_Opr_Reader : Window
    {
        string connectionString = "Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS" +
            " = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)))" +
            " (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = orcl)));User ID = C##liang; Password=liang;";

        OracleConnection oracleConnection;
        List<Users> ltCus;
        Users util;

        AdminOpr adminOpr;
        public Admin_Opr_Reader()
        {
            InitializeComponent();
        }

        public Admin_Opr_Reader(AdminOpr adminOpr)
        {
            this.adminOpr = adminOpr;
            InitializeComponent();
            oracleConnection = new OracleConnection(connectionString);
            ltCus = new List<Users>();
            Update();
        }
        public void Update()
        {

            if (oracleConnection == null)
                oracleConnection = new OracleConnection(connectionString);
            DataTable dt = new DataTable("table1");
            try
            {
                oracleConnection.Open();
                string cmdStr = @"select * from reader ";
                OracleCommand command = new OracleCommand(cmdStr, oracleConnection);
                OracleDataAdapter adapter = new OracleDataAdapter(command);
                adapter.Fill(dt);
                adapter.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                oracleConnection.Close();
            }
            listView.ItemsSource = null;
            ltCus.Clear();
            foreach (DataRow dataRow in dt.Rows)
            {
                Users util = new Users
                {
                    Id = dataRow["reader_id"].ToString(),
                    Name = dataRow["reader_name"].ToString(),
                    Sex = dataRow["sex"].ToString(),
                    Count = ltCus.Count + 1,
                    Phone = dataRow["telephone"].ToString(),
                    Email = dataRow["email"].ToString(),
                    Password = dataRow["password"].ToString()
                };

                ltCus.Add(util);
            }
            listView.ItemsSource = ltCus;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)  //添加
        {
            User_register user_Register = new User_register();
            Application.Current.MainWindow = user_Register;
            user_Register.IsVisibleChanged += User_Register_IsVisibleChanged;
            user_Register.Show();

            button1.IsEnabled = false;
            button2.IsEnabled = false;
        }

        private void User_Register_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Update();
        }

        private void listView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            util = (Users)listView.SelectedItem;
            button1.IsEnabled = true;
            button2.IsEnabled = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)  //编辑
        {
            User_register user_Register = new User_register(util);
            Application.Current.MainWindow = user_Register;
            user_Register.Title = "修改用户";

            user_Register.IsVisibleChanged += User_Register_IsVisibleChanged;
            user_Register.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) //查询
        {
            DataTable dt = new DataTable("table1");
            try
            {
                oracleConnection.Open();
                OracleCommand oracleCommand = oracleConnection.CreateCommand();
                string cmdStr = @"select * from reader where reader_name='" + name.Text + "'";
                OracleCommand command = new OracleCommand(cmdStr, oracleConnection);
                OracleDataAdapter adapter = new OracleDataAdapter(command);
                adapter.Fill(dt);
                adapter.Dispose();
                listView.ItemsSource = null;
                ltCus.Clear();
                foreach (DataRow dataRow in dt.Rows)
                {
                    Users util = new Users
                    {
                        Id = dataRow["reader_id"].ToString(),
                        Name = dataRow["reader_name"].ToString(),
                        Sex = dataRow["sex"].ToString(),
                        Count = ltCus.Count + 1,
                        Phone = dataRow["telephone"].ToString(),
                        Email = dataRow["email"].ToString(),
                        Password = dataRow["password"].ToString()
                    };

                    ltCus.Add(util);
                }
                listView.ItemsSource = ltCus;
            
            }
            catch
            {
                MessageBox.Show("查询失败");
            }
            finally
            {
                oracleConnection.Close();

            }

            button1.IsEnabled = false;
            button2.IsEnabled = false;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e) //删除
        {
            int result = 0;
            try
            {
                oracleConnection.Open();
                OracleCommand oracleCommand = oracleConnection.CreateCommand();
                string cmdStr = @"delete from reader WHERE reader_id='" + util.Id + "'";
                //MessageBox.Show(cmdStr);
                oracleCommand.CommandText = cmdStr;
                result = oracleCommand.ExecuteNonQuery();
            }
            catch 
            {
                //MessageBox.Show(ee.ToString());
                MessageBox.Show("删除失败!");
            }
            finally
            {
                oracleConnection.Close();
            }
            listView.ItemsSource = null;
            Update();
            if (result == 1)
            {
                MessageBox.Show("删除成功！");
            }
            button1.IsEnabled = false;
            button2.IsEnabled = false;
        }
    }



    public class Users
    {
        public int Count { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }

}
