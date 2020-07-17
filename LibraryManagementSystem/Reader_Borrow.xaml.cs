using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Student_Borrow.xaml 的交互逻辑
    /// </summary>
    public partial class Student_Borrow : Window
    {
        string connectionString = "Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS" +
            " = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)))" +
            " (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = orcl)));User ID = C##liang; Password=liang;";

        OracleConnection oracleConnection;
        List<BookUtil> ltCus;
        BookUtil borrowUtil;
        User user;
        public Student_Borrow()
        {
            InitializeComponent();
        }

        public Student_Borrow(User user)
        {
            InitializeComponent();
            oracleConnection = new OracleConnection(connectionString);
            ltCus = new List<BookUtil>();
            button.IsEnabled = false;
            this.user = user;

            DataTable dt = new DataTable("table1");
            oracleConnection.Open();
            _ = oracleConnection.CreateCommand();
            string cmdStr = @"select * from book ";
            OracleCommand command = new OracleCommand(cmdStr, oracleConnection);
            OracleDataAdapter adapter = new OracleDataAdapter(command);
            adapter.Fill(dt);
            adapter.Dispose();
            listView.ItemsSource = null;
            oracleConnection.Close();
            ltCus.Clear();
            foreach (DataRow dataRow in dt.Rows)
            {
                BookUtil bookUtil = new BookUtil
                {
                    Id = dataRow["BOOK_ID"].ToString(),
                    Name = dataRow["BOOK_NAME"].ToString(),
                    Writer = dataRow["book_writer"].ToString(),
                    Price = dataRow["book_price"].ToString(),
                    Count = ltCus.Count + 1,
                    Number = dataRow["book_count"].ToString(),
                    PublicTime = dataRow["publish_time"].ToString(),
                    Surplus = dataRow["book_surplus"].ToString()
                };

                ltCus.Add(bookUtil);
            }
            listView.ItemsSource = ltCus;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //查找图书
        {
            DataTable dt = new DataTable("table1");
            try
            {
                oracleConnection.Open();
                OracleCommand oracleCommand = oracleConnection.CreateCommand();
                string cmdStr = @"select * from book where book_name='" + name.Text + "'";
                OracleCommand command = new OracleCommand(cmdStr, oracleConnection);
                OracleDataAdapter adapter = new OracleDataAdapter(command);
                adapter.Fill(dt);
                adapter.Dispose();
                listView.ItemsSource = null;
                oracleConnection.Close();
                ltCus.Clear();
                foreach (DataRow dataRow in dt.Rows)
                {
                    BookUtil bookUtil = new BookUtil();
                    bookUtil.Id = dataRow["BOOK_ID"].ToString();
                    bookUtil.Name = dataRow["BOOK_NAME"].ToString();
                    bookUtil.Writer = dataRow["book_writer"].ToString();
                    bookUtil.Price = dataRow["book_price"].ToString();
                    bookUtil.Count = ltCus.Count + 1;
                    bookUtil.Number =  dataRow["book_count"].ToString();
                    bookUtil.PublicTime = dataRow["publish_time"].ToString();
                    bookUtil.Surplus = dataRow["book_surplus"].ToString();
                    ltCus.Add(bookUtil);
                }
                listView.ItemsSource = ltCus;
            }
            catch 
            {
                MessageBox.Show("查询失败");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)  //借阅
        {
            if (Convert.ToInt32(borrowUtil.Surplus) <= 0)
            {
                //MessageBox.Show(borrowUtil.Surplus);
                MessageBox.Show("该书已经全部借阅出去了");

            }
            else
            {
                int result = 0;
                try
                {
                    oracleConnection.Open();
                    OracleCommand oracleCommand = oracleConnection.CreateCommand();
                    string cmdStr = @"insert into borrow values('" + user.Id + "' ,'" + borrowUtil.Id
                        + "',to_date('" + DateTime.Now.ToString("yyyy-MM-dd")
                        + "','yyyy-mm-dd'),to_date('" + DateTime.Now.AddDays(30).ToString("yyyy-MM-dd") + "','yyyy-mm-dd'))";
                    oracleCommand.CommandText = cmdStr;
                    result = oracleCommand.ExecuteNonQuery();

                    DataTable dt = new DataTable("table1");
                    cmdStr = @"select * from book ";
                    OracleCommand command = new OracleCommand(cmdStr, oracleConnection);
                    OracleDataAdapter adapter = new OracleDataAdapter(command);
                    adapter.Fill(dt);
                    adapter.Dispose();
                    listView.ItemsSource = null;
                    ltCus.Clear();
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        BookUtil bookUtil = new BookUtil();
                        bookUtil.Id = dataRow["BOOK_ID"].ToString();
                        bookUtil.Name = dataRow["BOOK_NAME"].ToString();
                        bookUtil.Writer = dataRow["book_writer"].ToString();
                        bookUtil.Price = dataRow["book_price"].ToString();
                        bookUtil.Count = ltCus.Count + 1;
                        bookUtil.Number = dataRow["book_count"].ToString();
                        bookUtil.PublicTime = dataRow["publish_time"].ToString();
                        bookUtil.Surplus = dataRow["book_surplus"].ToString();
                        ltCus.Add(bookUtil);
                    }
                    listView.ItemsSource = ltCus;

                }
                catch
                {
                    MessageBox.Show("借阅失败！");
                }
                finally
                {
                    oracleConnection.Close();

                }
                if (result == 1)
                {
                    MessageBox.Show("借阅成功！");
                }
                button.IsEnabled = false;
            }

        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            button.IsEnabled = true;
            borrowUtil = (BookUtil)listView.SelectedItem;
        }
    }

    public class BookUtil
    {
        public int Count { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Writer { get; set; }
        public string Price { get; set; }
        public string PublicTime { get; set; }
        public string Number { get; set; }
        public string Surplus { get; set; }
    }
}
