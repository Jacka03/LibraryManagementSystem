using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Student_Return.xaml 的交互逻辑
    /// </summary>
    public partial class Student_Return : Window
    {
        User user;
        string connectionString = "Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS" +
            " = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)))" +
            " (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = orcl)));User ID = C##liang; Password=liang;";

        OracleConnection oracleConnection;
        List<ReturnUtil> ltCus;
        ReturnUtil util;

        public Student_Return()
        {
            InitializeComponent();
        }

        public Student_Return(User user)
        {
            this.user = user;
            InitializeComponent();
            oracleConnection = new OracleConnection(connectionString);
            ltCus = new List<ReturnUtil>();

            Update();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void Update()
        {
            DataTable dt = new DataTable("table1");
            oracleConnection.Open();
            string cmdStr = @"select * from borrow_info WHERE READER_ID='" + user.Id + "'";
            OracleCommand command = new OracleCommand(cmdStr, oracleConnection);
            OracleDataAdapter adapter = new OracleDataAdapter(command);
            adapter.Fill(dt);
            adapter.Dispose();
            oracleConnection.Close();
            ltCus.Clear();
            foreach (DataRow dataRow in dt.Rows)
            {
                ReturnUtil util = new ReturnUtil
                {
                    Id = dataRow["BOOK_ID"].ToString(),
                    Name = dataRow["BOOK_NAME"].ToString(),
                    BorrowTime = dataRow["BORROW_TIME"].ToString(),
                    Count = ltCus.Count + 1,
                    ReturnTime = dataRow["RETURN_TIME"].ToString()
                };
                ltCus.Add(util);
            }
            listView.ItemsSource = ltCus;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            button1.IsEnabled = true;
            button2.IsEnabled = true;
            util = (ReturnUtil)listView.SelectedItem;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // 续借
        {
            int result = 0;
            try
            {
                oracleConnection.Open();
                string[] str = util.BorrowTime.Split(' ');
                OracleCommand oracleCommand = oracleConnection.CreateCommand();
                string cmdStr = @"update borrow set return_time = return_time+20   WHERE READER_ID='" +
                    user.Id + "'and book_id='" + util.Id + "' and borrow_time=to_date('" + str[0] + "','yyyy-mm-dd')";
                oracleCommand.CommandText = cmdStr;
                result = oracleCommand.ExecuteNonQuery();
                oracleConnection.Close();
            }
            catch
            {
                MessageBox.Show("续借失败！");
            }
            listView.ItemsSource = null;
            Update();
            if (result == 1)
            {
                MessageBox.Show("续借成功！");
            }
            button1.IsEnabled = false;
            button2.IsEnabled = false;
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)  //归还
        {
            int result = 0;
            try
            {
                oracleConnection.Open();
                string[] str = util.BorrowTime.Split(' ');
                OracleCommand oracleCommand = oracleConnection.CreateCommand();
                string cmdStr = @"delete from borrow  WHERE READER_ID='" + user.Id + "'and book_id='" + util.Id + "' and borrow_time=to_date('" + str[0] + "','yyyy-mm-dd')";
                oracleCommand.CommandText = cmdStr;
                result = oracleCommand.ExecuteNonQuery();
                oracleConnection.Close();
            }
            catch
            {
                MessageBox.Show("归还失败!");
            }
            listView.ItemsSource = null;
            Update();
            if (result == 1)
            {
                MessageBox.Show("归还成功！");
            }
            button1.IsEnabled = false;
            button2.IsEnabled = false;
        }
    }


    class ReturnUtil
    {
        public int Count { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string BorrowTime { get; set; }
        public string ReturnTime { get; set; }
    }
}
