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
    public partial class Del_film : Form
    {
        SqlConnection r = Connect.Get_con();
        public int d;
        public Del_film()
        {
            InitializeComponent();
            dataGridView1.SelectionMode=DataGridViewSelectionMode.FullRowSelect;

            try
            {
                if (r.State == ConnectionState.Closed) r.Open();
                string query = @"SELECT FilmID,Name from Film order by FilmID";
                SqlDataAdapter sda = new SqlDataAdapter(query, r);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось выполнить");
            }
            finally
            {
                r.Close();
            }

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            
                
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Admin_films q = new Admin_films();
            Hide();
            q.Show();
            Close();
        }

        public string Txt
        {
            get { return this.textBox1.Text; }
            set { }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        

        private void Found_Click(object sender, EventArgs e)
        {
            if (r.State == ConnectionState.Closed) r.Open();
            if (textBox1.Text!="")
            {
                
                string query = @"SELECT FilmID,Name from Film where FilmID LIKE '"+textBox1.Text+"%'";
                SqlDataAdapter sda = new SqlDataAdapter(query, r);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else if (textBox2.Text != "")
            {
                string query = @"SELECT FilmID,Name from Film where Name LIKE '" + textBox2.Text + "%'";
                SqlDataAdapter sda = new SqlDataAdapter(query, r);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Заполните поле ID или Название");
                string query = @"SELECT FilmID,Name from Film order by FilmID";
                SqlDataAdapter sda = new SqlDataAdapter(query, r);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            r.Close();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void Del_film_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                    "Удалить выбранный фильм " + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "?",
                    "Сообщение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (dataGridView1.CurrentRow.Index != -1)
                    {
                        if (r.State == ConnectionState.Closed) r.Open();
                        int w = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        SqlCommand command = new SqlCommand("Delete_film", r);
                        
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter ID_Param = new SqlParameter
                        {
                            ParameterName = "@ID",
                            Value = w
                        };
                        command.Parameters.Add(ID_Param);
                        command.ExecuteNonQuery();

                        MessageBox.Show("Фильм удален");
                        string query = @"SELECT FilmID,Name from Film where Name LIKE '" + textBox2.Text + "%'";
                        SqlDataAdapter sda = new SqlDataAdapter(query, r);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridView1.DataSource = dt;

                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось выполнить");
                }
                finally
                {
                    r.Close();
                }
                
            }

            this.TopMost = true;
        }
        private void Correct_Click(object sender, EventArgs e)
        {
            Correct_film q = new Correct_film();
            
            q.Show();
            q.label11.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Close();
        }
    }
}
