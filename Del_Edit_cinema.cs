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
    public partial class Del_Edit_cinema : Form
    {
        SqlConnection r = Connect.Get_con();
        public Del_Edit_cinema()
        {
            InitializeComponent();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            try
            {
                if (r.State == ConnectionState.Closed) r.Open();
                string query = @"SELECT CinemaID,Name from Cinema order by CinemaID";
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

        private void Found_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {

                string query = @"SELECT CinemaID,Name from Cinema where CinemaID LIKE '" + textBox1.Text + "%'";
                SqlDataAdapter sda = new SqlDataAdapter(query, r);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else if (textBox2.Text != "")
            {
                string query = @"SELECT CinemaID,Name from Cinema where Name LIKE '" + textBox2.Text + "%'";
                SqlDataAdapter sda = new SqlDataAdapter(query, r);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Заполните поле ID или Название");
                string query = @"SELECT CinemaID,Name from Cinema order by CinemaID";
                SqlDataAdapter sda = new SqlDataAdapter(query, r);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
            }
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
                //try
                //{
                    if (dataGridView1.CurrentRow.Index != -1)
                    {
                        if (r.State == ConnectionState.Closed) r.Open();

                        int w = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        SqlCommand command = new SqlCommand("Delete_cinema", r);

                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter ID_Param = new SqlParameter
                        {
                            ParameterName = "@ID",
                            Value = w
                        };
                        command.Parameters.Add(ID_Param);
                        command.ExecuteNonQuery();

                        MessageBox.Show("Кинотеатр удален");
                        string query = @"SELECT CinemaID,Name from Cinema where Name LIKE '" + textBox2.Text + "%'";
                        SqlDataAdapter sda = new SqlDataAdapter(query, r);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridView1.DataSource = dt;

                        
                    }
                    /*
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось выполнить");
                }
                finally
                {
                    r.Close();
                }
                */
            }

            this.TopMost = true;
        }

        private void Correct_Click(object sender, EventArgs e)
        {
            Update_cine q = new Update_cine();
            q.Show();
            q.label11.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Admin_cinema q = new Admin_cinema();
            Hide();
            q.Show();
            Close();
        }
    }
}
