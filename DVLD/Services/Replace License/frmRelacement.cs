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
    public partial class frmRelacement: Form
    {
        public frmRelacement(int UserID)
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
        private void rbLost_CheckedChanged(object sender, EventArgs e)
        {
            lblMode.Text = "Replacement For Lost License";
            frmRelacement.ActiveForm.Text = "Replacement For Lost License";
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceLostDrivingLicense).Fees.ToString();

        }

        private void rbDamaged_CheckedChanged(object sender, EventArgs e)
        {
            lblMode.Text = "Replacement For Damaged License";
            frmRelacement.ActiveForm.Text = "Replacement For Damaged License";
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense).Fees.ToString();

        }

        private void frmRelacement_Load(object sender, EventArgs e)
        {
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense).Fees.ToString();
            //lblRenewIssueDate.Text = DateTime.Now.ToShortDateString();
            lblAppllicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = clsBussniessUser.GetUserById(_User_Id).UserName;
            rbLost.Checked = true;
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
                    
                    lblOldLicenseID.Text = lblLocalLicenseID.Text;
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            {
                if (string.IsNullOrEmpty(lblDriverID.Text) || lblDriverID.Text == "[????]")
                {
                    MessageBox.Show("Please Find the Driver First", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                if (!License.IsActive)
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
                        if(rbDamaged.Checked)
                        {
                        clsNewLocalDrivingLicenseApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
                        clsNewLocalDrivingLicenseApplication.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense).Fees;


                        }
                        if (rbLost.Checked)
                        {
                            clsNewLocalDrivingLicenseApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;
                            clsNewLocalDrivingLicenseApplication.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceLostDrivingLicense).Fees;
                        }
                        clsNewLocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                        clsNewLocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
                        clsNewLocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
                        clsNewLocalDrivingLicenseApplication.CreatedByUserID = _User_Id;
                        clsNewLocalDrivingLicenseApplication.LicenseClassID = clsLocalDrivingLicenseApplication.LicenseClassID;
                        if (clsNewLocalDrivingLicenseApplication.Save())
                        {
                            NewLicense = new clsLicense();
                            NewLicense.ApplicationID = clsNewLocalDrivingLicenseApplication.ApplicationID;
                            NewLicense.DriverID = Driver.DriverID;
                            NewLicense.LicenseClass = clsLocalDrivingLicenseApplication.LicenseClassID;
                            NewLicense.IssueDate = License.IssueDate;
                            NewLicense.ExpirationDate = License.ExpirationDate;
                            

                            NewLicense.PaidFees = 0;
                            NewLicense.IsActive = true;
                            if (rbDamaged.Checked)
                            {
                                NewLicense.Notes = "Replacement For Damaged License";
                                NewLicense.IssueReason = 3;
                            }
                            if (rbLost.Checked)
                            {
                                NewLicense.Notes = "Replacement For Lost License";
                                NewLicense.IssueReason = 4;
                            }
                            NewLicense.CreatedByUserID = _User_Id;
                            int NewLicenseID = NewLicense.Add();
                            if (NewLicenseID != -1)
                            {
                                lblRLApplicationID.Text = clsNewLocalDrivingLicenseApplication.ApplicationID.ToString();
                                lblRLLicenseID.Text = NewLicenseID.ToString();

                                MessageBox.Show("License Replaced Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnIssue.Enabled = false;
                                gbFilter.Enabled = false;
                            }
                            else
                            {
                                MessageBox.Show("Error in Replacing License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                    }
                }


            }
        }
    }
}
