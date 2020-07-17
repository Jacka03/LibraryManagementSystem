using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManagementSystem
{
    // 用户类
    public class User
    {
        private string id;              //用户账号
        private string userName;        //用户名
        private string password;        //用户密码
        private string email;           //用户邮箱
        private string phoneNumber;     //用户手机号
        private string sex;             //用户性别

        public string Sex
        {
            get { return sex; }
            set { sex = value; }

        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }

        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }

        }


        public User()
        {
            oracleConnection = new OracleConnection(connectionString);

        }
        public User(string id)
        {
            this.id = id;
            oracleConnection = new OracleConnection(connectionString);

        }
        /* public User(string userName)
         {
             this.userName = userName;

         }*/
        public User(string id, string password)
        {
            this.id = id;
            this.password = password;
            oracleConnection = new OracleConnection(connectionString);

        }
        public User(string userName, string password, string phoneNumber)
        {
            this.userName = userName;
            this.password = password;
            this.phoneNumber = phoneNumber;
            oracleConnection = new OracleConnection(connectionString);

        }


        /*public User(string id, string userName, string password)
        {
            this.id = id;
            this.userName = userName;
            this.password = password;
        }*/
        public User(string userName, string password, string email, string phoneNumber)
        {
            this.userName = userName;
            this.password = password;
            this.email = email;
            this.phoneNumber = phoneNumber;
            oracleConnection = new OracleConnection(connectionString);
        }
        public User(string id, string userName, string password, string email, string phoneNumber)
        {
            this.id = id;
            this.userName = userName;
            this.password = password;
            this.email = email;
            this.phoneNumber = phoneNumber;
            oracleConnection = new OracleConnection(connectionString);
        }


        //用来显示登陆的用户信息
        private static string connectionString = "Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS" +
           " = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)))" +
           " (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = orcl)));User ID = C##liang; Password=liang;";
        OracleConnection oracleConnection;
      
        //用户登陆验证
        public bool UserLogin()
        {

            bool flag = false;
            try
            {
                oracleConnection.Open();
                string sql = "SELECT reader_name, sex,telephone, email  FROM reader WHERE reader_id='" +
                    id + "' AND password='" + Password + "'";
                OracleCommand cmd = oracleConnection.CreateCommand();
                cmd.CommandText = sql;    //在这儿写sql语句
                OracleDataReader oracleDataReader = cmd.ExecuteReader();//创建一个OracleDateReader对象
                if (oracleDataReader.Read())
                {
                    flag = true;
                    userName = oracleDataReader["reader_name"].ToString();
                    email = oracleDataReader["email"].ToString();
                    phoneNumber = oracleDataReader["telephone"].ToString();
                    sex = oracleDataReader["sex"].ToString();
                }
                else
                {
                    flag = false;
                }
            }
            catch 
            {
                //MessageBox.Show(");
            }
            finally
            {
                oracleConnection.Close();
            }
            return flag;
        }


        //判断用户是否已经被注册
        public Boolean SameUser(string id)
        {
            bool flag;    //判断标志
            oracleConnection.Open();
            OracleCommand oracleCommand = oracleConnection.CreateCommand();
            string sql = "SELECT count (*) FROM reader WHERE reader_id='" + id + "'";
            oracleCommand.CommandText = sql;
            int count = Convert.ToInt32(oracleCommand.ExecuteScalar());
            if (count > 0)
            {
                flag = false;    //表明用户已经注册
            }
            else
            {
                flag = true;
            }

            oracleConnection.Close();
            return flag;
        }

        //用户注册
        public int Register(User user)
        {
            int result = 0;
            try
            {
                oracleConnection.Open();
                OracleCommand oracleCommand = oracleConnection.CreateCommand();

                string sql = @"INSERT INTO reader (reader_name,reader_id,sex,telephone,email,password)
                        " + " VALUES('" + user.UserName + "','" + user.id + "','" + user.sex +
                            "','" + user.phoneNumber + "','" + user.email + "','" + user.password + "')";

                oracleCommand.CommandText = sql;
                result = oracleCommand.ExecuteNonQuery();
            }
            catch (Exception eee)
            {
                MessageBox.Show(eee.ToString());
            }
            finally
            {
                oracleConnection.Close();
            }
            return result;

        }


        //修改用户
        public int UpdateUser(User user)
        {
            if (oracleConnection == null)
                oracleConnection = new OracleConnection(connectionString);

            oracleConnection.Open();
            OracleCommand oracleCommand = oracleConnection.CreateCommand();

            string sql = @"UPDATE reader set userName='" + user.UserName + "',password='" + user.Password + "',email='" + user.Email + "',phoneNumber='" + user.PhoneNumber + "'WHERE id='" + user.Id + "'";

            oracleCommand.CommandText = sql;

            int result = oracleCommand.ExecuteNonQuery();

            oracleConnection.Close();

            return result;
        }
    }
}
