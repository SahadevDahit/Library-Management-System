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
using System.Drawing.Imaging;
using System.Configuration;
using System.IO;
using System.Drawing.Printing;

namespace Library_Management_System
{
    public partial class IssueDeposit : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");

        public IssueDeposit()
        {
            InitializeComponent();
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Close();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox12.Text == "")
            {
                MessageBox.Show("Enter the Enrollment No ");
            }
            else
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Student where Enroll='" + textBox12.Text + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int i = 0;
                i = int.Parse(dt.Rows.Count.ToString());
                if (i == 0)
                {
                    MessageBox.Show("Enrollment No entered is not available");
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        textBox1.Text = dr["Enroll"].ToString();
                        textBox2.Text = dr["Name"].ToString();
                        textBox3.Text = dr["Faculty"].ToString();
                        textBox4.Text = dr["Semester"].ToString();
                        textBox5.Text = dr["Contact"].ToString();
                        byte[] imgdata = (byte[])dr["Picture"];
                        MemoryStream ms = new MemoryStream(imgdata);
                        pictureBox3.Image = Image.FromStream(ms);
                    }
                    con.Close();

                    con.Open();
                     cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from Total where Enrollno='" + textBox12.Text + "'";
                    cmd.ExecuteNonQuery();
                    DataTable ddt = new DataTable();
                    SqlDataAdapter daa = new SqlDataAdapter(cmd);
                    daa.Fill(ddt);
                    int y = 0;
                    y = int.Parse(ddt.Rows.Count.ToString());
                    if (y == 0)
                    {
                        totalbook.Text = "0";
                        
                    }
                    else
                    {
                        
                        SqlCommand cm = new SqlCommand("select * from Total where Enrollno=@enroll", con);
                        cm.Parameters.AddWithValue("@enroll", textBox12.Text);
                        SqlDataReader d = cm.ExecuteReader();
                        while (d.Read())
                        {
                            totalbook.Text = d.GetValue(1).ToString();
                        }
                        d.Close();
                        con.Close();
                    }

