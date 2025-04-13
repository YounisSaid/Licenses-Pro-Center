using System;

using System.Data;

using System.Windows.Forms;
using BussnessLayer;
using DVLD_BussnessLayer_;

namespace DVLD
{
    public partial class frmMangeTestTypes: Form
    {
        public frmMangeTestTypes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMangeTestTypes_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }
        private void RefreshGrid()
        {
            DataTable dt = clsBussniessTestTypes.GetAllTestTypes();
            dataGridView1.DataSource = dt;
            lblRecords.Text = "# " + dt.Rows.Count.ToString() + " Record(s)";
        }

        private void edToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmUpdateTestFees frm = new frmUpdateTestFees(selectedID);
            frm.FormClosed += CloseandUpdate;
            frm.ShowDialog();
        }

        private void CloseandUpdate(object sender, FormClosedEventArgs e)
        {
            RefreshGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
