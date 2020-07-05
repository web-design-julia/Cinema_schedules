using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Connection;


namespace WindowsFormsApplication
{
    public partial class AuthForms : Form
    {
        public AuthForms()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connect.Save_login_pass(textBox1.Text, textBox2.Text);
            SqlConnection con = Connect.Get_con();
            try
            {


                con.Open();
                string h = "Declare @role varchar(50) if IS_MEMBER('administrator_film') = 1 set @role = 'film' else if IS_MEMBER('administrator_cinema') = 1 " +
                    "set @role = 'cinema' else if IS_MEMBER('kassir') = 1 set @role = 'kassir' else set @role = 'Not' SELECT @role ";
                SqlCommand command = new SqlCommand(h, con);
                object count = command.ExecuteScalar();

                if (count.ToString() == "film")
                {
                    Admin_films film = new Admin_films();
                    Hide();
                    film.Show();
                }
                else if (count.ToString() == "cinema")
                {
                    Admin_cinema cine = new Admin_cinema();
                    Hide();
                    cine.Show();
                }
                else if (count.ToString() == "kassir")
                {
                    Cashier kas = new Cashier();

                    kas.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Вы не можете войти, т.к. вам не назначена роль");
                }
            }
            catch
            {
                MessageBox.Show("Проверьте логин и пароль");
            }
        }
    }
}
