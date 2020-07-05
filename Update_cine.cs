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

    public partial class Update_cine : Form
    {
        SqlConnection r = Connect.Get_con();
        Hall upd_hall = new Hall();
        public Update_cine()
        {
            InitializeComponent();
            
            
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (r.State == ConnectionState.Closed) r.Open();

            string upd = null;
            string choose = "Обновить данные: \n";
            if (checkBox1.Checked)
            {
                choose += "  " + label1.Text + ", \n";

            }
            if (checkBox2.Checked) choose += "  " + label3.Text + ", \n";
            if (checkBox3.Checked) choose += "  " + label4.Text + ", "+ label7.Text+", " + label6.Text + " \n";
            if (checkBox4.Checked) choose += "  Информация о залах, \n";
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
                SqlCommand command1 = null;
                string error = "Проверьте данные в следующих полях: \n";

                try
                {

                    if (checkBox1.Checked)
                    {
                        command1 = new SqlCommand(@"Update Cinema set Name=@name where CinemaID=@id", r);
                        command1.Parameters.AddWithValue("@id", label11.Text);
                        command1.Parameters.AddWithValue("@name", textBox1.Text);
                        command1.ExecuteNonQuery();
                    }
                }
                catch
                {
                    error += "  " + label1.Text + ", \n";
                }

                try
                {

                    if (checkBox2.Checked)
                    {
                        command1 = new SqlCommand(@"Update Cinema set City=(select City_Index from City where Name=@city) where CinemaID=@id", r);
                        command1.Parameters.AddWithValue("@id", label11.Text);
                        command1.Parameters.AddWithValue("@city", comboBox1.Text);
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
                        command1 = new SqlCommand(@"Update Cinema set Street=@street, Home=@home,Building=@build  where CinemaID=@id", r);
                        command1.Parameters.AddWithValue("@id", label11.Text);
                        command1.Parameters.AddWithValue("@street", textBox2.Text);
                        command1.Parameters.AddWithValue("@home", textBox4.Text);
                        command1.Parameters.AddWithValue("@build", textBox3.Text);
                        command1.ExecuteNonQuery();
                    }
                }
                catch
                {
                    error += "  " + label2.Text + ", \n";
                }
                if (checkBox4.Checked)
                {
                    try
                    {
                        SqlCommand sqlcom1 = new SqlCommand("select * from Hall where Cinema=@cin ", r);
                        sqlcom1.Parameters.AddWithValue("@cin", Convert.ToInt32(label11.Text));
                        SqlDataAdapter sda1 = new SqlDataAdapter(sqlcom1);

                        DataSet ds1 = new DataSet();
                        sda1.Fill(ds1);
                        int check = -1;
                        for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                        {
                            int id = Convert.ToInt32(ds1.Tables[0].Rows[j][0].ToString());
                            check = upd_hall.tmp.FindIndex(x => x.id == Convert.ToInt32(ds1.Tables[0].Rows[j][0].ToString()));

                            if (check == -1)
                            {
                                try
                                {
                                    command1 = new SqlCommand(@"Delete Hall where HallID=@id and Cinema=@cin", r);
                                    command1.Parameters.AddWithValue("@id", id);
                                    command1.Parameters.AddWithValue("@cin", label11.Text);
                                    command1.ExecuteNonQuery();
                                }
                                catch
                                {
                                    MessageBox.Show("Удалить Зал с ID="+id+" невозможно");
                                }
                            }
                            else
                            {
                                command1 = new SqlCommand(@"Update Hall set Name=@name, [Seating_capacity]=@Seat where HallID=@id and Cinema=@cin", r);
                                command1.Parameters.AddWithValue("@id", id);
                                command1.Parameters.AddWithValue("@name", upd_hall.tmp[check].Name);
                                command1.Parameters.AddWithValue("@cin", label11.Text);
                                command1.Parameters.AddWithValue("@Seat", upd_hall.tmp[check].Counter);
                                command1.ExecuteNonQuery();

                            }
                            check = -1;

                        }
                        for (int i = 0; i < upd_hall.tmp.Count; i++)
                        {
                            if (upd_hall.tmp[i].state == 0)
                            {
                                command1 = new SqlCommand(@"Insert into Hall(Cinema,Name, [Seating_capacity]) values (@cin, @name,@Seat)", r);
                                command1.Parameters.AddWithValue("@name", upd_hall.tmp[i].Name);
                                command1.Parameters.AddWithValue("@cin", label11.Text);
                                command1.Parameters.AddWithValue("@Seat", upd_hall.tmp[i].Counter);
                                command1.ExecuteNonQuery();
                            }
                        }
                    }
                    catch
                    {
                        error += "  " + label8.Text + ", \n";
                    }



                }
                

                if (error == "Проверьте данные в следующих полях: \n") MessageBox.Show("Данные обновились");
                else MessageBox.Show(error + "Остальные данные обновились");
                listView1.Items.Clear();
                upd_hall.tmp.Clear();
                Update();
                this.TopMost = true;
            }
        }

        private void Choose_cine(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            upd_hall.tmp.Clear();
            listView1.Items.Clear();
           
        }

        private void Add_cont(object sender, EventArgs e)
        {
            try
            {
                int flag = 0;
                for (int i = 0; i < upd_hall.tmp.Count; i++)
                {
                    if (upd_hall.tmp[i].Name == textBox5.Text) flag = 1;
                }
                if (flag == 1) MessageBox.Show("Такой зал уже добавлен");
                else
                {
                    upd_hall.Add_H(textBox5.Text, Convert.ToInt32(textBox6.Text));


                    textBox5.Text = "";
                    textBox6.Text = "";
                    ListViewItem item = new ListViewItem();
                    listView1.Items.Clear();
                    for (int i = 0; i < upd_hall.tmp.Count; i++)
                    {
                        ListViewItem it = new ListViewItem(upd_hall.tmp[i].Name, upd_hall.tmp[i].Counter);
                        it.SubItems.Add(upd_hall.tmp[i].Counter.ToString());
                        listView1.Items.Add(it);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Проверте введенные поля");
            }
        }

        private void Edit_H(object sender, EventArgs e)
        {
            int numberIndex = upd_hall.tmp.FindIndex(x => x.Name == listView1.SelectedItems[0].Text && x.Counter == Convert.ToInt32(listView1.SelectedItems[0].SubItems[1].Text));


            if (upd_hall.tmp[numberIndex].state == 1 & textBox6.Text != "") MessageBox.Show("Вместимость этого зала изменить нельзя");
            else if (upd_hall.tmp[numberIndex].state == 1 & textBox6.Text == "")
            {
                upd_hall.tmp[numberIndex].Name = textBox5.Text;
            }
            else if (upd_hall.tmp[numberIndex].state == 0)
            {
                upd_hall.tmp[numberIndex].Name = textBox5.Text;
                upd_hall.tmp[numberIndex].Counter = Convert.ToInt32(textBox6.Text);
            }
            listView1.Items.Clear();
            for (int i = 0; i < upd_hall.tmp.Count; i++)
            {
                ListViewItem it = new ListViewItem(upd_hall.tmp[i].Name, upd_hall.tmp[i].Counter);
                it.SubItems.Add(upd_hall.tmp[i].Counter.ToString());
                listView1.Items.Add(it);
            }
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void Del_H(object sender, EventArgs e)
        {
            int numberIndex = upd_hall.tmp.FindIndex(x => x.Name == textBox5.Text && x.Counter == Convert.ToInt32(textBox6.Text));
            upd_hall.tmp.RemoveAt(numberIndex);
            listView1.Items.Clear();
            for (int i = 0; i < upd_hall.tmp.Count; i++)
            {
                ListViewItem it = new ListViewItem(upd_hall.tmp[i].Name, upd_hall.tmp[i].Counter);
                it.SubItems.Add(upd_hall.tmp[i].Counter.ToString());
                listView1.Items.Add(it);
            }
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                foreach (ListViewItem i in listView1.SelectedItems)
                {
                    if (i.Selected)
                    {
                        textBox5.Text = i.Text;
                        textBox6.Text = i.SubItems[1].Text;
                    }
                }
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

        }

        private void Add_co(object sender, EventArgs e)
        {
            
        }

        private void Del_C(object sender, EventArgs e)
        {
             
        }

       

        

        private void Edit_C(object sender, EventArgs e)
        {
            
        }

        private void Cancel(object sender, EventArgs e)
        {
            Admin_cinema q = new Admin_cinema();
            Close();
            q.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Update();
        }
        public void Update()
        {
            try
            {
                r.Open();
            }
            catch
            {

            }
            listView1.Items.Clear();
            upd_hall.tmp.Clear();
            string sqlExpression = @"Select Name from Cinema where CinemaID=@a";

            if (r.State == ConnectionState.Closed) r.Open();
            SqlCommand command = new SqlCommand(sqlExpression, r);
            command.Parameters.AddWithValue("@a", Convert.ToInt32(label11.Text));
            object count = command.ExecuteScalar();
            textBox1.Text = count.ToString();
            SqlCommand sqlcom1 = new SqlCommand("select * from City ", r);

            SqlDataAdapter sda1 = new SqlDataAdapter(sqlcom1);

            DataSet ds1 = new DataSet();
            sda1.Fill(ds1);
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                comboBox1.Items.Add(ds1.Tables[0].Rows[i][1]);

            }
            sqlExpression = @"Select Name from City where City_Index=(select City from Cinema where CinemaID =@a)";

            if (r.State == ConnectionState.Closed) r.Open();
            command = new SqlCommand(sqlExpression, r);
            command.Parameters.AddWithValue("@a", Convert.ToInt32(label11.Text));
            count = command.ExecuteScalar();
            comboBox1.Text = count.ToString();


            command = new SqlCommand("Select Street, Building, Home from Cinema where CinemaID =@b", r);
            command.Parameters.AddWithValue("@b", Convert.ToInt32(label11.Text));
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (!reader.IsDBNull(0)) textBox2.Text = reader.GetString(0);

                if (!reader.IsDBNull(1)) textBox3.Text = reader.GetInt32(1).ToString();
                if (!reader.IsDBNull(2)) textBox4.Text = reader.GetInt32(2).ToString();
            }
            reader.Close();

            sqlExpression = @"Select HallID, Name, Seating_Capacity from Hall where Cinema = @b";
            command = new SqlCommand(sqlExpression, r);
            command.Parameters.AddWithValue("@b", Convert.ToInt32(label11.Text));
            reader = command.ExecuteReader();

            listView1.Items.Clear();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    upd_hall.Add_H_id(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                }
            }

            textBox5.Text = "";
            textBox6.Text = "";
            ListViewItem item = new ListViewItem();
            listView1.Items.Clear();
            for (int i = 0; i < upd_hall.tmp.Count; i++)
            {
                ListViewItem it = new ListViewItem(upd_hall.tmp[i].Name, upd_hall.tmp[i].Counter);
                it.SubItems.Add(upd_hall.tmp[i].Counter.ToString());
                listView1.Items.Add(it);
            }

            reader.Close();
        }
    }
}

