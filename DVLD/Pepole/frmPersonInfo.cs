using BussnessLayer;
using DVLD.Properties;
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
    public partial class frmPersonInfo: Form
    {
        public frmPersonInfo(int id)
        {
            InitializeComponent();
           
            clsBussinessPerson personinfo = clsBussinessPerson.FindPersonByID(id);
            lblMode.Text = "Person Information";
            lblPersonID.Text = personinfo.ID.ToString();
            lblFullName.Text = personinfo.FullName;
            
            //lblGender.Text = personinfo.Gender.ToString();

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
        }

        private void frmPersonInfo_Load(object sender, EventArgs e)
        {

        }

        private void lblGender_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
