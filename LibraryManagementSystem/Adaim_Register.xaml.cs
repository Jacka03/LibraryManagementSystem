using System;
using System.Windows;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Adaim_Register.xaml 的交互逻辑
    /// </summary>
    public partial class Adaim_Register : Window
    {
        AdminOpr adminOpr;
        public Adaim_Register()
        {
            adminOpr = new AdminOpr();
            InitializeComponent();

        }
   

        private void Button_Click(object sender, RoutedEventArgs e)  //注册
        {
            if(adminOpr.sameAdminr(id.Text))
            {
                if(!password.Text.Equals(password1.Text))
                {
                    MessageBox.Show("两次密码不一样");
                }
                else
                {
                    adminOpr.AdminName = name.Text;
                    adminOpr.Id = id.Text;
                    //adminOpr.Sex = sex.Text;
                    adminOpr.Age = Convert.ToInt32(age.Text);
                    adminOpr.Phone = phone.Text;
                    adminOpr.Password = password.Text;
                    if (adminOpr.adminRegister(adminOpr) > 0)
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
                MessageBox.Show("该账号已经存在");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            adminOpr.Sex = "男";
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            adminOpr.Sex = "女";

        }
    }
}
