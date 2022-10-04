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
    public partial class Form1 : Form
    {
        public bool modeType = true;
        public string ConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Hippo\\Documents\\Data_Base.mdb";
        public string CmdTextVendor = "SELECT * FROM [Vendor]";
        public string CmdTextDetail = "SELECT * FROM [detail]";
        public string CmdTextSupply = "SELECT * FROM [Supply]";

        public Form1()
        {
            InitializeComponent();
            mode();
            tabControl1.Height = this.Height - 40;
            tabControl1.Width = SystemInformation.PrimaryMonitorSize.Width;
            Vendors.Height = Details.Height = Supplies.Height = tabControl1.Height - 40;
            Vendors.Height = Vendors.Height - 84;
            detail_update();
            vendors_update();
            supply_update();
            for (int i = 4; i < 14; i++)
                this.Vendors.Columns[i].Visible = false;
            {
                Vendors.Columns[1].Width = 250;
                Vendors.Columns[2].Width = 300;
                Vendors.Columns[0].Width = 50;
                Details.Columns[0].Width = 50;
                Details.Columns[1].Width = 250;
                Details.Columns[3].Width = 300;
            }
            {
                label5.Text = label8.Text = label10.Text =
                label11.Text = label13.Text = label15.Text =
                label18.Text = label19.Text = label3.Text = null;
            }
        }

        private void mode()
        {
            Form2 frm = new Form2();
            frm.Owner = this;
            frm.ShowDialog();
            if (modeType == true)
            {
                tabPage3.Parent = tabControl1;
                New_Detail.Hide();
                New_Vendor.Text = "Новая поставка";
            }
            else
            {
                tabPage3.Parent = null;
                New_Detail.Show();
                New_Vendor.Text = "Новый поставщик";
            }
        }

        private void mode_button_Click(object sender, EventArgs e)
        {
            mode();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "data_BaseDataSet.Supply". При необходимости она может быть перемещена или удалена.
            this.supplyTableAdapter.Fill(this.data_BaseDataSet.Supply);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "data_BaseDataSet.detail". При необходимости она может быть перемещена или удалена.
            this.detailTableAdapter.Fill(this.data_BaseDataSet.detail);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "data_BaseDataSet.Vendor". При необходимости она может быть перемещена или удалена.
            this.vendorTableAdapter.Fill(this.data_BaseDataSet.Vendor);

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void My_Execute_Non_Querty(string CommandText)
        {
            OleDbConnection conn = new OleDbConnection(ConnString);
            conn.Open();
            OleDbCommand myCommand = conn.CreateCommand();
            myCommand.CommandText = CommandText;
            myCommand.ExecuteNonQuery();
            conn.Close();
        }

        private void Vendors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index != -1)
            {
                while (dataGridView1.Rows.Count != 0)
                {
                    dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                }
                bool u = false;
                int C = 4;
                int ID = Convert.ToInt32(Vendors.Rows[index].Cells[C].Value);
                while (ID != 0 || u != true)
                {
                    for (int i = 0; i < Details.Rows.Count; i++)
                        if (ID == Convert.ToInt32(Details.Rows[i].Cells[0].Value))
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[C - 4].Cells[0].Value = Details.Rows[i].Cells[0].Value;
                            dataGridView1.Rows[C - 4].Cells[1].Value = Details.Rows[i].Cells[1].Value;
                            dataGridView1.Rows[C - 4].Cells[2].Value = Details.Rows[i].Cells[2].Value;
                        }
                    u = true;
                    C++;
                    if (Convert.ToString(Vendors.Rows[index].Cells[C].Value) != "")
                        ID = Convert.ToInt32(Vendors.Rows[index].Cells[C].Value);
                    else ID = 0;
                }
            }
        }

        public int priceOfDetail(int row)
        {
            return Convert.ToInt32(Details.Rows[row].Cells[2].Value);
        }

        public int number;
        private void New_Detail_Click(object sender, EventArgs e)
        {
            number = Convert.ToInt32(Details.Rows[Details.Rows.Count - 1].Cells[0].Value);
            Form3 frm = new Form3();
            frm.Owner = this;
            frm.ShowDialog();
            detail_update();
        }

        private void New_Vendor_Click(object sender, EventArgs e)
        {
            if (modeType == false)
            {
                number = Convert.ToInt32(Vendors.Rows[Vendors.Rows.Count - 1].Cells[0].Value);
                Form4 frm = new Form4();
                frm.Owner = this;
                frm.ShowDialog();
                vendors_update();
            }
            else
            {
                tabPage3.Parent = null;
                tabPage2.Parent = null;
                Max2 = Details.Rows.Count;
                Max3 = Vendors.Rows.Count;
                New_Vendor.Enabled = false;
                button4.Enabled = false;
                System.Threading.Thread.Sleep(1000);
                Form6 frm = new Form6(Max2, Max3);
                frm.Owner = this;
                frm.Show();
            }
        }

        public int Max2;
        public int Max3;

        public void show()
        {
            tabPage2.Parent = tabControl1;
            tabPage3.Parent = tabControl1;
            New_Vendor.Enabled = true;
            button4.Enabled = true;
            tabControl1.SelectedIndex = 2;
            supply_update();
        }

        private void detail_update()
        {
            while (Details.Rows.Count != 0)
                Details.Rows.Remove(Details.CurrentRow);
            OleDbDataAdapter dAD = new OleDbDataAdapter(CmdTextDetail, ConnString);
            DataSet dsD = new DataSet();
            dAD.Fill(dsD, "[detail]");
            Details.DataSource = dsD.Tables[0].DefaultView;
        }
        private void vendors_update()
        {
            while (Vendors.Rows.Count != 0)
                Vendors.Rows.Remove(Vendors.CurrentRow);
            OleDbDataAdapter dAV = new OleDbDataAdapter(CmdTextVendor, ConnString);
            DataSet dsV = new DataSet();
            dAV.Fill(dsV, "[Vendor]");
            Vendors.DataSource = dsV.Tables[0].DefaultView;
        }
        private void supply_update()
        {
            while (Supplies.Rows.Count != 0)
                Supplies.Rows.Remove(Supplies.CurrentRow);
            OleDbDataAdapter dAS = new OleDbDataAdapter(CmdTextSupply, ConnString);
            DataSet dsS = new DataSet();
            dAS.Fill(dsS, "[Supply]");
            Supplies.DataSource = dsS.Tables[0].DefaultView;
        }

        private void Supplies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            int iV = Convert.ToInt32(Supplies.Rows[index].Cells[2].Value);
            int iD = Convert.ToInt32(Supplies.Rows[index].Cells[1].Value);
            label3.Text = Supplies.Rows[index].Cells[0].Value.ToString();
            label8.Text = iV.ToString();
            label11.Text = iD.ToString();
            label18.Text = Supplies.Rows[index].Cells[3].Value.ToString();
            label5.Text = Vendors.Rows[iV - 1].Cells[1].Value.ToString();
            label10.Text = Vendors.Rows[iV - 1].Cells[3].Value.ToString();
            label13.Text = Details.Rows[iD - 1].Cells[1].Value.ToString();
            label15.Text = Details.Rows[iD - 1].Cells[2].Value.ToString();
            label19.Text = (Convert.ToInt32(label18.Text) * Convert.ToInt32(label15.Text)).ToString();

        }
    }
}
