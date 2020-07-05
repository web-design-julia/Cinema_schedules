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
using System.Collections;



namespace WindowsFormsApplication
{
    public partial class Add_cinema : Form
    {
        SqlConnection r = Connect.Get_con();
        Hall a = new Hall();
        
        public Add_cinema()
        {
            InitializeComponent();

            
            SqlCommand sqlcom = new SqlCommand("select * from City ", r);
            r.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcom);

            DataSet ds = new DataSet();
            sda.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                comboBox1.Items.Add(ds.Tables[0].Rows[i][1]);

            }
            
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_cinema q = new Admin_cinema();
            q.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void City_combobox(object sender, EventArgs e)
        {

        }

        private void Add_hall(object sender, EventArgs e)
        {
            int flag = 0;
            for (int i = 0; i < a.tmp.Count; i++)
            {
                if (a.tmp[i].Name == textBox5.Text) flag = 1;
            }
            if (flag == 1) MessageBox.Show("Такой зал уже добавлен");
            else
            {
                a.Add_H(textBox5.Text, Convert.ToInt32(textBox6.Text));
                textBox5.Text = "";
                textBox6.Text = "";
                ListViewItem item = new ListViewItem();
                listView1.Items.Clear();
                for (int i = 0; i < a.tmp.Count; i++)
                {
                    ListViewItem it = new ListViewItem(a.tmp[i].Name, a.tmp[i].Counter);
                    it.SubItems.Add(a.tmp[i].Counter.ToString());
                    listView1.Items.Add(it);
                }
            }
            
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.Items.Count>0)
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

        private void Del_Click(object sender, EventArgs e)
        {
            int numberIndex = a.tmp.FindIndex(x => x.Name == textBox5.Text && x.Counter==Convert.ToInt32(textBox6.Text));
            a.tmp.RemoveAt(numberIndex);
            listView1.Items.Clear();
            for (int i = 0; i < a.tmp.Count; i++)
            {
                ListViewItem it = new ListViewItem(a.tmp[i].Name, a.tmp[i].Counter);
                it.SubItems.Add(a.tmp[i].Counter.ToString());
                listView1.Items.Add(it);
            }
            textBox5.Text = "";
            textBox6.Text = "";

        }

        private void edit_Click(object sender, EventArgs e)
        {
            int numberIndex = a.tmp.FindIndex(x => x.Name == listView1.SelectedItems[0].Text && x.Counter == Convert.ToInt32(listView1.SelectedItems[0].SubItems[1].Text));

            a.tmp[numberIndex].Name= textBox5.Text;
            a.tmp[numberIndex].Counter = Convert.ToInt32(textBox6.Text);
            listView1.Items.Clear();
            for (int i = 0; i < a.tmp.Count; i++)
            {
                ListViewItem it = new ListViewItem(a.tmp[i].Name, a.tmp[i].Counter);
                it.SubItems.Add(a.tmp[i].Counter.ToString());
                listView1.Items.Add(it);
            }
            textBox5.Text = "";
            textBox6.Text = "";
        }

        

        

        private void button8_Click(object sender, EventArgs e)
        {
            
                try
                {
                    r.Open();
                }
                catch
                {

                }

            try
            {
                
                if (textBox3.Text == "") textBox3.Text = null;
                SqlCommand command1 = new SqlCommand(@"Insert into Cinema(Name, City, Street, Home, Building) values (@a, (select City_index from City where Name=@b),@c, @f , @d)", r);

                command1.Parameters.AddWithValue("@a", textBox1.Text);
                command1.Parameters.AddWithValue("@b", comboBox1.Text);
                command1.Parameters.AddWithValue("@c", textBox2.Text);
                command1.Parameters.AddWithValue("@d", textBox3.Text);
                command1.Parameters.AddWithValue("@f", Convert.ToInt32(textBox4.Text));
                command1.ExecuteNonQuery();


                for (int i = 0; i < a.tmp.Count; i++)
                {
                    SqlCommand command2 = new SqlCommand(@"Insert into Hall(Cinema,Name,Seating_capacity) values ((select CinemaID from Cinema where Name=@a),@b, @c)", r);
                    command2.Parameters.AddWithValue("@a", textBox1.Text);
                    command2.Parameters.AddWithValue("@b", a.tmp[i].Name);
                    command2.Parameters.AddWithValue("@c", a.tmp[i].Counter);
                    command2.ExecuteNonQuery();
                }


                
                MessageBox.Show("Кинотеатр добавлен");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                a.tmp.Clear();
                listView1.Items.Clear();

            }

            catch(Exception ex)
            {
                MessageBox.Show("Проверьте введенные данные!");
            }










        }

        
    }
}

public class Hall
{
    public string Name { get; set; }
    public int Counter { get; set; }

    public int id  { get; set; }
    public int state { get; set; }

    public List<Hall> tmp = new List<Hall>();

    public void Add_H(string N, int Coun)//данные не из базы
    {
        tmp.Add(new Hall() {state=0, Name = N, Counter = Coun });
        
    }

    public void Add_H_id(int ID, string N, int Coun)
    {
        tmp.Add(new Hall() { state=1, id = ID, Name = N, Counter = Coun });

    }
}

