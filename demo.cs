using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class demo : Form
    {
        public demo()
        {
            InitializeComponent();
            display();  
        }
        public void display()
        {
            DateTime d1 = DateTime.Now;
            label1.Text = d1.ToLongDateString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now;
            string d1 = d.ToLongDateString();
            DateTime d2 = dateTimePicker1.Value.Date;
            label1.Text = d1;
            label2.Text = d2.ToLongDateString();
            DateTime a = Convert.ToDateTime(label1.Text);
            DateTime b = Convert.ToDateTime(label2.Text);
            int sd = Convert.ToInt32((b - a).TotalDays);
            MessageBox.Show(sd.ToString());
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
