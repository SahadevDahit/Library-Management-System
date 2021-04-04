using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class Form1 : Form
    {
      SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");

          public Form1()
        {
            InitializeComponent();
          
        }
       
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Forgotpass f = new Forgotpass();
            MessageBox.Show("Select Username and input to recover your password and vice-versa");
            f.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Changepass f = new Changepass();
           
            f.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");

            String na = username.Text;
            String pa = password.Text;
            if (na != "" && pa != "")
            {
                con.Open();
                SqlDataAdapter adap = new SqlDataAdapter("select * from login where Username='" + username.Text.Trim() + "' and password='" + password.Text.Trim() + "'", con);
                DataTable dtbl = new DataTable();
                adap.Fill(dtbl);
                if (dtbl.Rows.Count >0)
                {
                    
                    SqlCommand cmd = new SqlCommand("select * from login ", con);
                    SqlDataReader da = cmd.ExecuteReader();
                    while (da.Read())
                    {
                        String id = da.GetValue(0).ToString();
                        

                        if (id == "2")
                        {
                           
                            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");
                            conn.Open();
                            SqlCommand cmdd = conn.CreateCommand();
                            cmdd.CommandType = CommandType.Text;
                             cmdd = new SqlCommand("update login set loginid=5", conn);
                            cmdd.ExecuteNonQuery();
                            conn.Close();
                            DefaultChange d = new DefaultChange();
                            d.Show();
                            this.Hide();
                        }
                        else
                        {
                             Home h= new Home();
                            h.Show();
                            this.Hide();
                            
                          
                        } 
                    }

                    }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }
                con.Close();

            }
            else
            {
                MessageBox.Show("Please Fill the empty Area");
            }
            
           
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                password.UseSystemPasswordChar = false;
            }
            else
            {
                password.UseSystemPasswordChar = true;
            }





        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;

        }
    }
}
