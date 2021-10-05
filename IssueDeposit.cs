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
        public void lst()
        {
            try
            {
                if (comboBox2.SelectedIndex == 1)
                {
                    con.Open();
                    SqlCommand ccm = new SqlCommand("select * from issue_teacher where teacherid=@id", con);
                    ccm.Parameters.AddWithValue("@id", textBox12.Text);
                    SqlDataReader dd = ccm.ExecuteReader();
                    listBox1.Items.Clear();
                    listBox2.Items.Clear();
                    while (dd.Read())
                    {

                        listBox1.Items.Add(dd.GetValue(4).ToString());
                        listBox2.Items.Add(dd.GetValue(5).ToString());


                    }
                    dd.Close();
                    con.Close();
                }
                else
                {


                    con.Open();
                    SqlCommand ccm = new SqlCommand("select * from issue_student where Enroll=@id", con);
                    ccm.Parameters.AddWithValue("@id", textBox12.Text);
                    SqlDataReader dd = ccm.ExecuteReader();
                    listBox1.Items.Clear();
                    listBox2.Items.Clear();
                    while (dd.Read())
                    {

                        listBox1.Items.Add(dd.GetValue(5).ToString());
                        listBox2.Items.Add(dd.GetValue(6).ToString());


                    }
                    dd.Close();
                    con.Close();
                }
            }
            catch (IOException)
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox12.Text == "")
            {
                MessageBox.Show("Enter the Enrollment No ");
            }
            else
            {
                if (comboBox2.SelectedIndex == 1)
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from teacher where Teacher_ID='" + textBox12.Text + "'";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    int i = 0;
                    i = int.Parse(dt.Rows.Count.ToString());
                    if (i == 0)
                    {
                        MessageBox.Show("Teacher  No entered is not available");
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            textBox1.Text = dr["Teacher_ID"].ToString();
                            textBox2.Text = dr["Name"].ToString();
                            textBox3.Text = dr["faculty"].ToString();
                            textBox4.Text = dr["Post"].ToString();
                            textBox5.Text = dr["Contact"].ToString();
                            totalbook.Text = dr["noofbook"].ToString();

                            byte[] imgdata = (byte[])dr["Picture"];

                            MemoryStream ms = new MemoryStream(imgdata);
                            pictureBox3.Image = Image.FromStream(ms);

                        }

                    }
                    con.Close();
                    lst();
                }

                else

                {



                    con.Open();
                    SqlCommand cmdd = con.CreateCommand();
                    cmdd.CommandType = CommandType.Text;
                    cmdd.CommandText = "select * from Student where Enroll='" + textBox12.Text + "'";
                    cmdd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmdd);
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
                            totalbook.Text = dr["noofbook"].ToString();
                            byte[] imgdata = (byte[])dr["Picture"];

                            MemoryStream ms = new MemoryStream(imgdata);
                            pictureBox3.Image = Image.FromStream(ms);



                        }



                    }
                    con.Close();
                    lst();


                }

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
                        textBox8.Text = dr["faculty"].ToString();
                        textBox9.Text = dr["quantity"].ToString();
                        textBox10.Text = dr["status"].ToString();

                    }
                }
                con.Close();
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            IssueTable i = new IssueTable();
            i.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Deposit d = new Deposit();
            d.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {



            con.Close();
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


                        int limit = 0;
                        con.Open();
                        SqlCommand ccm = new SqlCommand("select * from booklimit ", con);
                        SqlDataReader dd = ccm.ExecuteReader();

                        while (dd.Read())
                        {

                            string s = dd.GetValue(1).ToString();
                            limit = int.Parse(s);


                        }
                        dd.Close();
                        con.Close();






                        if (int.Parse(totalbook.Text) < limit)
                        {
                            DateTime d1 = dateTimePicker1.Value.Date;
                            DateTime d2 = dateTimePicker2.Value.Date;
                            TimeSpan ts = d2 - d1;
                            int days = ts.Days;
                            if (days > 0)
                            {

                                try
                                {

                                    if (comboBox2.SelectedIndex == 1)
                                    {
                                        //insert value in issue teacher



                                        Image img = pictureBox3.Image;

                                        byte[] arr;
                                        ImageConverter converter = new ImageConverter();
                                        arr = (byte[])converter.ConvertTo(img, typeof(byte[]));

                                        con.Open();
                                        SqlCommand cmd = new SqlCommand("insert into issue_teacher values(@id,@name,@fac,@sem,@roll,@cont,@dob,@em,@img)", con);
                                        cmd.Parameters.AddWithValue("@id", textBox1.Text);
                                        cmd.Parameters.AddWithValue("@name", textBox2.Text);
                                        cmd.Parameters.AddWithValue("@fac", textBox3.Text);
                                        cmd.Parameters.AddWithValue("@sem", textBox6.Text);
                                        cmd.Parameters.AddWithValue("@roll", textBox7.Text);
                                        cmd.Parameters.AddWithValue("@cont", textBox8.Text);
                                        cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Text);
                                        cmd.Parameters.AddWithValue("@em", dateTimePicker2.Text);
                                        cmd.Parameters.AddWithValue("@img", arr);
                                        cmd.ExecuteNonQuery();
                                        con.Close();


                                        //updating no of issued book to Teacher
                                        con.Open();
                                        int t = int.Parse(totalbook.Text);
                                        t = t + 1;

                                        SqlCommand cmm = con.CreateCommand();
                                        cmm.CommandType = CommandType.Text;
                                        cmm.CommandText = "update Teacher set  noofbook='" + t + "' where Teacher_ID='" + textBox12.Text + "' ";
                                        cmm.ExecuteNonQuery();
                                        con.Close();
                                        totalbook.Text = t.ToString();


                                    }
                                    else
                                    {

                                        Image img = pictureBox3.Image;

                                        byte[] arr;
                                        ImageConverter converter = new ImageConverter();
                                        arr = (byte[])converter.ConvertTo(img, typeof(byte[]));


                                        //inserting the data into issue_student table
                                        con.Open();
                                        SqlCommand cmd = new SqlCommand("insert into issue_student values(@id,@name,@fac,@sem,@roll,@cont,@dob,@em,@img)", con);
                                        cmd.Parameters.AddWithValue("@id", textBox1.Text);
                                        cmd.Parameters.AddWithValue("@name", textBox2.Text);
                                        cmd.Parameters.AddWithValue("@fac", textBox3.Text);
                                        cmd.Parameters.AddWithValue("@sem", textBox4.Text);
                                        cmd.Parameters.AddWithValue("@roll", textBox6.Text);
                                        cmd.Parameters.AddWithValue("@cont", textBox7.Text);
                                        cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Text);
                                        cmd.Parameters.AddWithValue("@em", dateTimePicker2.Text);
                                        cmd.Parameters.AddWithValue("@img", arr);
                                        cmd.ExecuteNonQuery();
                                        con.Close();





                                        //updating no of issued book to student
                                        con.Open();
                                        int t = int.Parse(totalbook.Text);
                                        t = t + 1;

                                        SqlCommand cmm = con.CreateCommand();
                                        cmm.CommandType = CommandType.Text;
                                        cmm.CommandText = "update Student set  noofbook='" + t + "' where Enroll='" + textBox12.Text + "' ";
                                        cmm.ExecuteNonQuery();
                                        con.Close();
                                        totalbook.Text = t.ToString();


                                    }




                                    //updating book quantity
                                    con.Open();
                                    int y = int.Parse(textBox9.Text);
                                    y = y - 1;
                                    SqlCommand cm = con.CreateCommand();
                                    cm.CommandType = CommandType.Text;
                                    cm.CommandText = "update Book_Records set  quantity='" + y + "' where bookid='" + textBox13.Text + "' ";
                                    cm.ExecuteNonQuery();
                                    con.Close();
                                    textBox9.Text = y.ToString();

                                    MessageBox.Show("Book issued  Sucessfully");
                                    lst();
                                }
                                catch (InvalidOperationException)
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
                            MessageBox.Show("You cannot issue more than limit of book to the same student");
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
        public void dtime()
        {

            try
            {
                if (comboBox2.SelectedIndex==1)
                {
                    con.Open();
                    SqlCommand ccm = new SqlCommand("select * from issue_teacher where bookid=@id and bookname=@nam", con);
                    ccm.Parameters.AddWithValue("@id", textBox11.Text);
                    ccm.Parameters.AddWithValue("@nam", textBox14.Text);
                    SqlDataReader dd = ccm.ExecuteReader();

                    while (dd.Read())
                    {
                        dateTimePicker3.Value = DateTime.Parse(dd.GetValue(7).ToString());
                        dateTimePicker4.Value = DateTime.Parse(dd.GetValue(8).ToString());

                    }
                    dd.Close();
                    con.Close();
                }
                else
                {


                    con.Open();
                    SqlCommand ccm = new SqlCommand("select * from issue_student where bookid=@id and bookname=@nam", con);
                    ccm.Parameters.AddWithValue("@id", textBox11.Text);
                    ccm.Parameters.AddWithValue("@nam", textBox14.Text);
                    SqlDataReader dd = ccm.ExecuteReader();

                    while (dd.Read())
                    {
                        dateTimePicker3.Value = DateTime.Parse(dd.GetValue(7).ToString());
                        dateTimePicker4.Value = DateTime.Parse(dd.GetValue(8).ToString());

                    }
                    dd.Close();
                    con.Close();
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Error");
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex > -1)
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

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("This field should contain digits");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DateTime ddd = dateTimePicker3.Value.Date;
            DateTime d1 = dateTimePicker4.Value.Date;
            DateTime d2 = dateTimePicker5.Value.Date;
            TimeSpan ts = d2 - d1;
            TimeSpan ts1 = d2 - ddd;
            int dd = ts1.Days;
            
            int days = ts.Days;
            if (textBox15.Text != "")
            {
                if (days >= 0 && dd>0)
                {
                    int d = int.Parse(textBox15.Text);
                    days = days * d;
                    fine.Text = days.ToString();
                }
                else
                {
                    // MessageBox.Show("Date of deposit cannot be earlier than Date to be return");
                    textBox15.Text = null;
                }
            }
            else
            {
                MessageBox.Show("Enter the fine per day");
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox11.Text != "")
            {
                try
                {
                    //updating book record
                    con.Open();
                    SqlCommand asd = new SqlCommand("select * from Book_Records where bookid='" + textBox11.Text + "'", con);
                    SqlDataReader sa = asd.ExecuteReader();
                    while (sa.Read())

                    {
                        int y = int.Parse(sa.GetValue(6).ToString());
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

                    if (comboBox2.SelectedIndex == 1)
                    {
                        //deleting record from issue teacher 
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "delete from issue_teacher where teacherid='" + textBox1.Text + "' and bookid='" + textBox11.Text + "'";
                        cmd.ExecuteNonQuery();
                        con.Close();



                        //updating noofbook issued to student
                        con.Open();
                        int x = int.Parse(totalbook.Text);
                        x = x - 1;
                        SqlCommand cmm = con.CreateCommand();
                        cmm.CommandType = CommandType.Text;
                        cmm.CommandText = "update Teacher set  noofbook='" + x + "'  where Teacher_ID='" + textBox1.Text + "' ";
                        cmm.ExecuteNonQuery();
                        totalbook.Text = x.ToString();
                        con.Close();

                        //insert value in deposit teacher
                        con.Open();
                        Image img = pictureBox3.Image;

                        byte[] arr;
                        ImageConverter converter = new ImageConverter();
                        arr = (byte[])converter.ConvertTo(img, typeof(byte[]));


                        cmd = new SqlCommand("insert into deposit_teacher values(@id,@name,@fac,@sem,@bid,@bname,@d1,@d2,@d3,@fine,@pic)", con);
                        cmd.Parameters.AddWithValue("@id", textBox1.Text);
                        cmd.Parameters.AddWithValue("@name", textBox2.Text);
                        cmd.Parameters.AddWithValue("@fac", textBox3.Text);
                        cmd.Parameters.AddWithValue("@sem", textBox4.Text);
                        cmd.Parameters.AddWithValue("@bid", textBox11.Text);
                        cmd.Parameters.AddWithValue("@bname", textBox14.Text);

                        cmd.Parameters.AddWithValue("@d1", dateTimePicker3.Text);
                        cmd.Parameters.AddWithValue("@d2", dateTimePicker4.Text);
                        cmd.Parameters.AddWithValue("@d3", dateTimePicker5.Text);
                        cmd.Parameters.AddWithValue("@fine", fine.Text);
                        cmd.Parameters.AddWithValue("@pic", arr);

                        cmd.ExecuteNonQuery();
                        con.Close();



                    }
                    else
                    {
                        //deleting record from issue student
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "delete from issue_student where Enroll='" + textBox1.Text + "' and bookid='" + textBox11.Text + "'";
                        cmd.ExecuteNonQuery();
                        con.Close();



                        //updating noofbook issued to student
                        con.Open();
                        int x = int.Parse(totalbook.Text);
                        x = x - 1;
                        SqlCommand cmm = con.CreateCommand();
                        cmm.CommandType = CommandType.Text;
                        cmm.CommandText = "update Student set  noofbook='" + x + "'  where Enroll='" + textBox1.Text + "' ";
                        cmm.ExecuteNonQuery();
                        totalbook.Text = x.ToString();
                        con.Close();



                        //insert into issue deposit of student
                        con.Open();
                        Image img = pictureBox3.Image;

                        byte[] arr;
                        ImageConverter converter = new ImageConverter();
                        arr = (byte[])converter.ConvertTo(img, typeof(byte[]));


                        cmd = new SqlCommand("insert into deposit_student values(@id,@name,@fac,@sem,@bid,@bname,@d1,@d2,@d3,@fine,@pic)", con);
                        cmd.Parameters.AddWithValue("@id", textBox1.Text);
                        cmd.Parameters.AddWithValue("@name", textBox2.Text);
                        cmd.Parameters.AddWithValue("@fac", textBox3.Text);
                        cmd.Parameters.AddWithValue("@sem", textBox4.Text);
                        cmd.Parameters.AddWithValue("@bid", textBox11.Text);
                        cmd.Parameters.AddWithValue("@bname", textBox14.Text);

                        cmd.Parameters.AddWithValue("@d1", dateTimePicker3.Text);
                        cmd.Parameters.AddWithValue("@d2", dateTimePicker4.Text);
                        cmd.Parameters.AddWithValue("@d3", dateTimePicker5.Text);
                        cmd.Parameters.AddWithValue("@fine", fine.Text);
                        cmd.Parameters.AddWithValue("@pic", arr);

                        cmd.ExecuteNonQuery();
                        con.Close();

                    }


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
        picprint p= new picprint();
        private void button11_Click(object sender, EventArgs e)
        {
            int count = 0;
        
            string ss = "https://github.com/SahadevDahit/Library-Management-System" + " \n\n\n https://sahadevdahit.github.io/Webpage/";
            Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;

            p.pictureBox1.Image = qrcode.Draw(ss, 100);

            if (comboBox1.SelectedIndex == -1 || comboBox1.SelectedIndex == 0)
            {
                   if (textBox1.Text != "" && textBox11.Text != "")
                    {
                        foreach (string item in listBox1.Items)
                        {
                            if (textBox11.Text == item)
                                count++;

                        }

                    if (count == 0)
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
                        MessageBox.Show("Deposit Book at first.");
                    }
                }else
                {
                    MessageBox.Show("Select the proper book from issued list to be diposited");
                }
               

            }

            else if (textBox1.Text != "" && textBox6.Text != "")
            {


                
                foreach (string item in listBox1.Items)
                {
                    if (textBox6.Text ==item)
                        count++;

                }
                if (count == 1)
                {
                    this.Hide();
                    /* PrintDialog pd = new PrintDialog();
                     PrintDocument doc = new PrintDocument();
                     doc.PrintPage += printcode;
                     pd.Document = doc;
                     if (pd.ShowDialog() == DialogResult.OK)
                     {

                         doc.Print();
                     }*/
                    if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.Print();
                    }
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Issue the book first");

                }
                this.Show();
            }else
            {
                MessageBox.Show("Please fill the Record of Enrollment no and Book id");

                }
           
        }
        private void printcode(object sender,PrintPageEventArgs e)
        {
           
        }

        public void refreshtwo()
        {
            textBox13.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
        }
        public void refreshone()
        {
            textBox12.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            pictureBox3.Image = null;
            totalbook.Text = "";
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            textBox11.Text = "";
            textBox14.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            refreshtwo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            refreshone();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            totalbook.Text = "";
            if (comboBox2.SelectedIndex == 1)
            {
                label2.Text = "Teacher ID";
                label3.Text = "Teacher Name";
                label4.Text = "Faculty";
                label5.Text = "Post";
                refreshone();
                textBox12.Text = "";
                pictureBox3.Image = null;
            }
            else
            {
                label2.Text = "Enrollment No";
                label3.Text = "Student Name";
                label4.Text = "Faculty";
                label5.Text = "Semester";
                refreshone();
                textBox12.Text = "";
                pictureBox3.Image = null;



            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void IssueDeposit_Load(object sender, EventArgs e)
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

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                Bitmap bmp = new Bitmap(p.pictureBox1.Height, p.pictureBox1.Width);
                p.pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, p.pictureBox1.Width, p.pictureBox1.Height));
                e.Graphics.DrawImage(bmp, 600, 300);
                bmp.Dispose();

                e.Graphics.DrawString("*******************************************************************", new Font("Century Gothic", 14, FontStyle.Bold), Brushes.Black, new PointF(80, 80));

                e.Graphics.DrawString(h.Text, new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Black, new PointF(250, 100));
                e.Graphics.DrawString(hh.Text, new Font("Century Gothic", 17, FontStyle.Bold), Brushes.Black, new PointF(290, 140));
                e.Graphics.DrawString(hhh.Text, new Font("Century Gothic", 14, FontStyle.Bold), Brushes.Black, new PointF(330, 180));
                e.Graphics.DrawString("*********************************************************************", new Font("Century Gothic", 14, FontStyle.Bold), Brushes.Black, new PointF(80, 200));

                if (comboBox1.SelectedIndex == -1 || comboBox1.SelectedIndex == 0)
                {
                    e.Graphics.DrawString("Deposited Record", new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Black, new PointF(220, 250));
                    if (comboBox2.SelectedIndex == 1)
                    {
                        e.Graphics.DrawString("Teacher", new Font("Century Gothic", 16, FontStyle.Bold), Brushes.Black, new PointF(260, 300));

                        e.Graphics.DrawString(" Teacher ID       :-" + textBox1.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 400));
                        e.Graphics.DrawString(" Teacher Name     :-" + textBox2.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 440));
                        e.Graphics.DrawString(" Faculty          :-" + textBox3.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 480));
                        e.Graphics.DrawString(" Post             :-" + textBox4.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 520));

                    }
                    else
                    {
                        e.Graphics.DrawString("Student", new Font("Century Gothic", 16, FontStyle.Bold), Brushes.Black, new PointF(260, 300));

                        e.Graphics.DrawString(" Enrollment No    :-" + textBox1.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 400));
                        e.Graphics.DrawString(" Student Name     :-" + textBox2.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 440));
                        e.Graphics.DrawString(" Faculty          :-" + textBox3.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 480));
                        e.Graphics.DrawString(" Semester         :-" + textBox4.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 520));

                    }
                    e.Graphics.DrawString(" Book ID          :-" + textBox11.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 560));
                    e.Graphics.DrawString(" Book Name        :-" + textBox14.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 600));
                    e.Graphics.DrawString(" Date of Issue    :-" + dateTimePicker3.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 640));
                    e.Graphics.DrawString(" Date Tobe Return :-" + dateTimePicker4.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 680));
                    e.Graphics.DrawString(" Date of Return   :-" + dateTimePicker5.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 720));
                    e.Graphics.DrawString(" Total Fine       :-" + fine.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 760));
                    e.Graphics.DrawString("............................", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(550, 800));
                    e.Graphics.DrawString("Signature", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(575, 920));


                }
                else
                {
                    e.Graphics.DrawString("Issued Record", new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Black, new PointF(220, 250));
                    if (comboBox2.SelectedIndex == 1)
                    {
                        e.Graphics.DrawString("Teacher", new Font("Century Gothic", 16, FontStyle.Bold), Brushes.Black, new PointF(260, 3000));

                        e.Graphics.DrawString(" Teacher ID       :-" + textBox1.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 400));
                        e.Graphics.DrawString(" Teacher Name     :-" + textBox2.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 440));
                        e.Graphics.DrawString(" Faculty          :-" + textBox3.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 480));
                        e.Graphics.DrawString(" Post             :-" + textBox4.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 520));

                    }
                    else
                    {
                        e.Graphics.DrawString("Student", new Font("Century Gothic", 16, FontStyle.Bold), Brushes.Black, new PointF(260, 300));

                        e.Graphics.DrawString(" Enrollment No    :-" + textBox1.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 400));
                        e.Graphics.DrawString(" Student Name     :-" + textBox2.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 440));
                        e.Graphics.DrawString(" Faculty          :-" + textBox3.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 480));
                        e.Graphics.DrawString(" Semester         :-" + textBox4.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 520));

                    }
                    e.Graphics.DrawString(" Book ID          :-" + textBox6.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 560));
                    e.Graphics.DrawString(" Book Name        :-" + textBox7.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 600));
                    e.Graphics.DrawString(" Faculty          :-" + textBox8.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 640));
                    e.Graphics.DrawString(" Date of Issue    :-" + dateTimePicker3.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 680));
                    e.Graphics.DrawString(" Date Tobe Return :-" + dateTimePicker4.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 720));
                   // e.Graphics.DrawString(" Date of Return   :-" + dateTimePicker5.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 680));
                    e.Graphics.DrawString("............................", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(550, 900));
                    e.Graphics.DrawString("Signature", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new PointF(575, 920));



                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Unable to print the record");
            }

        }
    }
}
