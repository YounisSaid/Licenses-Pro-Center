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
    public partial class frmSecuduleTest: Form
    {
        int _LocalDrivingLicenseApplicationID;
        int _TestTypeID;
        int _TestAppointmentID =-1;
        clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication;
        enum enMode { AddNew = 0, Update = 1 };
        enMode Mode;
        clsTestAppiontment TestAppiontment;
        public frmSecuduleTest(int TestAppointmentID, int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;
            _TestAppointmentID = TestAppointmentID;
            if (TestAppointmentID == -1) {
                Mode = enMode.AddNew;
            }
            else
            {
                Mode = enMode.Update;
                
            }
        }
        private void LoadPicutreandTitle()
        {
            if (_TestTypeID == 1)
            {
                TestPicutre.Image = Resources.search__1_;
                gbMode.Text = "Vision Test Appiontment";
                return;
            }
            if (_TestTypeID == 2)
            {
                TestPicutre.Image = Resources.test__3_;
                gbMode.Text = "Written Test Appiontment";
                return;

            }
            else
            {
                TestPicutre.Image = Resources.driving_lessons__1_;
                gbMode.Text = "Driving Test Appiontment";
                return;

            }
        }
        private void frmSecuduleTest_Load(object sender, EventArgs e)
        {
            LoadPicutreandTitle();  
            LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);
            if (LocalDrivingLicenseApplication != null)
            {
                lblDLAPPID.Text = LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                lblClass.Text = LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
                
                lblUserName.Text =clsBussniessUser.GetUserById(LocalDrivingLicenseApplication.CreatedByUserID).UserName;
                lblFullName.Text = LocalDrivingLicenseApplication.PersonFullName;
                lblTrial.Text = LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString(); 
            }
            if (Mode == enMode.AddNew)
            {
               lblFees.Text =  clsBussniessTestTypes.Find(_TestTypeID).TestTypeFees.ToString();
                grbRetakeTestInfo.Enabled = false;
                TestAppiontment = new clsTestAppiontment();
                return;
            }
            TestAppiontment = clsTestAppiontment.Find(_TestAppointmentID);
            if (TestAppiontment != null)
            {
                cmbdate.Value = TestAppiontment.AppointmentDate;
                lblFees.Text = TestAppiontment.PaidFees.ToString();
                
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TestAppiontment.AppointmentDate = cmbdate.Value;
            TestAppiontment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            TestAppiontment.PaidFees = Convert.ToByte(lblFees.Text);
            TestAppiontment.AppointmentDate = cmbdate.Value;
            TestAppiontment.CreatedByUserID = LocalDrivingLicenseApplication.CreatedByUserID;
            TestAppiontment.TestTypeID = _TestTypeID;
            TestAppiontment.IsLocked = true;


            if (TestAppiontment.Save())
            {
                DialogResult result = MessageBox.Show("Are You Sure ?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.OK)
                {
                    MessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
