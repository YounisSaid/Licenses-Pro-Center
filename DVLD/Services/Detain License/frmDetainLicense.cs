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
    public partial class frmDetainLicense: Form
    {
        public frmDetainLicense(int UserID)
        {
            InitializeComponent();
            _User_Id = UserID;
        }
        int _User_Id = -1;
        int _LocalDrivingLicenseID = -1;
        int _PersonID = -1;
        int _LicenseClassID = -1;
        int _LicenseID = -1;
        clsBussinessPerson BussinessPerson;
        clsLocalDrivingLicenseApplication clsLocalDrivingLicenseApplication;
        clsLicense License;
        clsDriver Driver;
        clsDetainedLicense DetainedLicense;
        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
           
            lblDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = clsBussniessUser.GetUserById(_User_Id).UserName;
        }

        private void gbFilter_Enter(object sender, EventArgs e)
        {

        }

        private void btnFind_Click(object sender, EventArgs e)
        {

            {
                if (string.IsNullOrEmpty(txtLicenseID.Text))
                {
                    MessageBox.Show("Please Enter License ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int LicenseID = int.Parse(txtLicenseID.Text);

                License = clsLicense.Find(LicenseID);
                if (License != null)
                {
                    _LicenseID = License.LicenseID;
                    clsLocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByApplicationID(License.ApplicationID);
                    _LicenseClassID = License.LicenseClass;
                    Driver = clsDriver.Find(License.DriverID);
                    _PersonID = Driver.PersonID;
                    BussinessPerson = clsBussinessPerson.FindPersonByID(_PersonID);



                    clsDetainedLicense clsDetainedLicense = clsDetainedLicense.FindByLicenseID(_LicenseID);

                    if(clsDetainedLicense != null)
                    {
                        if (clsDetainedLicense.IsReleased)
                        {
                            lblIsdetained.Text = "No";

                        }
                        else
                        {
                            lblIsdetained.Text = "Yes";
                        }
                    }
                    else
                    {
                        lblIsdetained.Text = "No";
                    }

                    lblLicenseClass.Text = clsLocalDrivingLicenseApplication.LicenseClassInfo.ClassName;//
                    lblLocalLicenseID.Text = _LicenseID.ToString();//
                    lblNationalNo.Text = BussinessPerson.NationalNo;
                    lblFullName.Text = BussinessPerson.FullName;
                    if (BussinessPerson.Gender == 0)
                    {
                        lblGender.Text = "Male";
                    }
                    else
                    {
                        lblGender.Text = "Female";

                    }
                    lblIssueDate.Text = License.IssueDate.ToShortDateString();
                    lblExpirationDate.Text = License.ExpirationDate.ToShortDateString();
                    if (License.Notes != "")
                    {
                        lblNotes.Text = License.Notes;

                    }
                    else
                    {
                        lblNotes.Text = "No Notes";
                    }
                    if (License.IsActive)
                    {
                        lblIsActive.Text = "Active";
                    }
                    else
                    {
                        lblIsActive.Text = "Not Active";
                    }
                    lblDateOfBirth.Text = BussinessPerson.DateOfBirth.ToShortDateString();
                    lblDriverID.Text = License.DriverID.ToString();
                    
                    lblIssueReason.Text = License.IssueReason.ToString();
                    if (License.IssueReason == 1)
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
                    if (!string.IsNullOrEmpty(BussinessPerson.ImagePath))

                    {
                        pbPersonImage.Load(BussinessPerson.ImagePath);
                    }
                    else
                    {
                        if (BussinessPerson.Gender == 0)
                        {
                            pbPersonImage.Image = Resources.person_man__2_;
                        }
                        else
                        {
                            pbPersonImage.Image = Resources.person_little_girl;

                        }
                    }
                }
                else
                {
                    MessageBox.Show("No License Found with this ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(lblDriverID.Text) || lblDriverID.Text == "[????]")
            {
                MessageBox.Show("Please Find the Driver First", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmPersonInfo frmPersonInfo = new frmPersonInfo(_PersonID);
            frmPersonInfo.ShowDialog();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblDriverID.Text) || lblDriverID.Text == "[????]")
            {
                MessageBox.Show("Please Find the Driver First", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtFineFees.Text))
            {
                MessageBox.Show("Please Enter Fine Fees", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (lblIsdetained.Text == "Yes")
            {
                MessageBox.Show("This License is already Detained", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DetainedLicense = new clsDetainedLicense();
            DetainedLicense.LicenseID = _LicenseID;
            DetainedLicense.IsReleased = false;
            DetainedLicense.CreatedByUserID = _User_Id;
            DetainedLicense.DetainDate =DateTime.Now;
            DetainedLicense.FineFees = float.Parse(txtFineFees.Text);
            if(DetainedLicense.Save())
            {
                lblDetainID.Text = DetainedLicense.DetainID.ToString();
                MessageBox.Show("License Detained Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblIsdetained.Text = "Yes";
                gbFilter.Enabled = false;
                btnIssue.Enabled = false;
            }
            else
            {
                MessageBox.Show("Error in Detaining License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
