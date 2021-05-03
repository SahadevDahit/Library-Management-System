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
    public partial class Forgotpass : Form
    {
        public Forgotpass()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 ff = new Form1();
            ff.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            String nam = name.Text;

            String idd = iddd.Text;
            if (nam != "")
            {

                try
                {
                    SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");
                    con.Open();
                    
                   
                    
                        SqlCommand cmd = new SqlCommand("select * from login where pincode=@loginid or password=@pass", con);
                        cmd.Parameters.AddWithValue("@loginid",iddd.Text);
                    cmd.Parameters.AddWithValue("@pass", name.Text);

                    SqlDataReader da = cmd.ExecuteReader();
                    while (da.Read())
                    {
                        String id = da.GetValue(1).ToString();
                        String idname = da.GetValue(2).ToString();
                        String pass = da.GetValue(3).ToString();
                        //String user = ;
                        if (comboBox1.SelectedIndex > -1)
                        {
                            if (comboBox1.SelectedIndex==0)
                            {
                                int ab = id.CompareTo(iddd.Text);
                                int i = idname.CompareTo(name.Text);
                                if (ab == 0 && i == 0)
                                {
                                    MessageBox.Show("Your password is " + pass);
                                    MessageBox.Show("Sucessfully Recovered password :)");
                                }

                                else
                                {
                                    MessageBox.Show("Invalid id or username ");
                                }
                            }
                            else
                            {
                                if (comboBox1.SelectedIndex == 1)
                                {
                                    int i = pass.CompareTo(name.Text);
                                    if ( i == 0)
                                    {
                                        MessageBox.Show("Your pincode id is " + id);
                                        MessageBox.Show("Your Username is " + idname);
                                        MessageBox.Show("Sucessfully Recovered your username :)");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Invalid id or username ");
                                    }
                                }
                            }
                          
                        }
                        else
                        {

                            MessageBox.Show("Please select an item");
                        }
                    }
                    
                    con.Close();
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Problem in Recovering password");
                }

            }
            else
            {
                MessageBox.Show("Please Fill the empty Area");
            }

        }

        private void Forgotpass_Load(object sender, EventArgs e)
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
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                iddd.UseSystemPasswordChar = false;
            }
            else
            {
                iddd.UseSystemPasswordChar = true;
            }
        }
    }
}

