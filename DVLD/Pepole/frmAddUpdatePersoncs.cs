using BussnessLayer;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmAddUpdatePersoncs: Form
    {
        public frmAddUpdatePersoncs()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddUpdatePersoncs(int id)
        {
            InitializeComponent();
            _personID = id;
            _Mode= enMode.Update;
        }
       public event AddNewUser.NumberPassedEventHandler NumberPassed;

        private void frmAddUpdatePersoncs_Load(object sender, EventArgs e)
        {
            LoadPersonData();
        }



        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;


        clsBussinessPerson Person;
        int _personID=-1;




        public void FillCountryCombo()
        {
            DataTable dt = clsBussinessPerson.GetAllCountries();

            cmbCountry.DataSource = dt;
            cmbCountry.DisplayMember = "CountryName";
            cmbCountry.ValueMember = "CountryId";

        }


        
        void LoadPersonData()
        {
            FillCountryCombo();
            rbMale.Checked = true;
            cmbCountry.Text = "Egypt";
            //DateTime time = new DateTime();
            dateTimePicker1.MaxDate = DateTime.Today.AddYears(-18);
            dateTimePicker1.Value = DateTime.Today.AddYears(-18);
            if (_Mode == enMode.AddNew)
            {
                //MessageBox.Show("Add New Personl");
                lblMode.Text = "Add New Person";
                //MessageBox.Show($"_PersonID: {_PersonID}");
                return;
            }

            Person = clsBussinessPerson.FindPersonByID(_personID);
            //MessageBox.Show($"_PersonID: {Person.ID}");
            if (Person != null)
            {
                lblPersonID.Text = Person.ID.ToString();
                txtFirst.Text = Person.FirstName;
                txtSecound.Text = Person.SecoundName;
                txtThird.Text = Person.ThirdName;
                txtLast.Text = Person.LastName;
                txtNationalNo.Text = Person.NationalNo;
                dateTimePicker1.Value = Person.DateOfBirth;
                txtAddress.Text = Person.Address;
                txtPhone.Text = Person.Phone;
                txtEmail.Text = Person.Email;
                cmbCountry.SelectedValue = Person.NationailtyCountryId;
                if (Person.ImagePath != "")
                {
                    PersonPicture.Load(Person.ImagePath);
                }

            }
            else
            {
                MessageBox.Show("NotFound");
            }
            if (_Mode == enMode.Update)
            {
                lblMode.Text = "Update Person";

            }

        }

       
















      

        
      

      


     

        private void rbMale_CheckedChanged_1(object sender, EventArgs e)
        {
            PersonPicture.Image = Resources.person_man__2_;

        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            PersonPicture.Image = Resources.person_little_girl;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirst.Text) || string.IsNullOrEmpty(txtSecound.Text) || string.IsNullOrEmpty(txtThird.Text) || string.IsNullOrEmpty(txtLast.Text)
               || string.IsNullOrEmpty(txtNationalNo.Text)
               || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Please fill all the required fields");
                return;
            }

            if (clsBussinessPerson.CheckNationalNo(txtNationalNo.Text))
            {
                MessageBox.Show("This National No is already exist");
                return;
            }
            clsBussinessPerson person = new clsBussinessPerson();
            person.FirstName = txtFirst.Text;
            person.SecoundName = txtSecound.Text;
            person.ThirdName = txtThird.Text;
            person.LastName = txtLast.Text;
            person.NationalNo = txtNationalNo.Text;
            person.DateOfBirth = dateTimePicker1.Value;
            person.Address = txtAddress.Text;
            person.Phone = txtPhone.Text;
            person.Email = txtEmail.Text;
            person.NationailtyCountryId = Convert.ToInt32(cmbCountry.SelectedValue);
            //person.ImagePath = txtImagePath.Text;
            if (PersonPicture.ImageLocation != null)
            {
                //MessageBox.Show(" mmage");

                person.ImagePath = PersonPicture.ImageLocation;
            }
            else
            {
                //MessageBox.Show("No mmage");
                person.ImagePath = "";
            }
            if (rbMale.Checked)
            {
                person.Gender = 0;

            }
            else
            {
                person.Gender = 1;
            }
            if (_Mode == enMode.AddNew)
            {
                if (person.Save())
                {
                    MessageBox.Show("Person Added Successfully");

                    lblMode.Text = "Update Person";
                    lblPersonID.Text = clsBussinessPerson.GetPersonId(txtNationalNo.Text).ToString();
                    //lblPersonID.Text = person.ID.ToString();

                }

            }
            else
            {
                person.ID = _personID;
                if (person.Save())
                {
                    MessageBox.Show("Person Updated Successfully");
                }


            }
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (clsBussinessPerson.CheckNationalNo(txtNationalNo.Text))
            {
                e.Cancel = true;
                errorProvider2.SetError(txtNationalNo, "This National No is already exist");

            }
            else
            {
                e.Cancel = false;
                errorProvider2.Clear();
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string pattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";

            if (Regex.IsMatch(email, pattern))
            {
                errorProvider2.SetError(txtEmail, ""); // Clear error
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                errorProvider2.SetError(txtEmail, "Invalid email! Please enter a valid @gmail.com address.");

            }
        }

        private void lblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                //string selectedFilePath = openFileDialog1.FileName;
                try
                {
                    string selectedFilePath = openFileDialog1.FileName;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                    PersonPicture.Load(selectedFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message);
                }
                //pictureBox1.Load(selectedFilePath);

            }
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNationalNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmAddUpdatePersoncs_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (int.TryParse(lblPersonID.Text, out int number))
            {
                // Trigger the event and pass the number back to Form
                NumberPassed?.Invoke(number);

                
            }
        }
    }
}
