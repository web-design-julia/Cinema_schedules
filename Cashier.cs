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
    public partial class Cashier : Form
    {
        SqlConnection r = Connect.Get_con();
        public Cashier()
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
                
                SqlCommand sqlcom = new SqlCommand(query, r);

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
        }

        private void Choose(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            try
            {
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




            }
           catch (Exception ex)
            {
                MessageBox.Show("Выберите кинотеатр");
            }
        }

        private void Date(object sender, EventArgs e)
        {
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
                        label2.Text = i.Text;
                        label7.Text = i.SubItems[1].Text;
                        label3.Text = i.SubItems[2].Text;

                    }
                }
            }
        }

        private void Sell(object sender, EventArgs e)
        {
            int id = 0;

            foreach (ListViewItem i in listView1.SelectedItems)
            {
                if (i.Selected)
                {
                     id = Convert.ToInt32(i.Text);
                    
                }
            }
            try
            {


                string a = "" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day + "";
                if (r.State == ConnectionState.Closed) r.Open();
                string st = @"select HallID from Hall where Cinema=@id and Name=@b";
                SqlCommand command1 = new SqlCommand(st, r);
                command1.Parameters.AddWithValue("@id", id);
                command1.Parameters.AddWithValue("@b", label3.Text);
                object count = command1.ExecuteScalar();
                int id_hall = Convert.ToInt32(count.ToString());
                string num_carts = "";
                if (textBox1.Text != "") num_carts = textBox1.Text;
                else num_carts = "1234567";
               try
               {
                    int coun = Convert.ToInt32(textBox2.Text);
                    try
                    {
                        string st1 = @"INSERT INTO ticket(Number_carts_client,Time_,Date_,Hall,Cinema,Client_bithday,Film) VALUES" +
                        "(@num_carts , @time,@Date, @id_hall,@Cinema,Null, (select FilmID from Film where Name=@film))";
                        SqlCommand cm1 = new SqlCommand(st1, r);
                        cm1.Parameters.AddWithValue("@num_carts", num_carts);
                        cm1.Parameters.AddWithValue("@time", label7.Text);
                        cm1.Parameters.AddWithValue("@Date", a);
                        cm1.Parameters.AddWithValue("@id_hall", id_hall);
                        cm1.Parameters.AddWithValue("@Cinema", id);
                        cm1.Parameters.AddWithValue("@film", label2.Text);
                        if (coun < 0) MessageBox.Show("Проверьте кол-во билетов");
                        else
                        {
                            for (int i = 0; i < coun; i++)
                            {
                                cm1.ExecuteNonQuery();
                            }
                            MessageBox.Show("Продано " + coun + "  билета");
                            textBox1.Text = "";
                            textBox2.Text = "";
                            label2.Text = "Название фильма";
                            label3.Text = "Зал";
                            label7.Text = "Время";
                            Update();
                        }
                        
                        
                    }
                    catch 
                    {
                        MessageBox.Show("Номер карты клиента неверен или все билеты проданы");
                    }
                    

                }
                catch 
                {
                    MessageBox.Show("Введите количество билетов");
                }
            }
            catch 
            {
                MessageBox.Show("Выберите сеанс");
            }




            
        }

        private void Back(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AuthForms q = new AuthForms();
            q.Show();
            if (r.State == ConnectionState.Open) r.Close();
            Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        public void Update()
        {
            listView2.Items.Clear();

            if (r.State == ConnectionState.Closed) r.Open();
            string a = "" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month
                + "-" + dateTimePicker1.Value.Day + "";

            SqlCommand command = new SqlCommand("Select_Schedul", r);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            // параметр для ввода имени
            SqlParameter date_ = new SqlParameter
            {
                ParameterName = "@date",
                Value = a
            };
            command.Parameters.Add(date_);

            SqlParameter name_ = new SqlParameter
            {
                ParameterName = "@name",
                Value = label6.Text
            };
            command.Parameters.Add(name_);
            var result = command.ExecuteNonQuery();


            SqlDataAdapter sda1 = new SqlDataAdapter(command);

            DataSet ds = new DataSet();
            sda1.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ListViewItem it1 = new ListViewItem(ds.Tables[0].Rows[i][0].ToString());
                it1.SubItems.Add(ds.Tables[0].Rows[i][1].ToString());
                it1.SubItems.Add(ds.Tables[0].Rows[i][2].ToString());
                it1.SubItems.Add(ds.Tables[0].Rows[i][3].ToString());

                listView2.Items.Add(it1);
            }
        }
    }
}
