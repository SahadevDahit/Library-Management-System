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
    public partial class Books : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd;
       public Books()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\lb.mdf;Integrated Security=True;Connect Timeout=30");

            display();
        }
        public void display()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = 0;
            cmd = new SqlCommand("select * from Book_Records", con);
            SqlDataReader dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (dr.Read())
            {
                i = i + 1;
                dataGridView1.Rows.Add(i.ToString(), dr["bookid"].ToString(), dr["bookname"].ToString(), dr["category"].ToString(), dr["semester"].ToString(), dr["price"].ToString(), dr["quantity"].ToString(),
                dr["status"].ToString(), dr["author"].ToString(), dr["edition"].ToString());
            }
            dr.Close();
            con.Close();


        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Close();
        }
        public void refresh()
        {
            bid.Text = "";
            nam.Text = "";
            catego.Text = "";
            semes.Text = "";
            pric.Text = "";
            quantity.Text = "";
            stats.Text = "";
            auth.Text = "";
            editon.Text = "";
        }
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells["sn"].Value = (e.RowIndex + 1).ToString();
        }

        private void insert_Click(object sender, EventArgs e)
        {
            if (bid.Text!="" && nam.Text!="" && catego.Text!="" && quantity.Text!="")
            {


                try
                {

                    SqlDataAdapter adap = new SqlDataAdapter("select * from Book_Records where bookid='" + bid.Text + "'", con);
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Data Already Exists");

                    }
                    else
                    {

                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "insert into Book_Records values('" + bid.Text + "','" + nam.Text + "','" + catego.Text + "', '" + semes.Text + "','" + pric.Text + "', '" + quantity.Text + "','" + stats.Text + "', '" + auth.Text + "', '" + editon.Text + "')";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Record Inserted Sucessfully");
                        display();
                        refresh();



                    }
                }
                catch
                {
                    MessageBox.Show("Error in Inserting data");
                }

            }
            else
            {
                MessageBox.Show("Book id , Book name , Category and quantity are compulsory filled");
            }
        }

        private void update_Click(object sender, EventArgs e)
        {

            if (bid.Text != "" && nam.Text != "" && catego.Text != "" && quantity.Text != "")
            {
               
                       con.Open();

                        cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update Book_Records set bookid='" + bid.Text + "',bookname='" + nam.Text + "',category='" + catego.Text + "',  semester='" + semes.Text + "', price = '" + pric.Text + "',quantity = '" + quantity.Text + "',status = '" + stats.Text + "',author = '" + auth.Text + "',edition = '" + editon.Text + "' where bookid='" + bid.Text + "' ";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Record updated Sucessfully");
                        display();
                        refresh();

            }
            else
            {
                MessageBox.Show("Book id , Book name , Category and quantity are compulsory filled");
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Book_Records where bookid='" + bid.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record deleted Sucessfully");
            refresh();
            display();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[1].Value == null)
            {
                refresh();
            }
            else
            {
                bid.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                nam.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                catego.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                semes.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                pric.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                quantity.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                stats.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                auth.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                editon.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            }
        }

        private void Books_Load(object sender, EventArgs e)
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


            // TODO: This line of code loads data into the 'library_projectDataSet.Book_Records' table. You can move, or remove it, as needed.
          //  this.book_RecordsTableAdapter.Fill(this.library_projectDataSet.Book_Records);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void quantity_TextChanged(object sender, EventArgs e)
        {
              if (quantity.Text == "")
            {
                stats.Text = "";
            }
            else if (int.Parse(quantity.Text)>0)
            {
                stats.Text = "active";
            }
            else if(int.Parse(quantity.Text) ==0)
            {
                stats.Text = "inactive";
            }
           
            else
            {
                
            }
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            try { 

            if (con.State == ConnectionState.Closed)
                    con.Open();
                int i = 0;

                if (comboBox1.SelectedIndex == -1)
                {
                    cmd = new SqlCommand("select * from Book_Records where bookid like ('%" + search.Text + "%')", con);

                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    cmd = new SqlCommand("select * from Book_Records where bookname like ('%" + search.Text + "%')", con);

                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    cmd = new SqlCommand("select * from Book_Records where category like ('%" + search.Text + "%')", con);

                }
                else if (comboBox1.SelectedIndex == 3)
                {
                    cmd = new SqlCommand("select * from Book_Records where semester like ('%" + search.Text + "%')", con);

                }
                else
                {
                    cmd = new SqlCommand("select * from Book_Records where bookid like ('%" + search.Text + "%')", con);
                }
                SqlDataReader dr = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (dr.Read())
                {
                    i = i + 1;
                    dataGridView1.Rows.Add(i.ToString(), dr["bookid"].ToString(), dr["bookname"].ToString(), dr["category"].ToString(), dr["semester"].ToString(), dr["price"].ToString(), dr["quantity"].ToString(),
                    dr["status"].ToString(), dr["author"].ToString(), dr["edition"].ToString());
                }
                dr.Close();
                con.Close();


            }
            catch(InvalidOperationException)
            {
                MessageBox.Show("Error in showing data");
            }

}

        private void semes_TextChanged(object sender, EventArgs e)
        {

        }

        private void pric_TextChanged(object sender, EventArgs e)
        {

        }

        private void semes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("This field should contain digits");
            }
        }

        private void pric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("This field should contain digits");
            }
        }

        private void quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("This field should contain digits");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
