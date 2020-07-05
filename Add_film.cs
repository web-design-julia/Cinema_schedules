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
    public partial class Add_film : Form
    {
        SqlConnection r = Connect.Get_con();
        public Add_film()
        {
            InitializeComponent();
            
            SqlCommand sqlcom = new SqlCommand("select * from Format ",r);
            if (r.State == ConnectionState.Closed) r.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcom);
            
            
            DataSet ds = new DataSet();
            sda.Fill(ds);
            for (int i=0; i<ds.Tables[0].Rows.Count; i++)
            {
                comboBox1.Items.Add(ds.Tables[0].Rows[i][1]);

            }
            SqlCommand sqlcom1 = new SqlCommand("select * from Age_limit ", r);
            SqlDataAdapter sda1 = new SqlDataAdapter(sqlcom1);
            DataSet ds1 = new DataSet();
            sda1.Fill(ds1);
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                 comboBox2.Items.Add( ds1.Tables[0].Rows[i][1]);
                
            }
            
            SqlCommand sqlcom3 = new SqlCommand("select * from Genre ", r);
            SqlDataAdapter sda3 = new SqlDataAdapter(sqlcom3);
            DataSet ds3 = new DataSet();
            sda3.Fill(ds3);
            for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
            {
                comboBox4.Items.Add( ds3.Tables[0].Rows[i][0] );
            }

            

        }

        private void Add_fil_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" | comboBox1.Items[comboBox1.SelectedIndex].ToString() != "" | comboBox2.Items[comboBox2.SelectedIndex].ToString() != "" |
                    textBox2.Text != ""  | comboBox4.Items[comboBox1.SelectedIndex].ToString() !="" | richTextBox1.Text!="")
                {
                    if (r.State == ConnectionState.Closed) r.Open();

                    SqlCommand command = new SqlCommand("Add_film", r);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // параметр для ввода имени
                    SqlParameter name_ = new SqlParameter
                    {
                        ParameterName = "@Name",
                        Value = textBox1.Text
                    };
                    command.Parameters.Add(name_);
                    SqlParameter format_ = new SqlParameter
                    {
                        ParameterName = "@Format",
                        Value = comboBox1.Items[comboBox1.SelectedIndex].ToString()
                    };
                    command.Parameters.Add(format_);
                    SqlParameter age_limit_ = new SqlParameter
                    {
                        ParameterName = "@Age_limit",
                        Value = comboBox2.Items[comboBox2.SelectedIndex].ToString()
                    };
                    command.Parameters.Add(age_limit_);
                    SqlParameter duraction_ = new SqlParameter
                    {
                        ParameterName = "@Duraction",
                        Value = Convert.ToInt32(textBox2.Text)
                    };
                    command.Parameters.Add(duraction_);

                    SqlParameter genre_ = new SqlParameter
                    {
                        ParameterName = "@Genre",
                        Value = comboBox4.Items[comboBox1.SelectedIndex].ToString()
                    };command.Parameters.Add(genre_);
                    SqlParameter release_ = new SqlParameter
                    {
                        ParameterName = "@Release",
                        Value = this.dateTimePicker1.Value.ToShortDateString()
                    };
                    command.Parameters.Add(release_);

                    SqlParameter descr_ = new SqlParameter
                    {
                        ParameterName = "@Describe",
                        Value = richTextBox1.Text
                    };
                    command.Parameters.Add(descr_);


                    var result = command.ExecuteNonQuery();
                    
                    MessageBox.Show("Фильм добавлен");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    comboBox1.Text = "";
                    comboBox2.Text = "";
                    comboBox4.Text = "";
                    richTextBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("Не все параметры для данного фильма выбраны");
                }
                             
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось добавить фильм. Проверьте данные");
            }
            finally
            {
                r.Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Admin_films q = new Admin_films();
            q.Show();
            Close();
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            



        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
