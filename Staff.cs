using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace Library_Management_System
{
    public partial class Staff : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd;
        public Staff()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");
            lastvalue();
            display();
            pictureBox4.Image = Properties.Resources.sd;
        }
        public void display()
        {
            lastvalue();
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = 0;
            cmd = new SqlCommand("select * from Staff", con);
            SqlDataReader dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (dr.Read())
            {
                i = i + 1;
                dataGridView1.Rows.Add(i.ToString(), dr["Staff_ID"].ToString(), dr["Name"].ToString(), dr["Address"].ToString(), dr["Shift"].ToString(), dr["Post"].ToString(), dr["Contact"].ToString(), dr["Teach_time"].ToString(), dr["Email"].ToString(), dr["Picture"]);
            }
            dr.Close();
            con.Close();


        }
        public void refresh()
        {
            enroll.Text = "";
            name.Text = "";
            faculty.Text = "";
            sem.Text = "";
            roll.Text = "";
            contact.Text = "";
            dob.Text = "";
            email.Text = "";
            pictureBox4.Image = Properties.Resources.sd;
        }
        private void btnBrowser_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    //nam.Text = ofd.FileName.ToString();

                    pictureBox4.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        private void insert_Click(object sender, EventArgs e)
        {
            try
            {

                SqlDataAdapter adap = new SqlDataAdapter("select * from Staff where Staff_ID='" + enroll.Text + "'", con);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Data Already Exists");

                }
                else
                {
                    Image img = pictureBox4.Image;

                    byte[] arr;
                    ImageConverter converter = new ImageConverter();
                    arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Staff values(@id,@name,@fac,@sem,@roll,@cont,@dob,@em,@img)", con);
                    cmd.Parameters.AddWithValue("@id", enroll.Text);
                    cmd.Parameters.AddWithValue("@name", name.Text);
                    cmd.Parameters.AddWithValue("@fac", faculty.Text);
                    cmd.Parameters.AddWithValue("@sem", sem.Text);
                    cmd.Parameters.AddWithValue("@roll", roll.Text);
                    cmd.Parameters.AddWithValue("@cont", contact.Text);
                    cmd.Parameters.AddWithValue("@dob", dob.Text);
                    cmd.Parameters.AddWithValue("@em", email.Text);
                    cmd.Parameters.AddWithValue("@img", arr);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record inserted sucessfully");
                    con.Close();
                   display();
                    refresh();
                    lastvalue();



                }
            }
            catch
            {
                MessageBox.Show("Error in Inserting data");
            }

          
        }

        private void menu_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Close();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Staff where Staff_ID='" + enroll.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record deleted Sucessfully");
                refresh();
                display();
                lastvalue();
            }
            catch (IOException)
            {
                MessageBox.Show("Error in deleting Record");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[1].Value == null)
                {
                    refresh();
                }
                else
                {
                    try
                    {

                        enroll.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                        name.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                        faculty.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                        sem.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                        roll.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                        contact.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                        dob.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                        email.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                        if (dataGridView1.Rows[e.RowIndex].Cells[9].Value == null)
                        {
                            pictureBox4.Image = null;
                        }
                        else
                        {
                            byte[] imgdata = (byte[])dataGridView1.CurrentRow.Cells[9].Value;
                            MemoryStream ms = new MemoryStream(imgdata);
                            pictureBox4.Image = Image.FromStream(ms);
                        }
                    }catch(ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Please select proper cell");
                    }
                }
            }catch(ArgumentOutOfRangeException)
            {
                MessageBox.Show("Error");
            }
        }
        public void updatestaff()
        {
            Image img = pictureBox4.Image;

            byte[] arr;
            ImageConverter converter = new ImageConverter();
            arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
            con.Open();
            SqlCommand cmd = new SqlCommand("update  Staff set Staff_ID=@id ,  Name=@name,Address=@fac, Shift=@sem, Post=@roll,Contact=@cont,Teach_time=@dob,Email=@em , Picture=@img where Staff_ID='" + enroll.Text + "'", con);
            cmd.Parameters.AddWithValue("@id", enroll.Text);
            cmd.Parameters.AddWithValue("@name", name.Text);
            cmd.Parameters.AddWithValue("@fac", faculty.Text);
            cmd.Parameters.AddWithValue("@sem", sem.Text);
            cmd.Parameters.AddWithValue("@roll", roll.Text);
            cmd.Parameters.AddWithValue("@cont", contact.Text);
            cmd.Parameters.AddWithValue("@dob", dob.Text);
            cmd.Parameters.AddWithValue("@em", email.Text);
            cmd.Parameters.AddWithValue("@img", arr);
            cmd.ExecuteNonQuery();
        }
        private void update_Click(object sender, EventArgs e)
        {

            try
            {
                updatestaff();
                MessageBox.Show("Record updated sucessfully");
                refresh();
                con.Close();

                display();
            }
            catch (IOException)
            {
                MessageBox.Show("Error in updating data");
            }


            

         
        }

        private void Staff_Load(object sender, EventArgs e)
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
                if (con.State == ConnectionState.Closed)
                    con.Open();
                int i = 0;
                if (comboBox1.SelectedIndex == -1)
                {

                    cmd = new SqlCommand("select * from Staff where Staff_ID like ('%" + textBox1.Text + "%')", con);


                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    cmd = new SqlCommand("select * from Staff where Name like ('%" + textBox1.Text + "%')", con);

                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    cmd = new SqlCommand("select * from Staff where Address like ('%" + textBox1.Text + "%')", con);

                }
                else if (comboBox1.SelectedIndex == 3)
                {
                    cmd = new SqlCommand("select * from Staff where Post like ('%" + textBox1.Text + "%')", con);

                }
                else
                {

                    cmd = new SqlCommand("select * from Staff where Staff_ID like ('%" + textBox1.Text + "%')", con);
                }
                SqlDataReader dr = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (dr.Read())
                { 
                    i = i + 1;
                dataGridView1.Rows.Add(i.ToString(), dr["Staff_ID"].ToString(), dr["Name"].ToString(), dr["Address"].ToString(), dr["Shift"].ToString(), dr["Post"].ToString(), dr["Contact"].ToString(), dr["Teach_time"].ToString(), dr["Email"].ToString(), dr["Picture"]);
                }
                dr.Close();
                con.Close();

            }
            catch (IOException)
            {
                MessageBox.Show("Error in showing data");
            }

        }
        public void lastvalue()
        {

            int c = dataGridView1.RowCount - 1;
            label11.Text = c.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void enroll_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("This field should contain digits");
            }
        }
    }
}
