using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Kyrsovaya_Sazeev_Anodin
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            Form4 main = this.Owner as Form4;
            string ConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Hippo\\Documents\\Data_Base.mdb";
            string CmdTextDetail = "SELECT * FROM [detail]";
            OleDbDataAdapter dAD = new OleDbDataAdapter(CmdTextDetail, ConnString);
            DataSet dsD = new DataSet();
            dAD.Fill(dsD, "[detail]");
            Details.DataSource = dsD.Tables[0].DefaultView;
            {
                Details.Columns[0].ReadOnly = false;
                Details.Columns[1].ReadOnly = true;
                Details.Columns[2].ReadOnly = true;
                Details.Columns[3].ReadOnly = true;
                Details.Columns[4].ReadOnly = true;
                Details.Columns[1].Width = 50;
                Details.Columns[2].Width = 250;
                Details.Columns[4].Width = 200;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 main = this.Owner as Form4;
            int c = 0;
            for (int i = 0; i < Details.Rows.Count; i++)
                if (Convert.ToBoolean(Details.Rows[i].Cells[0].Value) == true)
                {
                    main.IDs[c] = Convert.ToInt32(Details.Rows[i].Cells[1].Value);
                    c++;
                }
            if (c != 0 && c <= 10)
                this.Close();
            label1.BackColor = Color.Salmon;
        }
    }
}
