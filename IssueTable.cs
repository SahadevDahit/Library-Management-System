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
using System.IO;

namespace Library_Management_System
{
    public partial class IssueTable : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");

        SqlCommand cmd;
        public IssueTable()
        {
            InitializeComponent();
            display();
        }
        public void display()
        {

            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = 0;
            cmd = new SqlCommand("select * from Issue", con);
            SqlDataReader dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (dr.Read())
            {
                i = i + 1;
                dataGridView1.Rows.Add(i.ToString(), dr["Stud_ID"].ToString(), dr["Stud_name"].ToString(), dr["Book_ID"].ToString(), dr["Book_name"].ToString(), dr["Dateofissue"].ToString(), dr["Datetobereturn"].ToString());
            }
            dr.Close();
            con.Close();


        }
        private void IssueTable_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Institue ", con);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                h.Text = da.GetValue(0).ToString();
                hh.Text = da.GetValue(1).ToString();
                hhh.Text = da.GetValue(2).ToString();

            }
            con.Close();
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                int i = 0;
                if (comboBox1.SelectedIndex == -1)
                {

                    cmd = new SqlCommand("select * from Issue where Stud_ID like ('%" + textBox1.Text + "%')", con);


                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    cmd = new SqlCommand("select * from Issue where Stud_name like ('%" + textBox1.Text + "%')", con);

                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    cmd = new SqlCommand("select * from Issue where Book_ID like ('%" + textBox1.Text + "%')", con);

                }
              
                else
                {

                    cmd = new SqlCommand("select * from Issue where Stud_ID like ('%" + textBox1.Text + "%')", con);
                }
                SqlDataReader dr = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (dr.Read())
                {
                    i = i + 1;
                    dataGridView1.Rows.Add(i.ToString(), dr["Stud_ID"].ToString(), dr["Stud_name"].ToString(), dr["Book_ID"].ToString(), dr["Book_name"].ToString(), dr["Dateofissue"].ToString(), dr["Datetobereturn"].ToString());
                }
                dr.Close();
                con.Close();

            }
            catch (IOException)
            {
                MessageBox.Show("Error in showing data");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IssueDeposit idd = new IssueDeposit();
            idd.Show();
            this.Close();
        }
    }
}
