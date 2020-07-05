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
    public partial class Correct_film : Form
    {
        SqlConnection r = Connect.Get_con();

        Genre g = new Genre();

        
        public Correct_film()
        {
            InitializeComponent();
            SqlCommand sqlcom = new SqlCommand("select * from Format ", r);
            r.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcom);

            DataSet ds = new DataSet();
            sda.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                comboBox1.Items.Add(ds.Tables[0].Rows[i][1]);

            }
            /*
            sqlcom = new SqlCommand("select * from Genre ", r);

            sda = new SqlDataAdapter(sqlcom);

            DataSet ds1 = new DataSet();
            sda.Fill(ds1);
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                comboBox3.Items.Add(ds1.Tables[0].Rows[i][1]);//1


            }
            */
            SqlCommand sqlcom1 = new SqlCommand("select * from Age_limit ", r);
            SqlDataAdapter sda1 = new SqlDataAdapter(sqlcom1);
            DataSet ds2 = new DataSet();
            sda1.Fill(ds2);
            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            {
                comboBox2.Items.Add(ds2.Tables[0].Rows[i][1]);

            }
            SqlCommand sqlcom3 = new SqlCommand("select * from Genre ", r);
            SqlDataAdapter sda3 = new SqlDataAdapter(sqlcom3);
            DataSet ds3 = new DataSet();
            sda3.Fill(ds3);
            for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
            {
                comboBox3.Items.Add(ds3.Tables[0].Rows[i][0]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Admin_films q = new Admin_films();
            Hide();
            q.Show();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Update_film();
                

                
        }
        

        private void Update_data(object sender, EventArgs e)
        {
            if (r.State == ConnectionState.Closed) r.Open();

            string upd = null;
            string choose = "Обновить данные: \n";
            if (checkBox1.Checked)
            {
                choose += "  " + label2.Text + ", \n";
               
            }
            if (checkBox2.Checked) choose += "  " + label3.Text + ", \n";
            if (checkBox3.Checked) choose += "  " + label4.Text + ", \n";
            if (checkBox4.Checked) choose += "  " + label5.Text + ", \n";
            if (checkBox6.Checked) choose += "  " + label9.Text + ", \n";
            if (checkBox7.Checked) choose += "  " + label7.Text + ", \n";
            if (checkBox8.Checked) choose += "  " + label8.Text + ", \n";
            choose += "?";
            
            DialogResult result = MessageBox.Show(
                    choose,
                    "Сообщение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);

            if (result == DialogResult.Yes)
            {
                SqlCommand command1=null;
                string error = "Проверьте данные в следующих полях: \n";
                
                try
                {
                    
                    if (checkBox1.Checked)
                    {
                        command1 = new SqlCommand(@"Update Film set Name=@name where FilmID=@id", r);
                        command1.Parameters.AddWithValue("@id", label11.Text);
                        command1.Parameters.AddWithValue("@name", textBox1.Text);
                        command1.ExecuteNonQuery();
                    }
                }
                catch
                {
                    error+="  "+label2.Text+", \n";
                }
                
                try
                {
                    if (checkBox2.Checked)
                    {
                        command1 = new SqlCommand(@"Update Film set Format=(select FormatID from Format where Type=@format) where FilmID=@id", r);
                        command1.Parameters.AddWithValue("@id", label11.Text);
                        command1.Parameters.AddWithValue("@format", comboBox1.Text);
                        command1.ExecuteNonQuery();
                    }
                }
                catch
                {
                    error += "  " + label3.Text + ", \n";
                }
                try
                {
                    if (checkBox3.Checked)
                    {
                        command1 = new SqlCommand(@"Update Film set Age_limit=(select Age_limitID from Age_limit where Type=@age_limit) where FilmID=@id", r);
                        command1.Parameters.AddWithValue("@id", label11.Text);
                        command1.Parameters.AddWithValue("@age_limit", comboBox2.Text);
                        command1.ExecuteNonQuery();
                    }
                }
                catch
                {
                    error += "  " + label4.Text + ", \n";
                }
                try
                {
                    if (checkBox4.Checked)
                    {
                        command1 = new SqlCommand(@"Update Film set [Duration(minutes)]=@min where FilmID=@id", r);
                        command1.Parameters.AddWithValue("@id", label11.Text);
                        command1.Parameters.AddWithValue("@min", textBox2.Text);
                        command1.ExecuteNonQuery();
                    }
                }
                catch
                {
                    error += "  " + label5.Text + ", \n";
                }
                try
                {
                     if (checkBox7.Checked)
                        {
                            command1 = new SqlCommand(@"Update Release set Release=@rel where Film=@id", r);
                            command1.Parameters.AddWithValue("@id", label11.Text);
                            command1.Parameters.AddWithValue("@rel", dateTimePicker1.Value.ToShortDateString());
                            command1.ExecuteNonQuery();
                        }
                    
                }
                catch
                {
                    error += "  " + label7.Text + ", \n";
                }
                
                try
                {
                    if (checkBox8.Checked)
                    {
                        command1 = new SqlCommand(@"Update Description_ set [Сontent]=@con where Film=@id", r);
                        command1.Parameters.AddWithValue("@id", label11.Text);
                        command1.Parameters.AddWithValue("@con", richTextBox1.Text);
                        command1.ExecuteNonQuery();
                    }

                }
                catch
                {
                    error += "  " + label8.Text + ", \n";
                }
                if (checkBox6.Checked)
                {
                    try
                    {
                        SqlCommand sqlcom1 = new SqlCommand("select * from Genre_d where Film=@film ", r);
                        sqlcom1.Parameters.AddWithValue("@film", Convert.ToInt32(label11.Text));
                        SqlDataAdapter sda1 = new SqlDataAdapter(sqlcom1);

                        DataSet ds1 = new DataSet();
                        sda1.Fill(ds1);
                        int check = -1;
                        for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                        {
                            int id = Convert.ToInt32(ds1.Tables[0].Rows[j][0].ToString());
                            check = g.tmp.FindIndex(x => x.ID == id);

                            if (check == -1)
                            {
                                try
                                {
                                    command1 = new SqlCommand(@"Delete Genre_d where Genre=@id and Film=@film", r);
                                    command1.Parameters.AddWithValue("@id", id);
                                    command1.Parameters.AddWithValue("@film", label11.Text);
                                    command1.ExecuteNonQuery();
                                }
                                catch
                                {
                                    MessageBox.Show("Удалить Жанр с ID=" + g.tmp[check].Name + " невозможно");
                                }
                            }
                            else
                            {
                                command1 = new SqlCommand(@"Update Genre_d set Genre=(select GenreID from Genre where Name=@name) where Genre=@lastid and Film=@film", r);
                                command1.Parameters.AddWithValue("@lastid", id);
                                command1.Parameters.AddWithValue("@name", g.tmp[check].Name);
                                command1.Parameters.AddWithValue("@film", label11.Text);
                                command1.ExecuteNonQuery();

                            }
                            check = -1;

                        }
                        for (int i = 0; i < g.tmp.Count; i++)
                        {
                            if (g.tmp[i].state == 0)
                            {
                                command1 = new SqlCommand(@"Insert into Genre_d(Genre,Film) values ((select GenreID from Genre where Name=@name), @film)", r);
                                command1.Parameters.AddWithValue("@name", g.tmp[i].Name);
                                command1.Parameters.AddWithValue("@film", label11.Text);
                                command1.ExecuteNonQuery();
                            }
                        }
                    }
                    catch
                    {
                        error += "  " + label9.Text + ", \n";
                    }
                }

                if (error== "Проверьте данные в следующих полях: \n") MessageBox.Show("Данные обновились");
                else MessageBox.Show(error+"Остальные данные обновились");

                Update_film();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                foreach (ListViewItem i in listView1.SelectedItems)
                {
                    if (i.Selected)
                    {
                        comboBox3.Text = i.Text;

                    }
                }
            }
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            int flag = 0;
            for (int i=0; i<g.tmp.Count; i++)
            {
                if (g.tmp[i].Name == comboBox3.Text) flag = 1;
            }
            if (flag == 1) MessageBox.Show("Данный жанр уже есть");
            else
            {
                g.Add(comboBox3.Text);
                listView1.Items.Clear();
                for (int i = 0; i < g.tmp.Count; i++)
                {
                    ListViewItem it1 = new ListViewItem(g.tmp[i].Name);
                    listView1.Items.Add(it1);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int numberIndex = g.tmp.FindIndex(x => x.Name == comboBox3.Text);
            g.tmp.RemoveAt(numberIndex);
            listView1.Items.Clear();
            for (int i = 0; i < g.tmp.Count; i++)
            {
                ListViewItem it = new ListViewItem(g.tmp[i].Name);
                listView1.Items.Add(it);
            }
        }

        public void Update_film()
        {
            listView1.Items.Clear();
            g.tmp.Clear();
            string sqlcmd = @"select Name,[Duration(minutes)] from Film where FilmID=@a ";
            if (r.State == ConnectionState.Closed) r.Open();


            SqlCommand command = new SqlCommand(sqlcmd, r);
            command.Parameters.AddWithValue("@a", label11.Text);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (!reader.IsDBNull(0)) label22.Text = reader.GetString(0);

                if (!reader.IsDBNull(1)) label14.Text = reader.GetInt32(1).ToString();
            }
            reader.Close();
            sqlcmd = @"select Format.Type from Format where Format.FormatID=(select Format from Film where FilmID=@a) ";
            if (r.State == ConnectionState.Closed) r.Open();

            command = new SqlCommand(sqlcmd, r);
            command.Parameters.AddWithValue("@a", label11.Text);
            object format = command.ExecuteScalar();
            label12.Text = format.ToString();

            sqlcmd = @"select Age_limit.Type from Age_limit where Age_limitID=(select  Age_limit from Film where FilmID=@a) ";

            command = new SqlCommand(sqlcmd, r);
            command.Parameters.AddWithValue("@a", label11.Text);
            object age = command.ExecuteScalar();
            label16.Text = age.ToString();



            sqlcmd = @"select Release from Release where Film=@a ";
            if (r.State == ConnectionState.Closed) r.Open();

            command = new SqlCommand(sqlcmd, r);
            command.Parameters.AddWithValue("@a", label11.Text);
            object time = command.ExecuteScalar();
            if (time != null) label24.Text = time.ToString();

            sqlcmd = @"select [Сontent]  from [Description_] where Film=@a ";
            if (r.State == ConnectionState.Closed) r.Open();

            command = new SqlCommand(sqlcmd, r);
            command.Parameters.AddWithValue("@a", label11.Text);
            object direc = command.ExecuteScalar();
            if (direc != null) richTextBox2.Text = direc.ToString();


            sqlcmd = @"Select GenreID, Genre.Name from Genre_d join Genre on GenreID=Genre where Film=@film ";
            if (r.State == ConnectionState.Closed) r.Open();
            command = new SqlCommand(sqlcmd, r);
            command.Parameters.AddWithValue("@film", Convert.ToInt32(label11.Text));
            string genre = null;
            try
            {
                command.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i < 3) genre += "" + ds.Tables[0].Rows[i][1].ToString() + ", ";
                    g.Add_G_id(Convert.ToInt32(ds.Tables[0].Rows[i][0].ToString()), ds.Tables[0].Rows[i][1].ToString());


                }
                label18.Text = genre;
                for (int i = 0; i < g.tmp.Count; i++)
                {
                    ListViewItem it1 = new ListViewItem(g.tmp[i].Name);
                    listView1.Items.Add(it1);
                }


            }
            catch
            {
                MessageBox.Show("Произошла ошибка");
                {
                }
            }
        }
    }
}


public class Genre
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int state { get; set; }

    public List<Genre> tmp = new List<Genre>();

    public void Add( string T) //данные не из базы
    {
        tmp.Add(new Genre() { state = 0, ID = -1, Name = T});

    }
    
    public void Add_G_id(int ID, string N)
    {
        tmp.Add(new Genre() { state = 1, ID = ID, Name = N });

    }

}