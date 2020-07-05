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
    public partial class Admin_films : Form
    {

        SqlConnection r = Connect.Get_con();
        public Admin_films()
        {
            InitializeComponent();
        }

        private void Admin_films_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            A_f_addparamers q = new A_f_addparamers();
            q.Show();Hide();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Upd_groups_Click(object sender, EventArgs e)
        {
            
            
        }

        private void Add_film_Click(object sender, EventArgs e)
        {
            
            Add_film q = new Add_film();
            q.Show();
            Close();
        }

        private void Del_film_Click(object sender, EventArgs e)
        {
            Del_film q = new Del_film();
            q.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            AuthForms q = new AuthForms();
            q.Show();
            if (r.State == ConnectionState.Open) r.Close();
            Close();
        }
    }
}
