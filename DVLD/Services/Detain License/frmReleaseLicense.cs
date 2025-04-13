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
    public partial class frmReleaseLicense: Form
    {
        public frmReleaseLicense(int UserId)
        {
            InitializeComponent();
            _User_Id = UserId;
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
        clsApplication ReleaseApplication;
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnFind_Click(object sender, EventArgs e)
        {

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



                       clsDetainedLicense  DetainedLicense = clsDetainedLicense.FindByLicenseID(_LicenseID);

                        if (DetainedLicense != null)
                        {
                            if (DetainedLicense.IsReleased)
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
        }

        private void frmReleaseLicense_Load(object sender, EventArgs e)
        {

            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).Fees.ToString(); ;
            lblCreatedByUser.Text = clsBussniessUser.GetUserById(_User_Id).UserName;
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblDriverID.Text) || lblDriverID.Text == "[????]")
            {
                MessageBox.Show("Please Find the Driver First", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (lblIsdetained.Text == "No")
            {
                MessageBox.Show("This License is Not Detained Choose Detained one !!! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ReleaseApplication = new clsApplication();
            ReleaseApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            ReleaseApplication.ApplicationDate = DateTime.Now;
            ReleaseApplication.ApplicantPersonID = _PersonID;
            ReleaseApplication.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            ReleaseApplication.CreatedByUserID = _User_Id;
            ReleaseApplication.PaidFees = float.Parse(lblApplicationFees.Text);
            if(ReleaseApplication.Save())
            {
                 DetainedLicense = clsDetainedLicense.FindByLicenseID(_LicenseID);

                DetainedLicense.IsReleased = true;
                DetainedLicense.ReleaseDate = DateTime.Now;
                DetainedLicense.ReleasedByUserID = _User_Id;
                DetainedLicense.ReleaseApplicationID = ReleaseApplication.ApplicationID;
                //MessageBox.Show(DetainedLicense.ReleaseApplicationID.ToString());
                if (DetainedLicense.Save())
                {
                    lblApplicationID.Text = ReleaseApplication.ApplicationID.ToString();
                    lblFineFees.Text = DetainedLicense.FineFees.ToString();
                    lblDate.Text = DetainedLicense.DetainDate.ToShortDateString();
                    lblTotalFess.Text = (DetainedLicense.FineFees + float.Parse(lblApplicationFees.Text)).ToString();
                    lblDetainID.Text = DetainedLicense.DetainID.ToString();
                    MessageBox.Show("License Released Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblIsdetained.Text = "No";
                    gbFilter.Enabled = false;
                    btnRelease.Enabled = false;

                }
                else
                {
                    MessageBox.Show("Error in Updating Detained License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Error in Saving Release Application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
