using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
namespace Library_Management_System
{
    public partial class Deposit : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");

        SqlCommand cmd;
        public Deposit()
        {
            InitializeComponent();
            display();
        }
        public void display()
        {

            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = 0;
            cmd = new SqlCommand("select * from Deposit", con);
            SqlDataReader dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (dr.Read())
            {
                i = i + 1;
                dataGridView1.Rows.Add(i.ToString(), dr["bookid"].ToString(), dr["bookname"].ToString(), dr["enrollno"].ToString(), dr["studname"].ToString(),dr["doi"].ToString() ,dr["dor"].ToString(), dr["dtober"].ToString(),dr["fine"].ToString());
            }
            dr.Close();
            con.Close();


        }
        private void Deposit_Load(object sender, EventArgs e)
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

                    cmd = new SqlCommand("select * from Deposit where bookid like ('%" + textBox1.Text + "%')", con);


                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    cmd = new SqlCommand("select * from Deposit where bookname like ('%" + textBox1.Text + "%')", con);

                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    cmd = new SqlCommand("select * from Deposit where enrollno like ('%" + textBox1.Text + "%')", con);

                }
                else if (comboBox1.SelectedIndex == 3)
                {
                    cmd = new SqlCommand("select * from Deposit where studname like ('%" + textBox1.Text + "%')", con);

                }

                else
                {

                    cmd = new SqlCommand("select * from Deposit where bookid like ('%" + textBox1.Text + "%')", con);
                }
                SqlDataReader dr = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (dr.Read())
                {

                    i = i + 1;
                    dataGridView1.Rows.Add(i.ToString(), dr["bookid"].ToString(), dr["bookname"].ToString(), dr["enrollno"].ToString(), dr["studname"].ToString(), dr["doi"].ToString(), dr["dor"].ToString(), dr["dtober"].ToString(), dr["fine"].ToString());
                }
                dr.Close();
                con.Close();

            }
            catch (IOException)
            {
                MessageBox.Show("Error in showing data");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IssueDeposit i = new IssueDeposit();
            i.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           try {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                if (comboBox1.SelectedIndex == -1)
                {

                    cmd.CommandText = "delete from deposit where bookid='" + textBox1.Text + "'";


                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    cmd.CommandText = "delete from deposit where bookname='" + textBox1.Text + "'";

                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    cmd.CommandText = "delete from deposit where enrollno='" + textBox1.Text + "'";

                }
                else if (comboBox1.SelectedIndex == 3)
                {
                    cmd.CommandText = "delete from deposit where studname='" + textBox1.Text + "'";

                }

                else
                {

                    cmd.CommandText = "delete from deposit where bookid='" + textBox1.Text + "'";
                }

                   

                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Record deleted Sucessfully");
                display();
               
            }
            catch (IOException)
            {
                MessageBox.Show("Error in deleting Record");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
          try  {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from deposit ";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("All Record deleted Sucessfully");
                display();
            }
            catch (IOException)
            {
                MessageBox.Show("Error in deleting Record");
            }
        }
    }
}
