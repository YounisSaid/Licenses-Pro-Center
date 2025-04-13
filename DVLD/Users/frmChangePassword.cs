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
    public partial class frmChangePassword: Form
    {
        private int _UserID;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            clsBussniessUser user = clsBussniessUser.GetUserById(_UserID);

            if (user != null)
            {
                clsBussinessPerson personinfo = clsBussinessPerson.FindPersonByID(user.PersonID);


                //lblMode.Text = "Person Information";
                lblPersonID.Text = personinfo.ID.ToString();
                lblFullName.Text = personinfo.FullName;
                
                lblGender.Text = personinfo.Gender.ToString();
                if (personinfo.Gender == 0)
                {
                    lblGender.Text = "Male";
                }
                else
                {
                    lblGender.Text = "Female";
                }
                lblAdress.Text = personinfo.Address;
                lblPhone.Text = personinfo.Phone;
                lblEmail.Text = personinfo.Email;
                lblNationalNo.Text = personinfo.NationalNo;
                lblCountry.Text = clsBussinessPerson.GetCountryName(personinfo.NationailtyCountryId);
                lblDateOfBirth.Text = personinfo.DateOfBirth.ToShortDateString();
                if (personinfo.ImagePath != "")
                {

                    PersonPicture.Load(personinfo.ImagePath);

                }
                else
                {
                    if (personinfo.Gender == 0)
                    {
                        PersonPicture.Image = Resources.person_man__2_;
                    }
                    else
                    {
                        PersonPicture.Image = Resources.person_little_girl;

                    }
                }
                lblUserName.Text = user.UserName;
                lblUserId.Text = user.UserId.ToString();
                if (user.IsActive == 0)
                {
                    lblIsActive.Text = "Not Active";
                }
                else
                {
                    lblIsActive.Text = "Active";
                }
            }
        }

        private void txtOldPassword_Validating(object sender, CancelEventArgs e)
        {
            if(!clsBussniessUser.PasswordMatches(_UserID, txtOldPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtOldPassword, "Old Password is not correct");
            }
            else if(string.IsNullOrEmpty(txtOldPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtOldPassword, "Please Enter Old Password");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtOldPassword, "");
            }
        }

        private void txtConfirm_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text != txtConfirm.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirm, "Password does not match");
            }
            else if (string.IsNullOrEmpty(txtConfirm.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirm, "Please Enter Confirm Password");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirm, "");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtConfirm.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtOldPassword.Text))
            {
                MessageBox.Show("Please Fill All Fields", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            clsBussniessUser User = clsBussniessUser.GetUserById(_UserID);
            User.Password = txtPassword.Text;
            if (User.Save())
            {
                MessageBox.Show("Password Changed Successfully", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Error Changing Password", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
