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
    public partial class Edit_Schedule : Form
    {
        SqlConnection r = Connect.Get_con();
        public Edit_Schedule()
        {

            InitializeComponent();
            listView1.FullRowSelect = true;
            listView2.FullRowSelect = true;
            dateTimePicker1.CustomFormat = "dd   MMMM  yyyy г. ";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;



            try
            {
                if (r.State == ConnectionState.Closed) r.Open();
                string query = @"Select Cinema.CinemaID, Cinema.Name, City.Name as City, Street,Home,Building from Cinema Join  City on City.City_Index=Cinema.City";
                
                SqlCommand sqlcom = new SqlCommand(query,r);

                SqlDataAdapter sda = new SqlDataAdapter(sqlcom);

                DataSet ds = new DataSet();
                sda.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ListViewItem it1 = new ListViewItem(ds.Tables[0].Rows[i][0].ToString());
                    it1.SubItems.Add(ds.Tables[0].Rows[i][1].ToString());
                    it1.SubItems.Add(ds.Tables[0].Rows[i][2].ToString());
                    it1.SubItems.Add(ds.Tables[0].Rows[i][3].ToString());
                    it1.SubItems.Add(ds.Tables[0].Rows[i][4].ToString());
                    it1.SubItems.Add(ds.Tables[0].Rows[i][5].ToString());


                    listView1.Items.Add(it1);
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
            
            try
            {
                if (r.State == ConnectionState.Closed) r.Open();
                SqlCommand sqlcom1 = new SqlCommand("select Name from Film ", r);
                SqlDataAdapter sda1 = new SqlDataAdapter(sqlcom1);

                DataSet ds1 = new DataSet();
                sda1.Fill(ds1);
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    comboBox1.Items.Add(ds1.Tables[0].Rows[i][0]);

                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }

            


        }

        private void Choose(object sender, EventArgs e)
        {
            try
            {
                comboBox2.Items.Clear();
                textBox3.Text = "";
                listView2.Items.Clear();
                if (r.State == ConnectionState.Closed) r.Open();
                string id = null;
                foreach (ListViewItem i in listView1.SelectedItems)
                {
                    if (i.Selected)
                    {
                        id = i.Text;

                    }
                }
                string sqlExpression = @"Select Name from Cinema where Cinema.CinemaID=@id";
                SqlCommand command = new SqlCommand(sqlExpression, r);
                command.Parameters.AddWithValue("@id", id);
                label6.Text = command.ExecuteScalar().ToString();
                try
                {
                    if (r.State == ConnectionState.Closed) r.Open();
                    SqlCommand sqlcom1 = new SqlCommand("select Name from Hall where Cinema=(select CinemaID from Cinema where Name=@a) ", r);
                    sqlcom1.Parameters.AddWithValue("@a", label6.Text);
                    SqlDataAdapter sda1 = new SqlDataAdapter(sqlcom1);

                    DataSet ds1 = new DataSet();
                    sda1.Fill(ds1);
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        comboBox2.Items.Add(ds1.Tables[0].Rows[i][0]);

                    }
                    comboBox1.Text = "";
                    comboBox2.Text = "";
                    textBox3.Text = "";
                    listView2.Items.Clear();
                    
                    Update();
                }
                catch
                {
                    MessageBox.Show("Ошибка");
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Выберите кинотеатр");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            Update();
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.Items.Count > 0)
            {
                foreach (ListViewItem i in listView2.SelectedItems)
                {
                    if (i.Selected)
                    {
                        comboBox1.Text = i.Text;
                        comboBox2.Text = i.SubItems[1].Text;
                        textBox3.Text= i.SubItems[2].Text;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                SqlCommand command1;
                string a = "" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day + "";


                command1 = new SqlCommand(@"INSERT INTO [dbo].[Schedule]([Cinema],[Hall],[Film],[Time],[Date]) VALUES((select CinemaID from Cinema where Name=@cinema),(select HallID from Hall where Name=@hall and Cinema=(select CinemaID from Cinema where Name=@cinema)), (select FilmID from Film where Name=@film), @time,@date) ", r);
                command1.Parameters.AddWithValue("@cinema", label6.Text);
                command1.Parameters.AddWithValue("@hall", comboBox2.Text);
                command1.Parameters.AddWithValue("@film", comboBox1.Text);
                command1.Parameters.AddWithValue("@time", textBox3.Text);
                command1.Parameters.AddWithValue("@date", a);
                command1.ExecuteNonQuery();

                comboBox1.Text = "";
                comboBox2.Text = "";
                textBox3.Text = "";
                listView2.Items.Clear();
                Update();


            }
            catch
            {
                MessageBox.Show("Проверьте введенные данные. Фильм и время показа не соответсвуют возрастным ограничениям или наложение времени показа фильма");
            }


        }

        private void del(object sender, EventArgs e)
        {
            Update();
            string f = null;
            string h = null;
            string t = null;
            foreach (ListViewItem i in listView2.SelectedItems)
            {
                if (i.Selected)
                {
                    f = i.Text;
                    h = i.SubItems[1].Text;
                    t = i.SubItems[2].Text;
                }
            }

            string a = "" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day + "";

            SqlCommand command1 = new SqlCommand(@"Delete from Schedule where Cinema=(select CinemaID from Cinema where Name=@cinema) and Hall=(select HallID from Hall where Name=@hall) and Film=(select FilmID from Film where Name=@film) and Time=@time and Date=@date", r);
            command1.Parameters.AddWithValue("@cinema", label6.Text);
            command1.Parameters.AddWithValue("@hall", h);
            command1.Parameters.AddWithValue("@film", f);
            command1.Parameters.AddWithValue("@time", textBox3.Text);
            command1.Parameters.AddWithValue("@date", a);
            command1.ExecuteNonQuery();
            listView2.Items.Clear();
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox3.Text = "";
            Update();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
            Update();
            string f = null;
            string h = null;
            string t = null;
            string a = "" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day + "";
            foreach (ListViewItem i in listView2.SelectedItems)
            {
                if (i.Selected)
                {
                    f = i.Text;
                    h = i.SubItems[1].Text;
                    t = i.SubItems[2].Text;
                }
            }
            try
            {
                if (f != comboBox1.Text)
                {
                    string q = @"Update Schedule set Film=(select FilmID from Film where Name=@film) where Cinema=(select CinemaID from Cinema where Name=@id) and Film=(select FilmID from Film where Name=@f)
and Hall=(select HallID from Hall where Name=@h) and Time=@t and Date=@a";
                    if (r.State == ConnectionState.Closed) r.Open();

                    SqlCommand sqlcom = new SqlCommand(q, r);
                    sqlcom.Parameters.AddWithValue("@film", comboBox1.Text);
                    sqlcom.Parameters.AddWithValue("@id", label6.Text);
                    sqlcom.Parameters.AddWithValue("@f", f);
                    sqlcom.Parameters.AddWithValue("@h", h);
                    sqlcom.Parameters.AddWithValue("@t", t);
                    sqlcom.Parameters.AddWithValue("@a", a);
                    sqlcom.ExecuteNonQuery();
                }

                if (h != comboBox2.Text)
                {
                    string q = @"Update Schedule set Hall=(select HallID from Hall where Name=@hall) where Cinema=(select CinemaID from Cinema where Name=@id) and Film=(select FilmID from Film where Name=@f)
and Hall=(select HallID from Hall where Name=@h) and Time=@t and Date=@a";
                    if (r.State == ConnectionState.Closed) r.Open();

                    SqlCommand sqlcom = new SqlCommand(q, r);
                    sqlcom.Parameters.AddWithValue("@hall", comboBox2.Text);
                    sqlcom.Parameters.AddWithValue("@id", label6.Text);
                    sqlcom.Parameters.AddWithValue("@f", f);
                    sqlcom.Parameters.AddWithValue("@h", h);
                    sqlcom.Parameters.AddWithValue("@t", t);
                    sqlcom.Parameters.AddWithValue("@a", a);
                    sqlcom.ExecuteNonQuery();
                }
                if (t != textBox3.Text)
                {
                    string q = @"Update Schedule set Time=@time where Cinema=(select CinemaID from Cinema where Name=@id) and Film=(select FilmID from Film where Name=@f)
and Hall=(select HallID from Hall where Name=@h) and Time=@t and Date=@a";
                    if (r.State == ConnectionState.Closed) r.Open();

                    SqlCommand sqlcom = new SqlCommand(q, r);
                    sqlcom.Parameters.AddWithValue("@time", textBox3.Text);
                    sqlcom.Parameters.AddWithValue("@id", label6.Text);
                    sqlcom.Parameters.AddWithValue("@f", f);
                    sqlcom.Parameters.AddWithValue("@h", h);
                    sqlcom.Parameters.AddWithValue("@t", t);
                    sqlcom.Parameters.AddWithValue("@a", a);
                    sqlcom.ExecuteNonQuery();
                }
            }
            catch
            {
                MessageBox.Show("Проверьте введенные данные");
            }
            listView2.Items.Clear();
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox3.Text = "";
            Update();
            */
        }
        
        public void Update()
        {
            string q = @"select Film.Name,Hall.Name, Schedule.Time from Schedule join Film on Film.FilmID=Schedule.Film join Hall on Schedule.Hall=Hall.HallID " +
                " where Schedule.Date=@a and (Schedule.Cinema=(select CinemaID from Cinema where Name=@id))";
            if (r.State == ConnectionState.Closed) r.Open();

            string a = "" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day + "";
            SqlCommand sqlcom = new SqlCommand(q, r);
            sqlcom.Parameters.AddWithValue("@a", a);
            sqlcom.Parameters.AddWithValue("@id", label6.Text);
            sqlcom.ExecuteNonQuery();
            SqlDataAdapter sda1 = new SqlDataAdapter(sqlcom);

            DataSet ds = new DataSet();
            sda1.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ListViewItem it1 = new ListViewItem(ds.Tables[0].Rows[i][0].ToString());
                it1.SubItems.Add(ds.Tables[0].Rows[i][1].ToString());
                it1.SubItems.Add(ds.Tables[0].Rows[i][2].ToString());

                listView2.Items.Add(it1);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
            Admin_cinema a = new Admin_cinema();
            a.Show();

        }
    }
}
