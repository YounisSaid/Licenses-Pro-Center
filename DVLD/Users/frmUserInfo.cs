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
    public partial class frmUserInfo: Form
    {
        int _UserID;
        public frmUserInfo(int Userid)
        {
            InitializeComponent();
            _UserID = Userid;
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            clsBussniessUser user = clsBussniessUser.GetUserById(_UserID);

            if (user != null)
            {
                clsBussinessPerson personinfo = clsBussinessPerson.FindPersonByID(user.PersonID);


                //lblMode.Text = "Person Information";
                lblPersonID.Text = personinfo.ID.ToString();
                lblFullName.Text = personinfo.FullName;
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
