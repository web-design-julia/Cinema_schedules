using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Connection;
using System.Data.SqlClient;
namespace WindowsFormsApplication
{
    public partial class Admin_cinema : Form
    {
        SqlConnection r = Connect.Get_con();
        public Admin_cinema()
        {
            InitializeComponent();
        }

        private void Add_cinema_Click(object sender, EventArgs e)
        {
            Add_cinema q = new Add_cinema();
            this.Hide();
            q.Show();
            Close();

        }

        private void Update_cinema(object sender, EventArgs e)
        {
            Del_Edit_cinema q = new Del_Edit_cinema();
            
            q.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            Edit_Schedule q = new Edit_Schedule();
            q.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AuthForms q = new AuthForms();
            q.Show();
            if (r.State == ConnectionState.Open) r.Close();
            Close();
        }
    }
}
