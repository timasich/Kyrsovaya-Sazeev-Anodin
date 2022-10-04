using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kyrsovaya_Sazeev_Anodin
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1;
            main.number++;
            label6.Text = Convert.ToString(main.number);
        }

        public int[] IDs = new int[10];

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 frm = new Form5();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1;
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                if (textBox1.Text == "")
                    textBox1.BackColor = Color.Salmon;
                if (textBox2.Text == "")
                    textBox2.BackColor = Color.Salmon;
                return;
            }
            DataSet dsD = new DataSet();
            string commandText = "INSERT INTO [Vendor]" +
            "VALUES ('" + main.number + "', '" + textBox1.Text + "', '" + textBox2.Text + "', '" + numericUpDown1.Value + "', '" + IDs[0] +
             "', '" + IDs[1] + "', '" + IDs[2] + "', '" + IDs[3] + "', '" + IDs[4] + "', '" + IDs[5] + "', '" + IDs[6] + "', '" + IDs[7] +
             "', '" + IDs[8] + "', '" + IDs[9] + "')";
            main.My_Execute_Non_Querty(commandText);
            this.Close();
        }

        private void label7_MouseMove(object sender, MouseEventArgs e)
        {
            label7.BackColor = DefaultBackColor;
        }
    }
}
