using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Properties;
using DVLD_BussnessLayer_;

namespace DVLD
{
    public partial class FrmTest: Form
    {
        int _LocalDrivingLicenseApplicationID;
        int _TestTypeID;
        clsLocalDrivingLicenseApplication clsLocalDrivingLicenseApplication;
        public FrmTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;
        }
        private void LoadPicutreandTitle()
        {
            if(_TestTypeID==1)
            {
                TestPicurte.Image = Resources.search__1_;
                lblMode.Text = "Vision Test Appiontment";
                lblPassedTests.Text = "0/3";
                return;
            }
            if (_TestTypeID == 2)
            {
                TestPicurte.Image = Resources.test__3_ ;
                lblMode.Text = "Written Test Appiontment";
                lblPassedTests.Text = "1/3";

                return;

            }
            else
            {
                TestPicurte.Image = Resources.driving_lessons__1_;
                lblMode.Text = "Driving Test Appiontment";
                lblPassedTests.Text = "2/3";

                return;

            }
        }
        
        private void FrmVisionTest_Load(object sender, EventArgs e)
        {
            LoadPicutreandTitle();
            clsLocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);
            if(clsLocalDrivingLicenseApplication != null)
            {
                lblDLAPPID.Text = clsLocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                lblClass.Text = clsLocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
                lblAppID.Text = clsLocalDrivingLicenseApplication.ApplicationID.ToString();
                lblStatus.Text = clsLocalDrivingLicenseApplication.StatusText;
                lblFees.Text = clsLocalDrivingLicenseApplication.ApplicationTypeInfo.ApplicationFees.ToString();
                lblDate.Text = clsLocalDrivingLicenseApplication.ApplicationDate.ToShortDateString();
                lblStatusDate.Text = clsLocalDrivingLicenseApplication.LastStatusDate.ToShortDateString();
                lblCreatedBy.Text = clsLocalDrivingLicenseApplication.CreatedByUserID.ToString();
                lblType.Text = clsLocalDrivingLicenseApplication.ApplicationTypeInfo.ApplicationTypeTitle;

                LblFullName.Text = clsLocalDrivingLicenseApplication.PersonFullName;
                RefreshGrid();


            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonInfo frmPersonInfo = new frmPersonInfo(clsLocalDrivingLicenseApplication.ApplicantPersonID);
            frmPersonInfo.ShowDialog();
        }
        private void RefreshGrid()
        {
            DataTable dt = clsTestAppiontment.GetAllTestAppiontementsforPerson(_LocalDrivingLicenseApplicationID,_TestTypeID);
            dataGridView1.DataSource = dt;
            lblRecords.Text = "# " + dt.Rows.Count.ToString() + " Record(s)";
        }
        private void closeandRefresh()
        {
            RefreshGrid();
        }
        private void btnAddAppiontment_Click(object sender, EventArgs e)
        {
            if(clsTestAppiontment.IsLastappLocked(_LocalDrivingLicenseApplicationID, _TestTypeID))
            {
                MessageBox.Show("You Already Have an Open Appiontement,You cannot open new one!","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmSecuduleTest frmSecuduleTest = new frmSecuduleTest(-1,_LocalDrivingLicenseApplicationID, _TestTypeID);
            frmSecuduleTest.FormClosed += (s, args) => closeandRefresh();
            frmSecuduleTest.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }


        private void edToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            frmSecuduleTest frmSecuduleTest = new frmSecuduleTest(selectedID, _LocalDrivingLicenseApplicationID, 1);
            frmSecuduleTest.FormClosed += (s, args) => closeandRefresh();
            frmSecuduleTest.ShowDialog();
        }
        frmTakeTest frmTakeTest;
        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedID = (int)dataGridView1.CurrentRow.Cells[0].Value;
             frmTakeTest = new frmTakeTest(selectedID, _TestTypeID);
            this.AddOwnedForm(frmTakeTest);
            frmTakeTest.FormClosed += (s, args) => closeandRefresh();
            

            frmTakeTest.ShowDialog();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
