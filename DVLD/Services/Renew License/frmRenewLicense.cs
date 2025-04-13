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
    public partial class frmRenewLicense: Form
    {
        public frmRenewLicense(int UserID)
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
        clsLocalDrivingLicenseApplication clsNewLocalDrivingLicenseApplication;

        clsLicense License;
        clsLicense NewLicense;
        clsDriver Driver;
        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {

        }

        private void lblType_Click(object sender, EventArgs e)
        {

        }

        private void frmRenewLicense_Load(object sender, EventArgs e)
        {
           
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).Fees.ToString();
            lblRenewIssueDate.Text = DateTime.Now.ToShortDateString();
            lblAppllicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = clsBussniessUser.GetUserById(_User_Id).UserName;
        }

        private void btnFind_Click(object sender, EventArgs e)
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
                lblIsdetained.Text = "No";
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
                lblRenewLicenseFees.Text = clsLicenseClass.Find(lblLicenseClass.Text).ClassFees.ToString();
                lblTotalFess.Text = (clsLicenseClass.Find(_LicenseClassID).ClassFees + clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).Fees).ToString();
                lblOldLicenseID.Text = lblLocalLicenseID.Text;
            }
            else
            {
                MessageBox.Show("No License Found with this ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblDriverID.Text) || lblDriverID.Text == "[????]")
            {
                MessageBox.Show("Please Find the Driver First", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (License.ExpirationDate > DateTime.Now)
            {
                MessageBox.Show("This License is not Expired Yet !!! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(!License.IsActive)
            {
                MessageBox.Show("This License is not Active !!! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (License.Lock(_LicenseID))
            {
                clsNewLocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
                if (clsNewLocalDrivingLicenseApplication != null)
                {


                    clsNewLocalDrivingLicenseApplication.ApplicantPersonID = clsLocalDrivingLicenseApplication.ApplicantPersonID; ;
                    clsNewLocalDrivingLicenseApplication.ApplicationDate = clsLocalDrivingLicenseApplication.ApplicationDate;
                    clsNewLocalDrivingLicenseApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
                    clsNewLocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                    clsNewLocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
                    clsNewLocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
                    clsNewLocalDrivingLicenseApplication.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).Fees;
                    clsNewLocalDrivingLicenseApplication.CreatedByUserID = _User_Id;
                    clsNewLocalDrivingLicenseApplication.LicenseClassID = clsLocalDrivingLicenseApplication.LicenseClassID;
                    if (clsNewLocalDrivingLicenseApplication.Save())
                    {
                        NewLicense = new clsLicense();
                        NewLicense.ApplicationID = clsNewLocalDrivingLicenseApplication.ApplicationID;
                        NewLicense.DriverID = Driver.DriverID;
                        NewLicense.LicenseClass = clsLocalDrivingLicenseApplication.LicenseClassID;
                        NewLicense.IssueDate = DateTime.Now;
                        NewLicense.ExpirationDate = DateTime.Now.AddYears(10);
                        if (txtNotes.Text != null)
                        {
                            NewLicense.Notes = txtNotes.Text;
                        }
                        else
                        {
                            NewLicense.Notes = "Renewed License";
                        }

                        NewLicense.PaidFees = Convert.ToByte(clsLicenseClass.Find(lblLicenseClass.Text).ClassFees);
                        NewLicense.IsActive = true;
                        NewLicense.IssueReason = 2;
                        NewLicense.CreatedByUserID = _User_Id;
                        int NewLicenseID = NewLicense.Add();
                        if (NewLicenseID != -1)
                        {
                            lblRLApplicationID.Text = clsNewLocalDrivingLicenseApplication.ApplicationID.ToString();
                            lblRLLicenseID.Text = NewLicenseID.ToString();

                            MessageBox.Show("License Renewed Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnIssue.Enabled = false;
                            gbFilter.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("Error in Saving License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
            }


        }

        private void lblRLLicenseID_Click(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gbFilter_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
