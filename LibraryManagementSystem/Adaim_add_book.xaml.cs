using Oracle.ManagedDataAccess.Client;
using System;
using System.Windows;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Adaim_add_book.xaml 的交互逻辑
    /// </summary>
    public partial class Adaim_add_book : Window
    {
        string connectionString = "Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS" +
            " = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)))" +
            " (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = orcl)));User ID = C##liang; Password=liang;";
        OracleConnection oracleConnection;


        string cmdStr;
        bool flag;
        public Adaim_add_book()
        {
            InitializeComponent();
            oracleConnection = new OracleConnection(connectionString);
            //this.cmdStr = cmdStr;
            flag = true;


        }
        public Adaim_add_book(BookUtil bookUtil)
        {

            InitializeComponent();
            oracleConnection = new OracleConnection(connectionString);

            id.Text = bookUtil.Id;
            name.Text = bookUtil.Name;
            writer.Text = bookUtil.Writer;
            publishTime.Text = bookUtil.PublicTime;
            price.Text = bookUtil.Price;
            number.Text = bookUtil.Number;
            surplus.Text = bookUtil.Surplus;
            id.IsEnabled = false;
            flag = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (flag)
            {

                cmdStr = @"insert into book values('" + id.Text + "' ,'" + name.Text + "','" + writer.Text
                    + "' , to_date('" + publishTime.Text + "', 'yyyy-mm-dd') ,'" + price.Text + "' ,'" + number.Text + "','" + surplus.Text + "')";
                
            }
            else
            {
                string[] str = publishTime.Text.Split(' ');

                cmdStr = @"update book set book_name ='" + name.Text + "',book_writer='" + writer.Text + "',publish_time=to_date('"
                    + str[0] + "','yyyy-mm-dd'),book_price='" + price.Text + "',book_count='" + number.Text + "',book_surplus='"+ surplus.Text+"'  WHERE book_id='" + id.Text + "'";
            }

            int result = 0;
            try
            {
                oracleConnection.Open();
                OracleCommand oracleCommand = oracleConnection.CreateCommand();

                oracleCommand.CommandText = cmdStr;
                result = oracleCommand.ExecuteNonQuery();
            }
            catch (Exception ww)
            {
                MessageBox.Show(ww.ToString());

                if (cmdStr[2] == 'd')
                    MessageBox.Show("修改失败！");
                else
                    MessageBox.Show("添加失败！");
            }
            finally
            {
                oracleConnection.Close();
            }
            if (result == 1)
            {

                if (cmdStr[2] == 'd')
                    MessageBox.Show("修改成功！");
                else
                    MessageBox.Show("添加成功！");
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
