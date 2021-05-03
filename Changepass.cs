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
    public partial class Changepass : Form
    {
       
        public Changepass()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");

            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 ff = new Form1();
            ff.Show();
            this.Close();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String na = username.Text;
                String pa = oldpass.Text;
                String newpa = newpass.Text;
                String confirm = confirmpass.Text;
                String newuse = newuser.Text;

                if (na != "" && pa != "" && newpa != "" && confirm != "" && newuse == "")
                {
                    if (newpa == confirm)
                    {
                        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");
                        SqlDataAdapter adap = new SqlDataAdapter("select * from login where username='" + username.Text.Trim() + "' and password='" + oldpass.Text.Trim() + "'", con);
                        DataTable dtbl = new DataTable();
                        adap.Fill(dtbl);
                        if (dtbl.Rows.Count == 1)
                        {
                            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");
                            conn.Open();
                            SqlCommand cmdd = conn.CreateCommand();
                            cmdd.CommandType = CommandType.Text;
                            cmdd = new SqlCommand("update login set password='" + confirmpass.Text + "'", conn);
                            cmdd.ExecuteNonQuery();
                            conn.Close();
                            MessageBox.Show("Password changed sucessfully");
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password");
                        }
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Password Mismatch");
                    }
                }
                else if (na != "" && pa != "" && newpa != "" && confirm != "" && newuse != "" && pincode.Text!="")
                {
                    if (newpa == confirm)
                    {
                        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");
                        SqlDataAdapter adap = new SqlDataAdapter("select * from login where username='" + username.Text.Trim() + "' and password='" + oldpass.Text.Trim() + "'", con);
                        DataTable dtbl = new DataTable();
                        adap.Fill(dtbl);
                        if (dtbl.Rows.Count == 1)
                        {
                            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");
                            conn.Open();
                            SqlCommand cmdd = conn.CreateCommand();
                            cmdd.CommandType = CommandType.Text;
                            cmdd = new SqlCommand("update login set username ='" + newuser.Text + "' ,password='" + confirmpass.Text + "',pincode='"+pincode.Text+"'", conn);
                            cmdd.ExecuteNonQuery();
                            conn.Close();
                            MessageBox.Show("Username and Password and pincode changed sucessfully");
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password");
                        }
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Password Mismatch");
                    }
                }
                else
                {
                    MessageBox.Show("Please Fill the empty Area");
                }
            }catch(InvalidOperationException)
            {
                MessageBox.Show("Error");
            }
    }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
              oldpass.UseSystemPasswordChar = false;
            }
            else
            {
                oldpass.UseSystemPasswordChar = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                newpass.UseSystemPasswordChar = false;
            }
            else
            {
                newpass.UseSystemPasswordChar = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                confirmpass.UseSystemPasswordChar = false;
            }
            else
            {
                confirmpass.UseSystemPasswordChar = true;
            }
        }

        private void Changepass_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");

            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Institue ", con);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                h.Text = da.GetValue(1).ToString();
                hh.Text = da.GetValue(2).ToString();
                hhh.Text = da.GetValue(3).ToString();

            }
            con.Close();
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                pincode.UseSystemPasswordChar = false;
            }
            else
            {
                pincode.UseSystemPasswordChar = true;
            }

        }
    }
}
