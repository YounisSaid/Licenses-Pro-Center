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
    public partial class frmNewInternationalDrivingLicense : Form
    {
        public frmNewInternationalDrivingLicense(int _UserID)
        {
            InitializeComponent();
            _User_Id = _UserID;
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
            }
            else
            {
                MessageBox.Show("No License Found with this ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private void frmNewInternationalDrivingLicense_Load(object sender, EventArgs e)
        {
            
            lblStatus.Text = clsApplication.enApplicationStatus.New.ToString();
            lblType.Text = clsApplication.enApplicationType.NewInternationalLicense.ToString();
            lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fees.ToString();
            lblDate.Text = DateTime.Now.ToShortDateString();
            lblStatusDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = clsBussniessUser.GetUserById(_User_Id).UserName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblDriverID.Text) || lblDriverID.Text == "[????]")
            {
                MessageBox.Show("Please Find the Driver First", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clsInternationalLicense.DoesPersonHaveActiveApplication(Convert.ToInt32(_PersonID),(int)clsApplication.enApplicationType.NewInternationalLicense))
            {
                MessageBox.Show("This person already has an International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //if (ActiveApplicationID != -1)
            //{
            //    MessageBox.Show("Choose another Driver, the selected Driver Already have an International License for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    return;
            //}
            clsInternationalLicense InternationalLicense = new clsInternationalLicense();
            InternationalLicense.DriverID = int.Parse(lblDriverID.Text);//
            InternationalLicense.IssuedUsingLocalLicenseID = int.Parse(lblLocalLicenseID.Text);//
            InternationalLicense.IssueDate = DateTime.Now;//
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(10);//
            InternationalLicense.IsActive = true;//
            InternationalLicense.CreatedByUserID = _User_Id;//
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicantPersonID = _PersonID;//
            InternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.New;//
            InternationalLicense.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;//
            InternationalLicense.ApplicationDate = DateTime.Now;//
            InternationalLicense.LastStatusDate = DateTime.Now;//
            InternationalLicense.PaidFees = float.Parse(lblFees.Text);//
          

            
            if (InternationalLicense.Save())
            {
                
                //lblApplicationID.Text = license.ApplicationID.ToString();
                lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
                btnIssue.Enabled = true;
                gbFilter.Enabled = false;
                MessageBox.Show($"International License Created Successfully with ID = {InternationalLicense.InternationalLicenseID}","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                
               
            }
            else
            {
                MessageBox.Show("Error in Creating International License");
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
