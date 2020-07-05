using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using System.Data.SqlClient;
using Connection;


namespace WindowsFormsApplication
{
    public partial class A_f_addparamers : Form
    {
        SqlConnection r = Connect.Get_con();
        public A_f_addparamers()
        {
            if (r.State == ConnectionState.Closed) r.Open();
            InitializeComponent();
            Load_Genre();
            Load_Format();
            Load_Age();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView3.Columns[1].ReadOnly = true;
            dataGridView3.Columns[2].ReadOnly = true;
            dataGridView4.Columns[1].ReadOnly = true;
             r.Close();

        }

        
        
        private void button2_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (Check_Genre(textBox1.Text) == 1) MessageBox.Show("Нельзя добавить - дублирование строк");
                else
                {
                    if (r.State == ConnectionState.Closed) r.Open();
                    textBox1.Text = textBox1.Text.Trim();
                    int id = 0;
                    SqlCommand command = new SqlCommand("Add_update_genre", r);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // параметр для ввода имени
                    SqlParameter id_ = new SqlParameter
                    {
                        ParameterName = "@ID",
                        Value = id
                    };
                    command.Parameters.Add(id_);

                    SqlParameter name_ = new SqlParameter
                    {
                        ParameterName = "@Name",
                        Value = textBox1.Text
                    };
                    command.Parameters.Add(name_);
                    var result = command.ExecuteNonQuery();

                    Load_Genre();
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

        private void Load_Genre()
        {
            try
            {
                if (r.State == ConnectionState.Closed) r.Open();
                string query = @"SELECT * from Genre order by Name";
                SqlDataAdapter sda = new SqlDataAdapter(query,r);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[1].Visible = false;

            }
            
            catch 
            {
                MessageBox.Show("Не удалось выполнить");
            }
            finally
            {
                r.Close();
            }
            
        }

        public void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if(dataGridView1.CurrentRow.Index != -1)
                {
                    int w = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    
                    
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (r.State == ConnectionState.Closed) r.Open();
                
                int id = -1;
                DynamicParameters param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@Name", textBox1.Text);
                r.Execute("Add_update_genre", param, commandType: CommandType.StoredProcedure);
                Load_Genre();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось выполнить, потому что один из фильмов ссылается на эту строчку. Вы можете только переименовать или сначала удалить фильм");
            }
            finally
            {
                r.Close();
            }
        }
        private void Upda_genre_Click(object sender, EventArgs e)
        {
            try
            {
                if (Check_Genre(textBox1.Text) == 1) MessageBox.Show("Нельзя обновить - дублирование строк");
                else
                {
                    if (r.State == ConnectionState.Closed) r.Open();
                    textBox1.Text = textBox1.Text.Trim();
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    SqlCommand command = new SqlCommand("Add_update_genre", r);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    SqlParameter id_ = new SqlParameter
                    {
                        ParameterName = "@ID",
                        Value = id
                    };
                    command.Parameters.Add(id_);

                    SqlParameter name_ = new SqlParameter
                    {
                        ParameterName = "@Name",
                        Value = textBox1.Text
                    };
                    command.Parameters.Add(name_);
                    var result = command.ExecuteNonQuery();
                    
                    Load_Genre();
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
        private void button5_Click(object sender, EventArgs e)
        {
            
        }
        

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            
        }
        private void Upd_filcom_Click(object sender, EventArgs e)
        {
            

        }

        private void Button_Age_limit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Check_Age(textBox3.Text) == 1) MessageBox.Show("Нельзя добавить - дублирование строк");
                else
                {
                    if (r.State == ConnectionState.Closed) r.Open();

                    int id = 0;
                    SqlCommand command = new SqlCommand("Add_update_Age_limit", r);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    SqlParameter id_ = new SqlParameter
                    {
                        ParameterName = "@ID",
                        Value = id
                    };
                    command.Parameters.Add(id_);

                    SqlParameter type_ = new SqlParameter
                    {
                        ParameterName = "@Type",
                        Value = textBox3.Text
                    };
                    command.Parameters.Add(type_);
                    SqlParameter from_ = new SqlParameter
                    {
                        ParameterName = "@From_",
                        Value = textBox5.Text
                    };
                    command.Parameters.Add(from_);
                    SqlParameter to_ = new SqlParameter
                    {
                        ParameterName = "@To_",
                        Value = textBox6.Text
                    };
                    command.Parameters.Add(to_);
                    var result = command.ExecuteNonQuery();
                    
                    Load_Age();
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
        private void Load_Age()
        {
            try
            {
                if (r.State == ConnectionState.Closed) r.Open();
                string query = @"SELECT * from Age_limit order by Type";
                SqlDataAdapter sda = new SqlDataAdapter(query, r);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView3.DataSource = dt;
                dataGridView3.Columns[0].Visible = false;

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

        private void dataGridView3_DoubleClick(object sender, EventArgs e)
        {
            try

            {
                if (r.State == ConnectionState.Closed) r.Open();
                if (dataGridView3.CurrentRow.Index != -1)
                {
                    int w = Convert.ToInt32(dataGridView3.CurrentRow.Cells[0].Value.ToString());
                    textBox3.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();
                    textBox5.Text = dataGridView3.CurrentRow.Cells[2].Value.ToString();
                    textBox6.Text = dataGridView3.CurrentRow.Cells[3].Value.ToString();

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
        private void Update_Age_Click(object sender, EventArgs e)
        {
            try
            {
                if (Check_Format(textBox3.Text) == 1) MessageBox.Show("Нельзя обновить - дублирование строк");
                else
                {
                    if (r.State == ConnectionState.Closed) r.Open();
                    
                    int id = Convert.ToInt32(dataGridView3.CurrentRow.Cells[0].Value.ToString());
                    SqlCommand command = new SqlCommand("Add_update_Age_limit", r);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    SqlParameter id_ = new SqlParameter
                    {
                        ParameterName = "@ID",
                        Value = id
                    };
                    command.Parameters.Add(id_);

                    SqlParameter type_ = new SqlParameter
                    {
                        ParameterName = "@Type",
                        Value = textBox3.Text
                    };
                    command.Parameters.Add(type_);
                    SqlParameter from_ = new SqlParameter
                    {
                        ParameterName = "@From_",
                        Value = textBox5.Text
                    };
                    command.Parameters.Add(from_);
                    SqlParameter to_ = new SqlParameter
                    {
                        ParameterName = "@To_",
                        Value = textBox6.Text
                    };
                    command.Parameters.Add(to_);
                    var result = command.ExecuteNonQuery();
                    Load_Age();
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

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                if (Check_Format(textBox4.Text) == 1) MessageBox.Show("Нельзя добавить - дублирование строк");
                else
                {
                    if (r.State == ConnectionState.Closed) r.Open();
                    textBox4.Text = textBox4.Text.Trim();
                    int id = 0;
                    SqlCommand command = new SqlCommand("Add_update_Format", r);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // параметр для ввода имени
                    SqlParameter id_ = new SqlParameter
                    {
                        ParameterName = "@ID",
                        Value = id
                    };
                    command.Parameters.Add(id_);

                    SqlParameter type_ = new SqlParameter
                    {
                        ParameterName = "@Type",
                        Value = textBox4.Text
                    };
                    command.Parameters.Add(type_);
                    var result = command.ExecuteNonQuery();

                    Load_Format();

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
        private void Load_Format()
        {
            try
            {
                if (r.State == ConnectionState.Closed) r.Open();
                string query = @"SELECT * from Format order by Type";
                SqlDataAdapter sda = new SqlDataAdapter(query, r);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView4.DataSource = dt;
                dataGridView4.Columns[0].Visible = false;

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

        private void dataGridView4_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView4.CurrentRow.Index != -1)
                {
                    int w = Convert.ToInt32(dataGridView4.CurrentRow.Cells[0].Value.ToString());
                    textBox4.Text = dataGridView4.CurrentRow.Cells[1].Value.ToString();

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

        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
            Admin_films a = new Admin_films();
            a.Show();
            
        }

        private void Update_Format_Click(object sender, EventArgs e)
        {
            try
            {
                if (Check_Format(textBox4.Text) == 1) MessageBox.Show("Нельзя обновить - дублирование строк");
                else
                {
                    if (r.State == ConnectionState.Closed) r.Open();
                    //var txtName = textBox1.Text;
                    int id = Convert.ToInt32(dataGridView4.CurrentRow.Cells[0].Value.ToString());
                    SqlCommand command = new SqlCommand("Add_update_Format", r);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // параметр для ввода имени
                    SqlParameter id_ = new SqlParameter
                    {
                        ParameterName = "@ID",
                        Value = id
                    };
                    command.Parameters.Add(id_);

                    SqlParameter type_ = new SqlParameter
                    {
                        ParameterName = "@Type",
                        Value = textBox4.Text
                    };
                    command.Parameters.Add(type_);
                    var result = command.ExecuteNonQuery();
                    Load_Format();
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

        private void Delet_kinocom_Click(object sender, EventArgs e)
        {
            
        }

        private void Delete_Age_Click(object sender, EventArgs e)
        {
            try
            {
                if (r.State == ConnectionState.Closed) r.Open();
                //var txtName = textBox1.Text;
                int id = -1;
                DynamicParameters param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@Type", textBox3.Text);
                param.Add("@From_", textBox5.Text);
                param.Add("@To_", textBox6.Text);
                r.Execute("Add_update_Age_limit", param, commandType: CommandType.StoredProcedure);
                Load_Age();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось выполнить, потому что один из фильмов ссылается на эту строчку. Вы можете только переименовать или сначала удалить фильм");
            }
            finally
            {
                r.Close();
            }
        }

        private void Delete_for_Click(object sender, EventArgs e)
        {
            try
            {
                if (r.State == ConnectionState.Closed) r.Open();
                //var txtName = textBox1.Text;
                int id = -1;
                DynamicParameters param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@Type", textBox4.Text);
                
                r.Execute("Add_update_Format", param, commandType: CommandType.StoredProcedure);
                Load_Format();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось выполнить, потому что один из фильмов ссылается на эту строчку. Вы можете только переименовать или сначала удалить фильм");
            }
            finally
            {
                r.Close();
            }
        }

        public int Check_Genre(string name)
        {
            string check = null;
            int a = 0;
            try
            {
                
                string sqlExpression = @"Select Name from Genre where Name=@a";
                if (r.State == ConnectionState.Closed) r.Open();
                SqlCommand command = new SqlCommand(sqlExpression, r);
                command.Parameters.AddWithValue("@a", name);
                object count = command.ExecuteScalar();
                check = count.ToString();
                a = 1;
            }
            catch
            {

            }
            return a;
        }
        public int Check_Format(string name)
        {
            string check = null;
            int a = 0;
            try
            {
                string sqlExpression = @"Select Type from Format where Type=@a";
                if (r.State == ConnectionState.Closed) r.Open();
                SqlCommand command = new SqlCommand(sqlExpression, r);
                command.Parameters.AddWithValue("@a", name);
                object count = command.ExecuteScalar();
                check = count.ToString();
                a = 1;

            }
            catch
            {

            }
            return a;
        }

        public int Check_Age(string name)
        {
            string check = null;
            int a = 0;
            try
            {
                string sqlExpression = @"Select Type from Age_limit where Type=@a";
                if (r.State == ConnectionState.Closed) r.Open();
                SqlCommand command = new SqlCommand(sqlExpression, r);
                command.Parameters.AddWithValue("@a", name);
                object count = command.ExecuteScalar();
                check = count.ToString();
                a = 1;
            }
            catch
            {
                
            }
            return a;
        }

        private void A_f_addparamers_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
