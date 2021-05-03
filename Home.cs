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
    public partial class Home : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");

        public Home()
        {
            Forgotpass f = new Forgotpass();
            
            InitializeComponent();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Institue ", con);
              SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                h.Text = da.GetValue(1).ToString();
                hh.Text= da.GetValue(2).ToString();
                hhh.Text= da.GetValue(3).ToString();

            }
            con.Close();
        }

            private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Books h = new Books();
            h.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Student s = new Student();
            s.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Staff st = new Staff();
            st.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IssueDeposit iis = new IssueDeposit();
            iis.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Editinstitue ee = new Editinstitue();
            ee.Show();
            this.Close();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Teacher t = new Teacher();
            t.Show();
            this.Hide();
        }
    }
}