                    lst();

                }
                
            }
        }
        public void lst()
        {
            try
            {
                con.Open();
                SqlCommand ccm = new SqlCommand("select * from Issue where Stud_ID=@id", con);
                ccm.Parameters.AddWithValue("@id", textBox1.Text);
                SqlDataReader dd = ccm.ExecuteReader();
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                while (dd.Read())
                {

                    listBox1.Items.Add(dd.GetValue(2).ToString());
                    listBox2.Items.Add(dd.GetValue(3).ToString());


                }
                dd.Close();
                con.Close();
            }
            catch (IOException)
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox13.Text == "")
            {
                MessageBox.Show("Enter the Book_ID");
            }
            else
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Book_Records where bookid='" + textBox13.Text + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int i = int.Parse(dt.Rows.Count.ToString());
                if (i == 0)
                {
                    MessageBox.Show("Book_ID entered is not available");
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        textBox6.Text = dr["bookid"].ToString();
                        textBox7.Text = dr["bookname"].ToString();
                        textBox8.Text = dr["category"].ToString();
                        textBox9.Text = dr["quantity"].ToString();
                        textBox10.Text = dr["status"].ToString();
                       
                    }
                }
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox12.Text = "";
            textBox1.Text ="";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            pictureBox3.Image =null;
            totalbook.Text = "";
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            textBox11.Text = "";
            textBox14.Text = "";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox13.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool ls = true;
            foreach (object litem in listBox1.Items)
            {
                if (textBox6.Text.CompareTo(litem.ToString()) == 0)
                {

                    ls = false;
                    break;
                }

            }
             if (textBox1.Text != "" && textBox6.Text != "")
              {
                if (int.Parse(textBox9.Text) > 0)
                {

                    if (ls == true)
                    {

                        if (int.Parse(totalbook.Text) < 6)
                        {
                            DateTime d1 = dateTimePicker1.Value.Date;
                            DateTime d2 = dateTimePicker2.Value.Date;
                            TimeSpan ts = d2 - d1;
                            int days = ts.Days;
                            if (days > 0)
                            {

                                try
                                {//inserting the data into issue table
                                    con.Open();
                                    SqlCommand cmd = con.CreateCommand();
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = "insert into Issue values( '" + textBox1.Text + "', '" + textBox2.Text + "','" + textBox6.Text + "' ,'" + textBox7.Text + "','" + dateTimePicker1.Text + "','" + dateTimePicker2.Text + "' )";
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                    //updating the total no of book issuing in total table
                                    con.Open();
                                    cmd = con.CreateCommand();
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = "select * from Total where Enrollno='" + textBox12.Text + "'";
                                    cmd.ExecuteNonQuery();
                                    DataTable dt = new DataTable();
                                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                                    da.Fill(dt);
                                    int i = int.Parse(dt.Rows.Count.ToString());
                                    if (i == 0)
                                    {
                                        int x = int.Parse(totalbook.Text);
                                        x = x + 1;
                                        cmd = con.CreateCommand();
                                        cmd.CommandType = CommandType.Text;
                                        cmd.CommandText = "insert into Total values( '" + textBox1.Text + "', '" + x + "')";
                                        cmd.ExecuteNonQuery();
                                        totalbook.Text = x.ToString();
                                    }
                                    else
                                    {
                                        int x = int.Parse(totalbook.Text);
                                        x = x + 1;
                                        SqlCommand cmm = con.CreateCommand();
                                        cmm.CommandType = CommandType.Text;
                                        cmm.CommandText = "update Total set  Noofbook='" + x + "'  where Enrollno='" + textBox1.Text + "' ";
                                        cmm.ExecuteNonQuery();
                                        totalbook.Text = x.ToString();
                                    }

                                    con.Close();

                                    //update quantity of book after issuing
                                    con.Open();
                                    int y = int.Parse(textBox9.Text);
                                    y = y - 1;
                                    SqlCommand cm = con.CreateCommand();
                                    cm.CommandType = CommandType.Text;
                                    cm.CommandText = "update Book_Records set  quantity='" + y + "'  where bookid='" + textBox13.Text + "' ";
                                    cm.ExecuteNonQuery();
                                    con.Close();
                                    textBox9.Text = y.ToString();
                                    MessageBox.Show("Book issued  Sucessfully");
                                    lst();
                                }
                                catch
                                {
                                    MessageBox.Show("Error in Issuing book ");
                                }

                            }
                            else
                            {
                                MessageBox.Show("Date to be return cannot be earlier than Date of issue ");
                            }
                        }


                        else
                        {
                            MessageBox.Show("You cannot issue more than 6 book to the same student");
                        }

                    }

                    else
                    {
                        MessageBox.Show("Same two book to one person");
                    }
                }
                else
                {
                    MessageBox.Show("You cannot issue book because it is not available");
                }
            }
            else
            {
                MessageBox.Show("Enrollment no and Book id is required to issue book");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void dtime()
        {

            try
            {
                con.Open();
                SqlCommand ccm = new SqlCommand("select * from Issue where Book_ID=@id and Book_name=@nam", con);
                ccm.Parameters.AddWithValue("@id", textBox11.Text);
                ccm.Parameters.AddWithValue("@nam", textBox14.Text);
                SqlDataReader dd = ccm.ExecuteReader();

                while (dd.Read())
                {
                    dateTimePicker3.Value = DateTime.Parse(dd.GetValue(4).ToString());
                    dateTimePicker4.Value = DateTime.Parse(dd.GetValue(5).ToString());

                }
                dd.Close();
                con.Close();
            }
            catch (IOException)
            {
                MessageBox.Show("Error");
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (listBox1.SelectedIndex > -1)
            {
                if(listBox2.Items.Count>listBox1.SelectedIndex)
                {
                    listBox2.SelectedIndex = listBox1.SelectedIndex;
                    textBox11.Text = listBox1.SelectedItem.ToString();
                    int index = listBox1.SelectedIndex;
                    textBox14.Text = listBox2.Items[index].ToString();
                }
            }

            dtime();



        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex > -1)
            {
                if (listBox1.Items.Count > listBox1.SelectedIndex)
                {
                    listBox1.SelectedIndex = listBox2.SelectedIndex;
                    textBox11.Text = listBox1.SelectedItem.ToString();
                    int index = listBox1.SelectedIndex;
                    textBox14.Text = listBox2.Items[index].ToString();
                }
            }
            dtime();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DateTime d1 = dateTimePicker4.Value.Date;
            DateTime d2 = dateTimePicker5.Value.Date;
            TimeSpan ts = d2 - d1;
            int days = ts.Days;
            if (days>0)
            {
            days = days * 5;
            fine.Text = days.ToString();
            }
            else
            {
                MessageBox.Show("Date of deposit cannot be earlier than Date to be return");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="" && textBox11.Text!="")
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "delete from Issue where Stud_ID='" + textBox1.Text + "' and Book_ID='" + textBox11.Text + "'";
                    cmd.ExecuteNonQuery();
                    con.Close();



                    con.Open();
                    SqlCommand asd = new SqlCommand("select * from Book_Records where bookid='" + textBox11.Text + "'", con);
                    SqlDataReader sa = asd.ExecuteReader();
                    while (sa.Read())

                    {
                        int y = int.Parse(sa.GetValue(5).ToString());
                        y = y + 1;
                        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");
                        conn.Open();
                        SqlCommand ad = conn.CreateCommand();
                        ad.CommandType = CommandType.Text;
                        ad.CommandText = "update Book_Records set  quantity='" + y + "'  where bookid='" + textBox11.Text + "' ";
                        ad.ExecuteNonQuery();
                        conn.Close();
                    }
                    sa.Close();
                    con.Close();



                    con.Open();
                    int x = int.Parse(totalbook.Text);
                    x = x - 1;
                    SqlCommand cmm = con.CreateCommand();
                    cmm.CommandType = CommandType.Text;
                    cmm.CommandText = "update Total set  Noofbook='" + x + "'  where Enrollno='" + textBox1.Text + "' ";
                    cmm.ExecuteNonQuery();
                    totalbook.Text = x.ToString();
                    con.Close();




                    con.Open();
                    cmd = new SqlCommand("insert into Deposit values(@id,@name,@fac,@sem,@roll,@cont,@dob,@em)", con);
                    cmd.Parameters.AddWithValue("@id", textBox11.Text);
                    cmd.Parameters.AddWithValue("@name", textBox14.Text);
                    cmd.Parameters.AddWithValue("@fac", textBox1.Text);
                    cmd.Parameters.AddWithValue("@sem", textBox2.Text);
                    cmd.Parameters.AddWithValue("@roll", dateTimePicker3.Text);
                    cmd.Parameters.AddWithValue("@cont", dateTimePicker4.Text);
                    cmd.Parameters.AddWithValue("@dob", dateTimePicker5.Text);
                    cmd.Parameters.AddWithValue("@em", fine.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Book Deposited Sucessfully");

                    lst();
                }
                catch (IOException)
                {
                    MessageBox.Show("Error");
                }


            }
            else
            {
                MessageBox.Show("Enter the Enrollment No and select Bookid or Book Name from listbox to be deposited");
            }
        }

        private void IssueDeposit_Load(object sender, EventArgs e)
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

        private void button6_Click(object sender, EventArgs e)
        {
            IssueTable iss = new IssueTable();
            iss.Show();
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Deposit d = new Deposit();
            d.Show();
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {

            this.Hide();
            if (comboBox1.SelectedIndex == -1 || comboBox1.SelectedIndex == 0)
            {

                if (textBox1.Text != "" && textBox11.Text != "")
                {
                    this.Hide();
                    if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.Print();
                    }
                    this.Show();
                }

            }

            else if (textBox1.Text != "" && textBox6.Text != "")
            {
                this.Hide();
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
                this.Show();
            }
            else
            {
                MessageBox.Show("Please fill the Record of Enrollment no and Book id");

            }




            this.Show();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {

            try
            {
                e.Graphics.DrawString("*******************************************************************", new Font("Century Gothic", 14, FontStyle.Bold), Brushes.Black, new PointF(80, 80));

                e.Graphics.DrawString(h.Text, new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Black, new PointF(180, 100));
                e.Graphics.DrawString(hh.Text, new Font("Century Gothic", 17, FontStyle.Bold), Brushes.Black, new PointF(220, 140));
                e.Graphics.DrawString(hhh.Text, new Font("Century Gothic", 14, FontStyle.Bold), Brushes.Black, new PointF(260, 180));
                e.Graphics.DrawString("*********************************************************************", new Font("Century Gothic", 14, FontStyle.Bold), Brushes.Black, new PointF(80, 200));

                if (comboBox1.SelectedIndex == -1 || comboBox1.SelectedIndex == 0)
                {
                    e.Graphics.DrawString("Deposited Record", new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Black, new PointF(220, 250));

                    e.Graphics.DrawString(" Enrollment No    :-" + textBox1.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(100, 320));
                    e.Graphics.DrawString(" Student Name     :-" + textBox2.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(100, 360));
                    e.Graphics.DrawString(" Faculty          :-" + textBox3.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(100, 400));
                    e.Graphics.DrawString(" Semester         :-" + textBox4.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(100, 440));
                    e.Graphics.DrawString(" Book ID          :-" + textBox11.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(100, 480));
                    e.Graphics.DrawString(" Book Name        :-" + textBox14.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(100, 520));
                    e.Graphics.DrawString(" Date of Issue    :-" + dateTimePicker3.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(100, 560));
                    e.Graphics.DrawString(" Date Tobe Return :-" + dateTimePicker4.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(100, 600));
                    e.Graphics.DrawString(" Date of Return   :-" + dateTimePicker5.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(100, 640));
                    e.Graphics.DrawString(" Total Fine       :-" + fine.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(100, 680));

                }
                else
                {
                    e.Graphics.DrawString("Issued Record", new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Black, new PointF(220, 250));

                    e.Graphics.DrawString(" Enrollment No    :-" + textBox1.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(100, 320));
                    e.Graphics.DrawString(" Student Name     :-" + textBox2.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(100, 360));
                    e.Graphics.DrawString(" Faculty          :-" + textBox3.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(100, 400));
                    e.Graphics.DrawString(" Semester         :-" + textBox4.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(100, 440));
                    e.Graphics.DrawString(" Book ID          :-" + textBox6.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(100, 480));
                    e.Graphics.DrawString(" Book Name        :-" + textBox7.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(100, 520));
                    e.Graphics.DrawString(" Category         :-" + textBox8.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(100, 560));
                    e.Graphics.DrawString(" Date of Issue    :-" + dateTimePicker3.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(100, 600));
                    e.Graphics.DrawString(" Date Tobe Return :-" + dateTimePicker4.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(100, 640));
                    e.Graphics.DrawString(" Date of Return   :-" + dateTimePicker5.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(100, 700));

                }
            }
            catch (IOException)
            {
                MessageBox.Show("Unable to print the record");
            }




        }

        private void printDocument2_PrintPage(object sender, PrintPageEventArgs e)
        {
          //  e.Graphics.DrawImage(Bitmap, 0, 0);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel20_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rateoffine_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("This field should contain digits");
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            DateTime d1 = dateTimePicker4.Value.Date;
            DateTime d2 = dateTimePicker5.Value.Date;
            TimeSpan ts = d2 - d1;
            int days = ts.Days;
            if (days > 0)
            {
                int d = int.Parse(textBox15.Text);
                days = days * d;
                fine.Text = days.ToString();
            }
            else
            {
                MessageBox.Show("Date of deposit cannot be earlier than Date to be return");
            }

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                if (listBox2.Items.Count > listBox1.SelectedIndex)
                {
                    listBox2.SelectedIndex = listBox1.SelectedIndex;
                    textBox11.Text = listBox1.SelectedItem.ToString();
                    int index = listBox1.SelectedIndex;
                    textBox14.Text = listBox2.Items[index].ToString();
                }
            }

            dtime();

        }

        private void listBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex > -1)
            {
                if (listBox1.Items.Count > listBox1.SelectedIndex)
                {
                    listBox1.SelectedIndex = listBox2.SelectedIndex;
                    textBox11.Text = listBox1.SelectedItem.ToString();
                    int index = listBox1.SelectedIndex;
                    textBox14.Text = listBox2.Items[index].ToString();
                }
            }
            dtime();
        }
    }
}
