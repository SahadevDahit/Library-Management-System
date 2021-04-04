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
    public partial class DefaultChange : Form
    {
        public DefaultChange()
        {
            InitializeComponent();

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String user = username.Text;
            String pa = pass.Text;
            String confirm = confirmpass.Text;
            if ((log.Text).Length > 3)
            {


                if (user != "" && pa != "" && confirm != "" && institute.Text!="")
                {
                    

                    if (pa == confirm)
                    {
                        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");

                        con.Open();
                        SqlCommand cmdd = con.CreateCommand();
                        cmdd.CommandType = CommandType.Text;
                        cmdd = new SqlCommand("update login set loginid=@id, Username=@name,password=@pass", con);
                        cmdd.Parameters.AddWithValue("@id", log.Text);
                        cmdd.Parameters.AddWithValue("@name", username.Text);
                        cmdd.Parameters.AddWithValue("@pass", pass.Text);
                        cmdd.ExecuteNonQuery();
                        con.Close();

                       con.Open();
                        SqlCommand  cm = new SqlCommand("insert into Institue(inst,Address,phone) values(@name,@state,@ph)", con);
                        cm.Parameters.AddWithValue("@name", institute.Text);
                        cm.Parameters.AddWithValue("@state", addres.Text);
                        cm.Parameters.AddWithValue("@ph", phno.Text);
                        cm.ExecuteNonQuery();
                        con.Close();

                      
                        Form1 ff = new Form1();
                        ff.Show();
                        this.Close();
                        MessageBox.Show("You have changed your default username and password sucessfully");
                    }
                    else
                    {
                        MessageBox.Show("Password Mismatched");
                    }

                }
                else
                {
                    MessageBox.Show("Enter the value of username password and Institute name");
                }
            }

            else
            {
                MessageBox.Show("Too week login id. It's length must be greater than 3");
            }
        }

    private void button3_Click(object sender, EventArgs e)
        {
            String user = username.Text;
            String pa = pass.Text;
            String confirm = confirmpass.Text;
            if (user == "" || pa == "" || confirm == "")
            {

                SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");
                conn.Open();
                SqlCommand cmdd = conn.CreateCommand();
                cmdd.CommandType = CommandType.Text;
            
                cmdd = new SqlCommand("update login set loginid=2", conn); ;
                cmdd.ExecuteNonQuery();
                conn.Close();
                Form1 ff = new Form1();
                ff.Show();
                this.Close();
            }
            else {
                Form1 ff = new Form1();
                ff.Show();
                this.Close(); }
          
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                pass.UseSystemPasswordChar = false;
            }
            else
            {
                pass.UseSystemPasswordChar = true;
            }
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
              confirmpass.UseSystemPasswordChar = false;
            }
            else
            {
              confirmpass.UseSystemPasswordChar = true;
            }
        }

        private void DefaultChange_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
