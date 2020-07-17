using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Search_Books.xaml 的交互逻辑
    /// </summary>
    public partial class Admain_Opr_Books : Window
    {
        string connectionString = "Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS" +
           " = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)))" +
           " (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = orcl)));User ID = C##liang; Password=liang;";

        OracleConnection oracleConnection;
        List<BookUtil> ltCus;
        BookUtil bookUtil;
        public Admain_Opr_Books()
        {
            InitializeComponent();
            oracleConnection = new OracleConnection(connectionString);
            ltCus = new List<BookUtil>();
            Update();
            bookUtil = new BookUtil();
        }
        public void Update()
        {
            if (oracleConnection == null)
                oracleConnection = new OracleConnection(connectionString);
            DataTable dt = new DataTable("table1");
            try
            {
                oracleConnection.Open();
                string cmdStr = @"select * from book ";

                OracleCommand command = new OracleCommand(cmdStr, oracleConnection);
                OracleDataAdapter adapter = new OracleDataAdapter(command);
                adapter.Fill(dt);
                adapter.Dispose();

                oracleConnection.Close();
            }
            catch 
            {
                //MessageBox.Show(e.ToString());

            }
            listView.ItemsSource = null;
            ltCus.Clear();
            foreach (DataRow dataRow in dt.Rows)
            {
                BookUtil util = new BookUtil
                {
                    Count = ltCus.Count + 1,
                    Id = dataRow["book_id"].ToString(),
                    Name = dataRow["book_name"].ToString(),
                    Writer = dataRow["book_writer"].ToString(),
                    PublicTime = dataRow["publish_time"].ToString(),
                    Price = dataRow["book_price"].ToString(),
                    Number = dataRow["book_count"].ToString(),
                    Surplus = dataRow["book_surplus"].ToString()
                };
                ltCus.Add(util);
            }
            listView.ItemsSource = ltCus;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)  //编辑
        {
            Adaim_add_book adaim_add_book = new Adaim_add_book(bookUtil);
            Application.Current.MainWindow = adaim_add_book;
            adaim_add_book.IsVisibleChanged += Adaim_add_book_IsVisibleChanged;
            adaim_add_book.Show();
            button1.IsEnabled = false;
            button2.IsEnabled = false;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            button1.IsEnabled = true;
            button2.IsEnabled = true;
            bookUtil = (BookUtil)listView.SelectedItem;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //查询
        {
            if (name.Text == "")
            {
                Update();
            }
            else
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
                catch
                {
                    MessageBox.Show("查询失败");
                }
            }
            button1.IsEnabled = false;
            button2.IsEnabled = false;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)  //添加
        {
            Adaim_add_book adaim_add_book = new Adaim_add_book();
            Application.Current.MainWindow = adaim_add_book;
            adaim_add_book.IsVisibleChanged += Adaim_add_book_IsVisibleChanged;
            adaim_add_book.Show();
            button1.IsEnabled = false;
            button2.IsEnabled = false;
        }

        private void Adaim_add_book_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Update();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e) //删除
        {
            int result = 0;
            try
            {
                oracleConnection.Open();
                OracleCommand oracleCommand = oracleConnection.CreateCommand();
                string cmdStr = @"delete from book WHERE book_id='" + bookUtil.Id + "'";
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
}
