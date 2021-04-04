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

namespace Library_Management_System
{
    public partial class Editinstitue : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");
       public Editinstitue()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Click(object sender, EventArgs e)
        {
           
            con.Open();
            SqlCommand cmd = new SqlCommand("update Institue set inst=@id,Address=@name, phone=@state", con);
            cmd.Parameters.AddWithValue("@id", n.Text);
            cmd.Parameters.AddWithValue("@name", nn.Text);
            cmd.Parameters.AddWithValue("@state", nnn.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Institute Information Updated Successfully");
            con.Close();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Home hh = new Home();
            hh.Show();
            this.Close();
        }

        private void name_TextChanged(object sender, EventArgs e)
        {

        }

        private void Editinstitue_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.WindowState=   FormWindowState.Maximized;
        }
    }
}
