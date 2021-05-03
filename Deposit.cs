using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

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


            if (comboBox2.SelectedIndex == 1)
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                int i = 0;
                cmd = new SqlCommand("select * from deposit_teacher", con);
                SqlDataReader dr = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (dr.Read())
                {
                    i = i + 1;
                    dataGridView1.Rows.Add(i.ToString(), dr["teachid"].ToString(), dr["teachname"].ToString(), dr["teachfaculty"].ToString(), dr["teachpost"].ToString(), dr["bookid"].ToString(), dr["bookname"].ToString(), dr["doi"].ToString(), dr["dtobereturn"].ToString(), dr["dor"].ToString(), dr["fine"].ToString(), dr["picture"]);
                }
                dr.Close();
                con.Close();
            }
            else
            {

                if (con.State == ConnectionState.Closed)
                    con.Open();
                int i = 0;
                cmd = new SqlCommand("select * from deposit_student", con);
                SqlDataReader dr = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (dr.Read())
                {
                    i = i + 1;
                    dataGridView1.Rows.Add(i.ToString(), dr["enroll"].ToString(), dr["studname"].ToString(), dr["faculty"].ToString(), dr["semester"].ToString(), dr["bookid"].ToString(), dr["bookname"].ToString(), dr["doi"].ToString(), dr["dtobereturn"].ToString(), dr["dor"].ToString(), dr["fine"].ToString(),dr["picture"]);
                }
                dr.Close();
                con.Close();
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            IssueDeposit i = new IssueDeposit();
            i.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox2.SelectedIndex== 1)
            {

                dataGridView1.Columns[1].HeaderText = "Teacher_ID";
                dataGridView1.Columns[2].HeaderText = "Name";
                dataGridView1.Columns[3].HeaderText = "Faculty";
                dataGridView1.Columns[4].HeaderText = "Post";
                comboBox1.Items.Clear();
                comboBox1.Items.Add("Teacher Id");
                comboBox1.Items.Add(" Name");
                comboBox1.Items.Add("Faculty");
                comboBox1.Items.Add("Post");
                comboBox1.Items.Add("Book ID");
                comboBox1.Items.Add("Book Name");
                label3.Text = "Teacher_ID";
                label5.Text = "Post";

                display();
            }
            else
            {

                dataGridView1.Columns[1].HeaderText = "Enroll_No";
                dataGridView1.Columns[2].HeaderText = "Name";
                dataGridView1.Columns[3].HeaderText = "Faculty";
                dataGridView1.Columns[4].HeaderText = "Semester";
                comboBox1.Items.Clear();
                comboBox1.Items.Add("Enrollment NO");
                comboBox1.Items.Add(" Name");
                comboBox1.Items.Add("Faculty");
                comboBox1.Items.Add("Semester");
                comboBox1.Items.Add("Book ID");
                comboBox1.Items.Add("Book Name");
                label3.Text = "Enroll NO";
                label5.Text = "Faculty";
                display();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (comboBox2.SelectedIndex == 1)
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    int i = 0;
                    if (comboBox1.SelectedIndex == -1 || comboBox1.SelectedIndex==0)
                    {

                        cmd = new SqlCommand("select * from deposit_teacher where teachid like ('%" + textBox1.Text + "%')", con);


                    }
                    else if (comboBox1.SelectedIndex == 1)
                    {
                        cmd = new SqlCommand("select * from deposit_teacher where teachname like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 2)
                    {
                        cmd = new SqlCommand("select * from deposit_teacher where teachfaculty like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 3)
                    {
                        cmd = new SqlCommand("select * from deposit_teacher where teachpost like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 4)
                    {
                        cmd = new SqlCommand("select * from deposit_teacher where bookid like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 5)
                    {
                        cmd = new SqlCommand("select * from deposit_teacher where bookname like ('%" + textBox1.Text + "%')", con);

                    }
                    else
                    {

                        cmd = new SqlCommand("select * from deposit_teacher where teachid like ('%" + textBox1.Text + "%')", con);
                    }
                    SqlDataReader dr = cmd.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    while (dr.Read())
                    {

                        i = i + 1;
                        dataGridView1.Rows.Add(i.ToString(), dr["teachid"].ToString(), dr["teachname"].ToString(), dr["teachfaculty"].ToString(), dr["teachpost"].ToString(), dr["bookid"].ToString(), dr["bookname"].ToString(), dr["doi"].ToString(), dr["dtobereturn"].ToString(), dr["dor"].ToString(), dr["fine"].ToString(), dr["picture"]);
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

                        cmd = new SqlCommand("select * from deposit_student where enroll like ('%" + textBox1.Text + "%')", con);


                    }
                    else if (comboBox1.SelectedIndex == 1)
                    {
                        cmd = new SqlCommand("select * from deposit_student where studname like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 2)
                    {
                        cmd = new SqlCommand("select * from deposit_student where faculty like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 3)
                    {
                        cmd = new SqlCommand("select * from deposit_student where semester like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 4)
                    {
                        cmd = new SqlCommand("select * from deposit_student where bookid like ('%" + textBox1.Text + "%')", con);

                    }
                    else if (comboBox1.SelectedIndex == 5)
                    {
                        cmd = new SqlCommand("select * from deposit_student where bookname like ('%" + textBox1.Text + "%')", con);

                    }
                    else
                    {

                        cmd = new SqlCommand("select * from deposit_student where enroll like ('%" + textBox1.Text + "%')", con);
                    }
                    SqlDataReader dr = cmd.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    while (dr.Read())
                    {

                        i = i + 1;
                        dataGridView1.Rows.Add(i.ToString(), dr["enroll"].ToString(), dr["studname"].ToString(), dr["faculty"].ToString(), dr["semester"].ToString(), dr["bookid"].ToString(), dr["bookname"].ToString(), dr["doi"].ToString(), dr["dtobereturn"].ToString(), dr["dor"].ToString(), dr["fine"].ToString(), dr["picture"]);
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

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from deposit_student ";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("All Record deleted Sucessfully");
                display();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Error in deleting all Record");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 1)
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    if (comboBox1.SelectedIndex == -1)
                    {

                        cmd.CommandText = "delete from deposit_teacher where  enroll='" + textBox1.Text + "'";


                    }
                    else if (comboBox1.SelectedIndex == 1)
                    {
                        cmd.CommandText = "delete from deposit_teacher where studname='" + textBox1.Text + "'";

                    }
                    else if (comboBox1.SelectedIndex == 2)
                    {
                        cmd.CommandText = "delete from deposit_teacher where faculty='" + textBox1.Text + "'";

                    }
                    else if (comboBox1.SelectedIndex == 3)
                    {
                        cmd.CommandText = "delete from deposit_teacher where semester='" + textBox1.Text + "'";

                    }
                    else if (comboBox1.SelectedIndex == 4)
                    {
                        cmd.CommandText = "delete from deposit_teacher where bookid='" + textBox1.Text + "'";

                    }
                    else if (comboBox1.SelectedIndex == 5)
                    {
                        cmd.CommandText = "delete from deposit_teacher where bookname='" + textBox1.Text + "'";

                    }
                    else
                    {

                        cmd.CommandText = "delete from deposit_teacher where enroll='" + textBox1.Text + "'";
                    }



                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Record deleted Sucessfully");
                    display();

                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Error in deleting Record");
                } 

            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    if (comboBox1.SelectedIndex == -1)
                    {

                        cmd.CommandText = "delete from deposit_student where enroll='" + textBox1.Text + "'";


                    }
                    else if (comboBox1.SelectedIndex == 1)
                    {
                        cmd.CommandText = "delete from deposit_student where studname='" + textBox1.Text + "'";

                    }
                    else if (comboBox1.SelectedIndex == 2)
                    {
                        cmd.CommandText = "delete from deposit_student where faculty='" + textBox1.Text + "'";

                    }
                    else if (comboBox1.SelectedIndex == 3)
                    {
                        cmd.CommandText = "delete from deposit_student where semester='" + textBox1.Text + "'";

                    }
                    else if (comboBox1.SelectedIndex == 4)
                    {
                        cmd.CommandText = "delete from deposit_student where bookid='" + textBox1.Text + "'";

                    }
                    else if (comboBox1.SelectedIndex == 5)
                    {
                        cmd.CommandText = "delete from deposit_student where bookname='" + textBox1.Text + "'";

                    }
                    else
                    {

                        cmd.CommandText = "delete from deposit_student where enroll='" + textBox1.Text + "'";
                    }



                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Record deleted Sucessfully");
                    display();

                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Error in deleting Record");
                }
            }
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
                    textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    textBox9.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    textBox10.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();




                    if (dataGridView1.Rows[e.RowIndex].Cells[11].Value == null)
                    {
                        pictureBox3.Image = null;
                    }
                    else
                    {
                        byte[] imgdata = (byte[])dataGridView1.CurrentRow.Cells[11].Value;
                        MemoryStream ms = new MemoryStream(imgdata);
                        pictureBox3.Image = Image.FromStream(ms);
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }

        private void Deposit_Load(object sender, EventArgs e)
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
    }
}
