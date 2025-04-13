using BussnessLayer;
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
    public partial class frmLicenseInfo: Form
    {
        int _LocalDrivingLicenseID=-1;
        int _PersonID=-1;
        int _LicenseClassID = -1;
        int _LicenseID = -1;
        clsLocalDrivingLicenseApplication clsLocalDrivingLicenseApplication;
        clsLicense License;
        public frmLicenseInfo(int LocalDrivingLicenseID)
        {
            InitializeComponent();
            _LocalDrivingLicenseID = LocalDrivingLicenseID;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmLicenseInfo_Load(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseID);
            _PersonID = clsLocalDrivingLicenseApplication.ApplicantPersonID;
            clsBussinessPerson clsBussinessPerson = clsBussinessPerson.FindPersonByID(_PersonID);
            _LicenseClassID = clsLocalDrivingLicenseApplication.LicenseClassID;
            _LicenseID = clsLicense.GetActiveLicenseIDByPersonID(_PersonID, _LicenseClassID);
            License = clsLicense.Find(_LicenseID);
            
            lblClass.Text = clsLocalDrivingLicenseApplication.LicenseClassInfo.ClassName;//
            lblLicenseID.Text = _LicenseID.ToString();//
            lblNationalNo.Text = clsBussinessPerson.NationalNo;
            lblFullName.Text = clsBussinessPerson.FullName;
            if (clsBussinessPerson.Gender == 0)
            {
                lblGender.Text = "Male";
            }
            else
            {
                lblGender.Text = "Female";

            }
            lblIssueDate.Text = License.IssueDate.ToShortDateString();
            lblExpireDate.Text = License.ExpirationDate.ToShortDateString();
            if(License.Notes!="")
            {
            lblNotes.Text = License.Notes;

            }
            else
            {
                lblNotes.Text = "No Notes";
            }
            if (License.IsActive)
            {
                lblstatus.Text = "Active";
            }
            else
            {
                lblstatus.Text = "Not Active";
            }
            lblDateOfBirth.Text = clsBussinessPerson.DateOfBirth.ToShortDateString();
            lblDriverID.Text = License.DriverID.ToString();
            lblIsDetained.Text = "No";
            lblIssueReason.Text = License.IssueReason.ToString();
            if(License.IssueReason ==1)
            {
                lblIssueReason.Text = "New";
            }
            if (License.IssueReason == 2)
            {
                lblIssueReason.Text = "Renew";
            }

            if (License.IssueReason == 3)
            {
                lblIssueReason.Text = "Damaged License";
            }
            if (License.IssueReason == 4)
            {
                lblIssueReason.Text = "Lost License";
            }
            if (PersonPicture.Image != null)
            {
                PersonPicture.Load(clsBussinessPerson.ImagePath);
            }
            else
            {
                if(clsBussinessPerson.Gender==0)
                {
                    PersonPicture.Image = Resources.person_man__2_;
                }
                else
                {
                    PersonPicture.Image = Resources.person_little_girl;

                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PersonPicture_Click(object sender, EventArgs e)
        {

        }
    }
}
