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
            if (comboBox2.SelectedIndex == 1)
            {
                comboBox1.Items.Clear();
                comboBox1.Items.Add("Teacher_ID");
                comboBox1.Items.Add("Teacher_name");
                comboBox1.Items.Add("Post");
                comboBox1.Items.Add("Book_ID");
                comboBox1.Items.Add("Book name");
                comboBox1.Items.Add("Faculty");
                if (con.State == ConnectionState.Closed)
                    con.Open();
                int i = 0;
                cmd = new SqlCommand("select * from issue_teacher", con);
                SqlDataReader dr = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (dr.Read())
                {
                    i = i + 1;
                    dataGridView1.Rows.Add(i.ToString(), dr["teacherid"].ToString(), dr["teachername"].ToString(), dr["post"].ToString(), dr["bookid"].ToString(), dr["bookname"].ToString(), dr["faculty"].ToString(), dr["doi"].ToString(), dr["dtobereturn"].ToString(), dr["picture"]);
                }
                dr.Close();
                con.Close();

            }
            else
            {
                comboBox1.Items.Clear();
                comboBox1.Items.Add("Enrolment No");
                comboBox1.Items.Add("Student Name");
                comboBox1.Items.Add("Faculty");
                comboBox1.Items.Add("Semester");
                comboBox1.Items.Add("Book ID");
                comboBox1.Items.Add("Book Name");
                if (con.State == ConnectionState.Closed)
                    con.Open();
                int i = 0;
                cmd = new SqlCommand("select * from issue_student", con);
                SqlDataReader dr = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (dr.Read())
                {
                    i = i + 1;
                    dataGridView1.Rows.Add(i.ToString(), dr["Enroll"].ToString(), dr["Stud_name"].ToString(), dr["faculty"].ToString(), dr["semester"].ToString(), dr["bookid"].ToString(), dr["bookname"].ToString(), dr["doi"].ToString(), dr["dtobereturn"].ToString(), dr["picture"]);
                }
                dr.Close();
                con.Close();

            }
        }
        private void IssueTable_Load(object sender, EventArgs e)
        {
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.SelectedIndex == 1)
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    int i = 0;
                    if (comboBox1.SelectedIndex == -1)
                    {

                        cmd = new SqlCommand("select * from issue_teacher where Enroll like ('%" + textBox1.Text + "%')", con);


                    }
                    else if (comboBox1.SelectedIndex == 1)
                    {
                        cmd = new SqlCommand("select * from issue_teacher where teacherid like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 2)
                    {
                        cmd = new SqlCommand("select * from issue_teacher where teachername like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 3)
                    {
                        cmd = new SqlCommand("select * from issue_teacher where post like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 4)
                    {
                        cmd = new SqlCommand("select * from issue_teacher where bookid like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 5)
                    {
                        cmd = new SqlCommand("select * from issue_teacher where bookname like ('%" + textBox1.Text + "%')", con);

                    }
                    else
                    {

                        cmd = new SqlCommand("select * from issue_teacher where teacherid like ('%" + textBox1.Text + "%')", con);
                    }
                    SqlDataReader dr = cmd.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    while (dr.Read())
                    {
                        i = i + 1;
                        dataGridView1.Rows.Add(i.ToString(), dr["teacherid"].ToString(), dr["teachername"].ToString(), dr["post"].ToString(), dr["bookid"].ToString(), dr["bookname"].ToString(), dr["faculty"].ToString(), dr["doi"].ToString(), dr["dtobereturn"].ToString(), dr["picture"]);
                    }
                    dr.Close();
                    con.Close();
                }
                else
                {

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    int i = 0;
                    if (comboBox1.SelectedIndex == -1)
                    {

                        cmd = new SqlCommand("select * from issue_student where Enroll like ('%" + textBox1.Text + "%')", con);


                    }
                    else if (comboBox1.SelectedIndex == 1)
                    {
                        cmd = new SqlCommand("select * from issue_student where Stud_name like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 2)
                    {
                        cmd = new SqlCommand("select * from issue_student where faculty like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 3)
                    {
                        cmd = new SqlCommand("select * from issue_student where semester like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 4)
                    {
                        cmd = new SqlCommand("select * from issue_student where bookid like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 5)
                    {
                        cmd = new SqlCommand("select * from issue_student where bookname like ('%" + textBox1.Text + "%')", con);

                    }
                    else
                    {

                        cmd = new SqlCommand("select * from issue_student where Enroll like ('%" + textBox1.Text + "%')", con);
                    }
                    SqlDataReader dr = cmd.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    while (dr.Read())
                    {
                        i = i + 1;
                        dataGridView1.Rows.Add(i.ToString(), dr["Enroll"].ToString(), dr["Stud_name"].ToString(), dr["faculty"].ToString(), dr["semester"].ToString(), dr["bookid"].ToString(), dr["bookname"].ToString(), dr["doi"].ToString(), dr["dtobereturn"].ToString(), dr["picture"]);
                    }
                    dr.Close();
                    con.Close();
                }

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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshh();
            if (comboBox2.SelectedIndex == 1)
            {
               
                dataGridView1.Columns[1].HeaderText = "Teacher_ID";
                dataGridView1.Columns[2].HeaderText = "Name";
                dataGridView1.Columns[3].HeaderText = "Post";
                dataGridView1.Columns[4].HeaderText = "Book_ID";
                dataGridView1.Columns[5].HeaderText = "Book_Name";
                dataGridView1.Columns[6].HeaderText = "Faculty";
                

                label3.Text = "Teacher ID";

                label5.Text = "Post";
                label6.Text = "Book_ID";
                label7.Text = "Book Name";
                label8.Text = "Faculty";
                display();
            } else
            {
                
                dataGridView1.Columns[1].HeaderText = "Enroll_No";
                dataGridView1.Columns[2].HeaderText = "Name";
                dataGridView1.Columns[3].HeaderText = "Faculty";
                dataGridView1.Columns[4].HeaderText = "Semester";
                dataGridView1.Columns[5].HeaderText = "Book_ID";
                dataGridView1.Columns[6].HeaderText = "Book_Name";
                label3.Text = "Enroll NO";
                label5.Text = "Faculty";
                label6.Text = "Semester";
                label7.Text = "Book_ID";
                label8.Text = "Book_Name";
                display();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IssueDeposit i = new IssueDeposit();
            i.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (dataGridView1.Rows[e.RowIndex].Cells[1].Value == null)
                {
                    refreshh();
                }
                else
                {
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    textBox9.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    textBox10.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    if (dataGridView1.Rows[e.RowIndex].Cells[9].Value == null)
                    {
                        pictureBox3.Image = null;
                    }
                    else
                    {
                        byte[] imgdata = (byte[])dataGridView1.CurrentRow.Cells[9].Value;
                        MemoryStream ms = new MemoryStream(imgdata);
                        pictureBox3.Image = Image.FromStream(ms);
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }
        public void refreshh()
            {
            pictureBox3.Image = null;
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            }
        private void button4_Click(object sender, EventArgs e)
        {
        refreshh();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            // this.dataGridView1.DefaultCellStyle.ForeColor = Color.Red;
          
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(textBox2.Text!="")
            {
                /*  DateTime d = DateTime.Now;
                  string dn = d.ToLongDateString();
                  DateTime d1 = Convert.ToDateTime(dn);
                  int alert = int.Parse(textBox2.Text);
                  try
                  {

                      if (dataGridView1.Rows.Count > 0)
                      {
                          for (int i = 0; i < dataGridView1.Rows.Count; i++)
                          {
                              string ssd = dataGridView1.Rows[i].Cells[8].Value.ToString();
                              DateTime dr = Convert.ToDateTime(ssd);
                              string ss = dr.ToLongDateString();



                              DateTime d2 = Convert.ToDateTime(ss);

                              int sd = Convert.ToInt32((d2 - d1).TotalDays);
                              if (alert >= sd)
                              {
                                  //  dataGridView1.Rows[i].Cells[i].Style.ForeColor = Color.Red;


                              }

                          }
                      }*/
                int alert = int.Parse(textBox2.Text);

                DateTime d = DateTime.Now;
                string dn = d.ToLongDateString();
                DateTime d1 = Convert.ToDateTime(dn);
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    string dtobereturn = row.Cells[8].Value.ToString();
                    DateTime dr = Convert.ToDateTime(dtobereturn);
                    string ss = dr.ToLongDateString();
                    DateTime d2 = Convert.ToDateTime(ss);
                    int sd = Convert.ToInt32((d2 - d1).TotalDays);
                    if (alert >= sd)

                        row.DefaultCellStyle.ForeColor = Color.Red;
                    else
                    {
                        row.DefaultCellStyle.ForeColor = Color.Black;

                    }

                }

            }
               

            
            else
            {
                MessageBox.Show("Enter the alert day");
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DateTime d = DateTime.Now;
            string dn = d.ToLongDateString();
            DateTime d1 = Convert.ToDateTime(dn);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string dtobereturn = row.Cells[8].Value.ToString();
                DateTime dr = Convert.ToDateTime(dtobereturn);
                string ss = dr.ToLongDateString();
                DateTime d2 = Convert.ToDateTime(ss);
                int sd = Convert.ToInt32((d2 - d1).TotalDays);
                if (5>= sd)
                    row.DefaultCellStyle.ForeColor = Color.Red;


            }

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            try
            {

                if (comboBox2.SelectedIndex == 1)
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    int i = 0;
                    if (comboBox1.SelectedIndex == -1 || comboBox1.SelectedIndex == 0)
                    {

                        cmd = new SqlCommand("select * from issue_teacher where teacherid like ('%" + textBox1.Text + "%')", con);


                    }
                    else if (comboBox1.SelectedIndex == 1)
                    {
                        cmd = new SqlCommand("select * from issue_teacher where teachername like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 2)
                    {
                        cmd = new SqlCommand("select * from issue_teacher where post like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 3)
                    {
                        cmd = new SqlCommand("select * from deposit_teacher where  bookid like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 4)
                    {
                        cmd = new SqlCommand("select * from issue_teacher where bookname like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 5)
                    {
                        cmd = new SqlCommand("select * from issue_teacher where faculty like ('%" + textBox1.Text + "%')", con);

                    }
                    else
                    {

                        cmd = new SqlCommand("select * from issue_teacher where teacherid like ('%" + textBox1.Text + "%')", con);
                    }
                    SqlDataReader dr = cmd.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    while (dr.Read())
                    {

                        i = i + 1;
                        dataGridView1.Rows.Add(i.ToString(), dr["teacherid"].ToString(), dr["teachername"].ToString(), dr["post"].ToString(), dr["bookid"].ToString(), dr["bookname"].ToString(), dr["faculty"].ToString(), dr["doi"].ToString(), dr["dtobereturn"].ToString(), dr["picture"]);
                    }
                    dr.Close();
                    con.Close();


                }
                else
                {


                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    int i = 0;
                    if (comboBox1.SelectedIndex == -1)
                    {

                        cmd = new SqlCommand("select * from issue_student where Enroll like ('%" + textBox1.Text + "%')", con);


                    }
                    else if (comboBox1.SelectedIndex == 1)
                    {
                        cmd = new SqlCommand("select * from issue_student where Stud_name like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 2)
                    {
                        cmd = new SqlCommand("select * from issue_student where faculty like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 3)
                    {
                        cmd = new SqlCommand("select * from issue_student where semester like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 4)
                    {
                        cmd = new SqlCommand("select * from issue_student where bookid like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 5)
                    {
                        cmd = new SqlCommand("select * from issue_student where bookname like ('%" + textBox1.Text + "%')", con);

                    }
                    else
                    {

                        cmd = new SqlCommand("select * from issue_student where Enroll like ('%" + textBox1.Text + "%')", con);
                    }
                    SqlDataReader dr = cmd.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    while (dr.Read())
                    {

                        i = i + 1;
                        dataGridView1.Rows.Add(i.ToString(), dr["Enroll"].ToString(), dr["Stud_name"].ToString(), dr["faculty"].ToString(), dr["semester"].ToString(), dr["bookid"].ToString(), dr["bookname"].ToString(), dr["doi"].ToString(), dr["dtobereturn"].ToString(), dr["picture"]);
                    }
                    dr.Close();
                    con.Close();

                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Error in showing data");
            }

        }

        private void textBox2_TextChanged_2(object sender, EventArgs e)
        {
            if(textBox2.Text!="")
            {
                int alert = int.Parse(textBox2.Text);

                DateTime d = DateTime.Now;
                string dn = d.ToLongDateString();
                DateTime d1 = Convert.ToDateTime(dn);
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    string dtobereturn = row.Cells[8].Value.ToString();
                    DateTime dr = Convert.ToDateTime(dtobereturn);
                    string ss = dr.ToLongDateString();
                    DateTime d2 = Convert.ToDateTime(ss);
                    int sd = Convert.ToInt32((d2 - d1).TotalDays);
                    if (alert >= sd)
                    {
                        row.Visible = true;
                        row.DefaultCellStyle.ForeColor = Color.Red;
                    }
                    else
                    {
                        row.Visible = false;
                        row.DefaultCellStyle.ForeColor = Color.Black;

                    }

                }
            }
            else
            {
                DateTime d = DateTime.Now;
                string dn = d.ToLongDateString();
                DateTime d1 = Convert.ToDateTime(dn);
                foreach (DataGridViewRow rowone in dataGridView1.Rows)
                {
                    string dtobereturn = rowone.Cells[8].Value.ToString();
                    DateTime dr = Convert.ToDateTime(dtobereturn);
                    string ss = dr.ToLongDateString();
                    DateTime d2 = Convert.ToDateTime(ss);
                    int sd = Convert.ToInt32((d2 - d1).TotalDays);
                    rowone.Visible = true;
                    if (5 >= sd)
                        rowone.DefaultCellStyle.ForeColor = Color.Red;


                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("This field should contain digits");
            }
        }
    }
}
