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
    public partial class Form6 : Form
    {
        public Form6(int Max2, int Max3)
        {
            InitializeComponent();
            Form1 main = this.Owner as Form1;
            label8.Text = null;

            numericUpDown3.Maximum = Max3;
            numericUpDown2.Maximum = Max2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1;
            main.show();
            this.Close();
        }

        private void label7_MouseMove(object sender, MouseEventArgs e)
        {
            label7.BackColor = DefaultBackColor;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1;
            int id = Convert.ToInt32(numericUpDown3.Value) - 1;
            bool k = false;
            for (Int16 i = 4; i < 14; i++)
                if (main.Vendors.Rows[id].Cells[i].Value != null)
                    if (main.Vendors.Rows[id].Cells[i].Value != DBNull.Value)
                        if (Convert.ToInt32(main.Vendors.Rows[id].Cells[i].Value) == Convert.ToInt32(numericUpDown2.Value))
                            k = true;
            if (k == false)
            {
                button3.Enabled = false;
                label8.Text = null;
                return;
            }

            button3.Enabled = true;
            label8.Text = Convert.ToString(main.priceOfDetail(Convert.ToInt32(numericUpDown2.Value) - 1) * Convert.ToInt32(numericUpDown1.Value));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1;
            DataSet dsD = new DataSet();
            string commandText = "INSERT INTO [Supply]" +
            "VALUES ('" + dateTimePicker1.Value + "', '" + numericUpDown2.Value + "', '" + numericUpDown3.Value + "', '" + numericUpDown1.Value + "')";
            main.My_Execute_Non_Querty(commandText);
            main.show();
            this.Close();
        }
    }
}
