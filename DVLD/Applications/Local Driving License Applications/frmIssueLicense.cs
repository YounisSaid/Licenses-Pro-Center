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
using System.Windows.Media;

namespace DVLD
{
    public partial class frmIssueLicense: Form
    {
        int _LocalDrivingLicenseApplication = -1;
        int _UserID=-1;
        int _DriverID=-1;
        int _LicenseID = -1;
        clsLocalDrivingLicenseApplication clsLocalDrivingLicenseApplication;
        public frmIssueLicense(int LocalDrivingLicenseApplication, int UserID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplication = LocalDrivingLicenseApplication;
            _UserID = UserID;
        }

        private void frmIssueLicense_Load(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplication);
            if (clsLocalDrivingLicenseApplication != null)
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
                

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonInfo frmPersonInfo = new frmPersonInfo(clsLocalDrivingLicenseApplication.ApplicantPersonID);
            frmPersonInfo.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clsDriver clsDriver = new clsDriver();
            clsDriver.PersonID = clsLocalDrivingLicenseApplication.ApplicantPersonID;
            clsDriver.CreatedByUserID = _UserID;
            clsDriver.CreatedDate = DateTime.Now;

            //if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            //{

            //    if (clsDriver.Save())
            //    {
            //        MessageBox.Show("Driver Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    }
            //    else
            //    {
            //        MessageBox.Show("Driver Not Added", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            _DriverID = clsDriver.Add();
            if (_DriverID !=-1)
            {
                clsLicense clsLicense = new clsLicense();
                clsLicense.DriverID = _DriverID;
                clsLicense.LicenseClass = clsLocalDrivingLicenseApplication.LicenseClassID;
                clsLicense.ApplicationID = clsLocalDrivingLicenseApplication.ApplicationID;
                clsLicense.IssueDate = DateTime.Now;
                clsLicense.ExpirationDate = DateTime.Now.AddYears(clsLicenseClass.Find(lblClass.Text).DefaultValidityLength);
                clsLicense.Notes = txtNotes.Text;
                clsLicense.PaidFees = Convert.ToByte(clsLicenseClass.Find(lblClass.Text).ClassFees);
                clsLicense.IsActive = true;
                clsLicense.IssueReason = 1;
                clsLicense.CreatedByUserID = _UserID;
                _LicenseID = clsLicense.Add();
                if (_LicenseID !=-1)
                {
                    MessageBox.Show($"License Added Successfully with ID = {_LicenseID}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    
                }
                else
                {
                    MessageBox.Show("License Not Added", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
