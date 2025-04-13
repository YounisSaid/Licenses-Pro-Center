using DVLD.Properties;
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
    public partial class frmTakeTest: Form
    {
         int _TestAppiontmentID;
        int _TestTypeID;
        enum enmode
        {
            Add,
            Update
        }
        enmode mode;
        clsTest Test;
        clsTestAppiontment TestAppiontment;
        public frmTakeTest(int TestAppiontmentID,int TestTypeID)
        {
            InitializeComponent();
            _TestAppiontmentID = TestAppiontmentID;
            _TestTypeID = TestTypeID;
            mode = enmode.Add;

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void LblFullName_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label42_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {

        }

        private void lblCreatedBy_Click(object sender, EventArgs e)
        {

        }
        private void LoadPicutreandTitle()
        {
            if (_TestTypeID == 1)
            {
                TestPicutre.Image = Resources.search__1_;
                gbTest.Text = "Vision Test Appiontment";
                return;
            }
            if (_TestTypeID == 2)
            {
                TestPicutre.Image = Resources.test__3_;
                gbTest.Text = "Written Test Appiontment";
                return;

            }
            else
            {
                TestPicutre.Image = Resources.driving_lessons__1_;
                gbTest.Text = "Driving Test Appiontment";
                return;

            }
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            LoadPicutreandTitle();
            TestAppiontment = clsTestAppiontment.Find(_TestAppiontmentID);
                lblDLAPPID.Text =  TestAppiontment.LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                lblCreatedBy.Text =clsBussniessUser.GetUserById( TestAppiontment.LocalDrivingLicenseApplication.CreatedByUserID).UserName;
                lblClass.Text = TestAppiontment.LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
                lblFullName.Text = TestAppiontment.LocalDrivingLicenseApplication.PersonFullName;
                lblFees.Text = TestAppiontment.LocalDrivingLicenseApplication.ApplicationTypeInfo.ApplicationFees.ToString();
                lblDatee.Text = TestAppiontment.AppointmentDate.ToShortDateString();
            lblTrials.Text = TestAppiontment.LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();
            
            if (mode == enmode.Add)
            {
           
                Test = new clsTest();
                return;

            }
            
        }
       
        private void btnSave_Click(object sender, EventArgs e)
        {
            Test.TestAppointmentID = _TestAppiontmentID;
            if(rbPass.Checked)
            {
                Test.TestResult = true;
                
            }
            else
            {
                Test.TestResult = false;
                clsTestAppiontment.UnLock(TestAppiontment.LocalDrivingLicenseApplicationID, _TestTypeID);
            }
            Test.Notes = lblNotes.Text;
            Test.CreatedByUserID = TestAppiontment.LocalDrivingLicenseApplication.CreatedByUserID;
            if(Test.Save())
            {
                
                if ( MessageBox.Show("Are You Sure ?","Confirm",MessageBoxButtons.OKCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    MessageBox.Show("Test Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rbFail.Enabled = false;
                    rbPass.Enabled = false;
                    lblNotes.Enabled = false;
                    btnSave.Enabled = false;
                    lblTestID.Text = clsTest.Find(_TestAppiontmentID).TestID.ToString();
                    if (rbPass.Checked)
                    {
                       
                        this.Owner?.Close();  // Close the parent if found
                        this.Close();
                        
                    }
                }
            
            }
            else
            {
                MessageBox.Show("Test Not Saved","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblTrial_Click(object sender, EventArgs e)
        {

        }
    }
}
