using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManagementSystem
{
    public class AdminOpr
    {
        private string id;          //管理员编号
        private string adminName;   //管理员名字
        private string password;    //管理员密码
        private string phone;
        private int age;
        private string sex;

        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public string AdminName
        {
            get { return adminName; }
            set { adminName = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public AdminOpr()
        {

        }
        public AdminOpr(string id, string password)
        {
            this.id = id;
            this.password = password;
        }
        public AdminOpr(string id, string adminName, string password)
        {
            this.id = id;
            this.adminName = adminName;
            this.password = password;
        }




        private static string connectionString = "Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS" +
           " = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)))" +
           " (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = orcl)));User ID = C##liang; Password=liang;";
        OracleConnection oracleConnection;



        //管理员登陆
        public Boolean adminLogin()
        {
            bool flag = false;
            if (oracleConnection == null)
                oracleConnection = new OracleConnection(connectionString);
            try
            {
                oracleConnection.Open();
                //MessageBox.Show("连接成功", "提示");
                string sql = "SELECT manager_name, age,sex, telephone  FROM manager WHERE manager_id='" +
                    id + "' AND password='" + Password + "'";
                //MessageBox.Show(sql);
                OracleCommand cmd = oracleConnection.CreateCommand();
                cmd.CommandText = sql;    //在这儿写sql语句
                OracleDataReader oracleDataReader = cmd.ExecuteReader();//创建一个OracleDateReader对象
                if (oracleDataReader.Read())
                {

                    flag = true;
                    adminName = oracleDataReader["manager_name"].ToString();
                    age = Convert.ToInt32( oracleDataReader["age"]);
                    phone = oracleDataReader["telephone"].ToString();
                    sex = oracleDataReader["sex"].ToString();
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("哎呀，程序出现错误了呢，快叫程序小哥哥来修改吧(758168660@qq.com)\n" + e.ToString(), "提示");
            }
            finally
            {
                oracleConnection.Close();
            }
            return flag;
        }


        //判断用户是否已经被注册
        public Boolean sameAdminr(string id)
        {
            bool flag = false;    //判断标志
            if (oracleConnection == null)
                oracleConnection = new OracleConnection(connectionString);

            //OracleConnection oracleConnection = new OracleConnection(connectionString);
            oracleConnection.Open();
            OracleCommand oracleCommand = oracleConnection.CreateCommand();
            string sql = "SELECT count (*) FROM reader WHERE reader_id='" + id + "'";
            oracleCommand.CommandText = sql;
            int count = Convert.ToInt32(oracleCommand.ExecuteScalar());
            //MessageBox.Show(count.ToString());

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
        public int adminRegister(AdminOpr adminOpr)
        {
            int query = 0;
            if (oracleConnection == null)
                oracleConnection = new OracleConnection(connectionString);
            try
            {
                oracleConnection.Open();
                OracleCommand oracleCommand = oracleConnection.CreateCommand();

                string sql = @"INSERT INTO manager (manager_name,manager_id,age,sex,telephone,password)
                        " + " VALUES('" + adminOpr.adminName + "','" + adminOpr.id + "','" + adminOpr.age + "','" + adminOpr.sex +
                            "','" + adminOpr.phone + "','" + adminOpr.password + "')";

                oracleCommand.CommandText = sql;
                query = oracleCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                oracleConnection.Close();
            }
            //MessageBox.Show(query.ToString());
            return query;

        }



/*
        //查询用户
        public DataTable selectUser(User user)
        {
            SqlConnection sqlCon = null;
            sqlCon = DbUtil.getConnection();
            string sql = @"SELECT * FROM [user] WHERE userName like '%" + user.UserName + "%' AND email like '%" + user.Email + "%' AND phoneNumber like '%" + user.PhoneNumber + "%'";
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlCon);
            DataSet ds = new DataSet();
            sda.Fill(ds, "user");   //填充数据集，实质是填充ds中的第0个表
            DataTable dt = ds.Tables["user"];
            return dt;//返回数据中的表
        }

        //修改用户
        public int updateUser(User user)
        {
            int x;
            sqlCon.Open();
            string sql = @"UPDATE [user] set userName='" + user.UserName + "',password='" + user.Password + "',email='" + user.Email + "',phoneNumber='" + user.PhoneNumber + "'WHERE id='" + user.Id + "'";
            SqlCommand sqlCommand = new SqlCommand(sql, sqlCon);
            x = sqlCommand.ExecuteNonQuery();
            sqlCon.Close();
            return x;
        }

        //删除用户
        public int deleteUser(User user)
        {
            int x;      //删除成功返回1

            sqlCon.Open();
            string sql = @"DELETE FROM [user] WHERE id='" + user.Id + "'";
            SqlCommand sqlCommand = new SqlCommand(sql, sqlCon);
            x = sqlCommand.ExecuteNonQuery();
            sqlCon.Close();
            return x;
        }

        ////数据集跟新与数据库相一致
        //public void upGridView()
        //{
        //    sqlCon = DbUtil.getConnection();
        //    sqlCon.Open();

        //    SqlCommandBuilder builder = new SqlCommandBuilder(sda);

        //    dataTable = dataSet.Tables["user"];
        //    sda.Update(dataTable);
        //    dataTable.AcceptChanges();
        //    sqlCon.Close();
        //}
        //显示所有用户信息                                        
        public DataTable fill()
        {
            dataTable.Clear();
            string sql = @"SELECT id,userName,password,email,phoneNumber FROM [user]";

            sda = new SqlDataAdapter(sql, sqlCon);
            //填充数据集，实质是填充数据集中的第0个表
            DataSet ds = new DataSet();
            sda.Fill(ds, "user");
            DataTable dt = ds.Tables["user"];
            return dt;
        }

       
*/
    }
}
