using DVLD_BussnessLayer_;
using System;
using System.Data;
using System.Windows.Forms;


namespace DVLD
{
    public partial class frmMangeApplicationTypes: Form
    {
        public frmMangeApplicationTypes()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmMangeApplicationTypes_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }
        private void RefreshGrid()
        {
            DataTable dt = clsBussniessApplicationTypes.GetAllApplicationTypes();
            dataGridView1.DataSource = dt;
            lblRecords.Text = "# " + dt.Rows.Count.ToString() + " Record(s)";

        }

        private void edToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmUpdateApplicationFees frm = new frmUpdateApplicationFees(selectedID);
            frm.FormClosed += CloseandUpdate;
            frm.ShowDialog();
        }
        private void CloseandUpdate(object sender, FormClosedEventArgs e)
        {
            RefreshGrid();
        }
    }
}
