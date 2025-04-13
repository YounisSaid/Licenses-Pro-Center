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
    public partial class frmInternationalLicenseCard : Form
    {
        private int _InternationalLicenseID;
        private clsBussinessPerson _person;
        public frmInternationalLicenseCard(int InternationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID = InternationalLicenseID;
        }

        private void frmInternationalLicenseCard_Load(object sender, EventArgs e)
        {
            clsInternationalLicense internationalLicense = clsInternationalLicense.Find(_InternationalLicenseID);
            if (internationalLicense != null)
            {
                lblFullName.Text = internationalLicense.ApplicantFullName;
                _person = clsBussinessPerson.FindPersonByID(internationalLicense.ApplicantPersonID);
                lblDateOfBirth.Text = _person.DateOfBirth.ToString("yyyy-MM-dd");
                if (_person.Gender == 0)
                {
                    lblGender.Text = "Male";

                }
                else
                {
                    lblGender.Text = "Female";

                }
                lblNationalNo.Text = _person.NationalNo;
                lblInterNationalLicenseID.Text = internationalLicense.InternationalLicenseID.ToString();
                lblIssueDate.Text = internationalLicense.IssueDate.ToString("yyyy-MM-dd");
                lblExpirationDate.Text = internationalLicense.ExpirationDate.ToString("yyyy-MM-dd");
                lblIsActive.Text = internationalLicense.IsActive ? "Yes" : "No";
                lblLocalLicenseID.Text = internationalLicense.IssuedUsingLocalLicenseID.ToString();
                lblDriverID.Text = internationalLicense.DriverID.ToString();
                lblApplicationID.Text = internationalLicense.ApplicationID.ToString();
                if (pbPersonImage.Image != null)
                {
                    pbPersonImage.Load(_person.ImagePath);
                }
                else
                {
                    if (_person.Gender == 0)
                    {
                        pbPersonImage.Image = Resources.person_man__2_;
                    }
                    else
                    {
                        pbPersonImage.Image = Resources.person_little_girl;

                    }
                }


            }
        }
    }
}
