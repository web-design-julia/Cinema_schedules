using System;
using System.Windows.Forms;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Connection;
namespace Connection
{
    public class Connect
    {
        private int id = -1;
        public SqlConnection connection = null;
        static string log;
        static string pass;
        public static void Save_login_pass(string login, string password)
        {
            log = login;
            pass = password;

        }

        public static SqlConnection Get_con()
        {
            string sqlcom = @"Server=LAPTOP-235OATTP\SQLEXPRESS;Database=Cinema_schedule;User Id=" + log + ";Password=" + pass + "";
            SqlConnection connection = new SqlConnection(sqlcom);
            return connection;
        }
    }
}

namespace WindowsFormsApplication
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        public void buttonLogin_Click(object sender, EventArgs e)

        {
            
        }
        
    }
}
