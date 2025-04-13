using DVLD_BussnessLayer_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmLocalDrivingLicenseApplication: Form
    {
        int _UserID;
        private DataTable _dtAllLocalDrivingLicenseApplications;
        public frmLocalDrivingLicenseApplication(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        void SetDimensions()
        {
            dataGridView1.Columns[1].Width = 250;
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.Columns[4].Width = 150;




        }
        private void frmLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            cmbFilter.Text = "None";
            RefreshGrid();
            SetDimensions(); 
        }
        private void RefreshGrid()
        {
             _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dataGridView1.DataSource = _dtAllLocalDrivingLicenseApplications;
            lblRecords.Text = "# " + dataGridView1.Rows.Count.ToString() + " Record(s)";

        }
        private void CloseandUpdate(object sender, FormClosedEventArgs e)
        {
            RefreshGrid();
        }
        private void sechudleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            FrmTest frmVisionTest = new FrmTest(selectedID,1);
            frmVisionTest.FormClosed += CloseandUpdate;
            frmVisionTest.Show();
        }

        private void sechudleTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
          

        }
        void ResetMenuItems()
        {
            sechudleTestToolStripMenuItem.Enabled = true;
            sechudleStreetTestToolStripMenuItem.Enabled = true;
            sechudleWrittenTestToolStripMenuItem.Enabled = true;
            sechudleVisionTestToolStripMenuItem.Enabled = true;
            showLiceneseToolStripMenuItem.Enabled = true;
            showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
            cToolStripMenuItem.Enabled = true;
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;



        }
        void SetTests()
        {
            int PassedTests = (int)dataGridView1.CurrentRow.Cells[5].Value;
           
            ResetMenuItems();
            if (PassedTests == 0)
            {

                sechudleStreetTestToolStripMenuItem.Enabled = false;
                sechudleWrittenTestToolStripMenuItem.Enabled = false;

                showLiceneseToolStripMenuItem.Enabled = false;
                showPersonLicenseHistoryToolStripMenuItem.Enabled = false;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;


            }
            if (PassedTests == 1)
            {

                sechudleVisionTestToolStripMenuItem.Enabled = false;
                sechudleStreetTestToolStripMenuItem.Enabled = false;


                showLiceneseToolStripMenuItem.Enabled = false;
                showPersonLicenseHistoryToolStripMenuItem.Enabled = false;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;




            }
            if (PassedTests == 2)
            {

                sechudleVisionTestToolStripMenuItem.Enabled = false;

                sechudleWrittenTestToolStripMenuItem.Enabled = false;

                showLiceneseToolStripMenuItem.Enabled = false;
                showPersonLicenseHistoryToolStripMenuItem.Enabled = false;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;


            }
            if (PassedTests == 3)
            {

                sechudleTestToolStripMenuItem.Enabled = false;
                cToolStripMenuItem.Enabled = false;
                int LocalDrivingAppLicenseID = (int)dataGridView1.CurrentRow.Cells[0].Value;

                clsLocalDrivingLicenseApplication drivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingAppLicenseID);
                if (drivingLicenseApplication.IsLicenseIssued())
                {
                    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;

                    showLiceneseToolStripMenuItem.Enabled = true;
                    showPersonLicenseHistoryToolStripMenuItem.Enabled = true;


                }
                else
                {
                    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;

                    showLiceneseToolStripMenuItem.Enabled = false;
                    showPersonLicenseHistoryToolStripMenuItem.Enabled = false;



                }



            }
        }

        private void sechudleTestToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void sechudleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            FrmTest frmVisionTest = new FrmTest(selectedID,2);
            frmVisionTest.FormClosed += CloseandUpdate;
            frmVisionTest.Show();
        }

        private void sechudleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            FrmTest frmVisionTest = new FrmTest(selectedID, 3);
            frmVisionTest.FormClosed += CloseandUpdate;
            frmVisionTest.Show();
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(selectedID);
            if (MessageBox.Show("Are you sure you want to delete this application?", "Delete Application", MessageBoxButtons.OKCancel, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                if(LocalDrivingLicenseApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully", "Delete Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshGrid();
                }
                else
                {
                    MessageBox.Show("Application cannot be Deleted Because It has Data Linked to it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            SetTests();

            int selectedID = (int)dataGridView1.CurrentRow.Cells[5].Value;
            string Status = (string)dataGridView1.CurrentRow.Cells[6].Value;

            if (Status == "Cancelled")
            {
                contextMenuStrip1.Enabled = false;
            }
            else
            {
                contextMenuStrip1.Enabled = true;
            }
            if (selectedID > 0)
            {
                deleteApplicationToolStripMenuItem.Enabled = false;
            }
            else
            {
                deleteApplicationToolStripMenuItem.Enabled = true;
            }

        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(selectedID);
            if (MessageBox.Show("Are you sure you want to close this application?", "Close Application", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                LocalDrivingLicenseApplication.ApplicationStatus = clsLocalDrivingLicenseApplication.enApplicationStatus.Cancelled;
                if (LocalDrivingLicenseApplication.Save())
                {
                    MessageBox.Show("Application Closed Successfully", "Close Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshGrid();
                }
            }

        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmIssueLicense frmIssueLicense = new frmIssueLicense(selectedID,_UserID);
            frmIssueLicense.FormClosed += CloseandUpdate;
            frmIssueLicense.ShowDialog();
        }

        private void showLiceneseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(selectedID);
            frmLicenseInfo.FormClosed += CloseandUpdate;
            frmLicenseInfo.ShowDialog();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            txtFilter.Visible = true;

            //Map Selected Filter to real Column name 
            switch (cmbFilter.Text)
            {

                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Status":
                    FilterColumn = "Status";
                    break;


                default:
                    FilterColumn = "None";
                    
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblRecords.Text = dataGridView1.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "LocalDrivingLicenseApplicationID")
                //in this case we deal with integer not string.
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());
            else
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilter.Text.Trim());

            lblRecords.Text = dataGridView1.Rows.Count.ToString();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase L.D.L.AppID id is selected.
            if (cmbFilter.Text == "L.D.L.AppID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbFilter.SelectedIndex != 0)
            {
                txtFilter.Visible = true;
                txtFilter.Focus();
            }
            else
            {
                txtFilter.Visible = false;
            }
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frmAddUpdateLocalDrivingLicenseApplication = new frmAddUpdateLocalDrivingLicenseApplication(_UserID);
            frmAddUpdateLocalDrivingLicenseApplication.FormClosed += CloseandUpdate;
            frmAddUpdateLocalDrivingLicenseApplication.ShowDialog();
        }
    }
}
