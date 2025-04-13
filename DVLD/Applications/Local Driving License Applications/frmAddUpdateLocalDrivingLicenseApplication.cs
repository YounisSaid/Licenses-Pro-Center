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
    public partial class frmAddUpdateLocalDrivingLicenseApplication: Form
    {
        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _SelectedPersonID = -1;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        
        public int _User_Id = -1;

        clsBussinessPerson personinfo;


        public frmAddUpdateLocalDrivingLicenseApplication(int UserID,int _LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            //lblMode.Text = "Person Information";
            _User_Id = UserID;
            _Mode = enMode.Update;
            this._LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            groupBox2.Enabled = false;


        }

        public frmAddUpdateLocalDrivingLicenseApplication(int UserID)
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            _User_Id = UserID;


        }
        private void _FillLicenseClassesInComoboBox()
        {
            DataTable dtLicenseClasses = clsLicenseClass.GetAllLicenseClasses();

            foreach (DataRow row in dtLicenseClasses.Rows)
            {
                cbLicenseClass.Items.Add(row["ClassName"]);
            }
        }
        private void frmLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _FillLicenseClassesInComoboBox();


            if (_Mode == enMode.AddNew)
            {
            cmbFilter.SelectedIndex = 0;
                lblMode.Text = "New Local Driving License Application";
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();

            cbLicenseClass.SelectedIndex = 2;


                lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).Fees.ToString();
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                lblCreatedByUser.Text = clsBussniessUser.GetUserById(_User_Id).UserName;
                return;

            }



            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);




            lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text =_LocalDrivingLicenseApplication.ApplicationDate.ToString();
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName);
            lblFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblCreatedByUser.Text = clsBussniessUser.GetUserById(_LocalDrivingLicenseApplication.CreatedByUserID).UserName;

            //MessageBox.Show(_User_Id.ToString());
            

            if (_LocalDrivingLicenseApplication != null)
            {
                 personinfo = _LocalDrivingLicenseApplication.PersonInfo;


                //lblMode.Text = "Person Information";
                lblPersonID.Text = personinfo.ID.ToString();
                _SelectedPersonID = personinfo.ID;
                lblFullName.Text = personinfo.FullName;
                
                //lblGender.Text = personinfo.Gender.ToString();
                if(personinfo.Gender ==0)
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
            
            if (_Mode == enMode.Update)
            {
                lblMode.Text = "Update Local Driving License Application";
            }

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            switch (cmbFilter.SelectedItem.ToString())
            {
                //case "None": txtFilter.Visible = false; break;
                case "Person ID":
                    //txtFilter.Visible = true;
                    if (int.TryParse(txtFilter.Text, out int personID))
                    {
                        //dataGridView1.DataSource = clsBussinessPerson.FilterByPersonID(personID);
                        dataTable = clsBussinessPerson.FilterByPersonID(personID);
                    }
                    else
                    {
                        // Handle invalid input, e.g., clear the DataSource or show a message
                        dataTable.Clear();
                    }
                    break;
                case "National No.":
                    //txtFilter.Visible = true;

                    if (txtFilter.Text.Length > 0)
                    {

                        dataTable = clsBussinessPerson.FilterByNationalNo(txtFilter.Text);
                    }

                    break;

                default:
                    break;
            }
            if (dataTable.Rows.Count > 0)
            {

                lblPersonID.Text = dataTable.Rows[0]["PersonID"].ToString();
                lblFullName.Text = dataTable.Rows[0]["FirstName"].ToString() + " "+ dataTable.Rows[0]["SecondName"].ToString() + " "+ dataTable.Rows[0]["ThirdName"].ToString() + " " + dataTable.Rows[0]["LastName"].ToString();
            
                
                string Gender = dataTable.Rows[0]["Gender"].ToString();
                if(Gender == "Male")
                {
                    lblGender.Text = "Male";
                }
                else
                {
                    lblGender.Text = "Female";
                }
                lblAdress.Text = dataTable.Rows[0]["Address"].ToString();
                lblPhone.Text = dataTable.Rows[0]["Phone"].ToString();
                lblEmail.Text = dataTable.Rows[0]["Email"].ToString();
                lblCountry.Text = dataTable.Rows[0]["CountryName"].ToString();
                DateTime DateOfBirth = Convert.ToDateTime(dataTable.Rows[0]["DateOfBirth"]);
                lblDateOfBirth.Text = DateOfBirth.ToShortDateString();
                lblNationalNo.Text = dataTable.Rows[0]["NationalNo"].ToString();
                if (dataTable.Rows[0]["ImagePath"].ToString() != "")
                {
                    PersonPicture.Load(dataTable.Rows[0]["ImagePath"].ToString());
                }
                else
                {
                    if (Gender == "0")
                    {
                        PersonPicture.Image = Resources.person_man__2_;
                    }
                    else
                    {
                        PersonPicture.Image = Resources.person_little_girl;

                    }
                }
            }
            else
            {
                lblPersonID.Text = "N/A";
                lblFullName.Text = "????";
                
                lblGender.Text = "????";
                lblAdress.Text = "????";
                lblPhone.Text = "????";
                lblEmail.Text = "????";
                lblCountry.Text = "????";
                lblDateOfBirth.Text = "????";
                lblNationalNo.Text = "????";
                PersonPicture.Image = null;
            }

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lblPersonID.Text == "N/A" || lblPersonID.Text == null)
            {
                MessageBox.Show("Please select a person first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            if (clsLocalDrivingLicenseApplication.DoesPersonHaveActiveApplication(Convert.ToInt32(lblPersonID.Text), 1))
            {
                MessageBox.Show("This person already has an open Application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            

            int LicenseClassID = clsLicenseClass.Find(cbLicenseClass.Text).LicenseClassID;


            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_SelectedPersonID, clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;
            }
            _LocalDrivingLicenseApplication.ApplicantPersonID = Convert.ToInt32(lblPersonID.Text); ;
            _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplication.ApplicationTypeID = 1;
            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplication.PaidFees = Convert.ToSingle(lblFees.Text);
            _LocalDrivingLicenseApplication.CreatedByUserID = _User_Id;
            _LocalDrivingLicenseApplication.LicenseClassID = LicenseClassID;


            if (_LocalDrivingLicenseApplication.Save())
            {
                lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblMode.Text = "Update Local Driving License Application";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);



        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lblPersonID.Text == "N/A" || lblPersonID.Text == null)
            {
                MessageBox.Show("Please select a person first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            if (clsLocalDrivingLicenseApplication.DoesPersonHaveActiveApplication(Convert.ToInt32(lblPersonID.Text),1))
            {
                MessageBox.Show("This person already has an open Application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            tabControl1.SelectedTab = tabLoginInfo;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void frm_OnPersonIDPassed(int PersonID)
        {
            
            if (PersonID != null)
            {

                _SelectedPersonID = PersonID;
                cmbFilter.SelectedIndex = 1;
                txtFilter.Text = _SelectedPersonID.ToString();
            }

        }
        private void pictureBox14_Click(object sender, EventArgs e)
        {
            frmAddUpdatePersoncs frm = new frmAddUpdatePersoncs();
            frm.NumberPassed += frm_OnPersonIDPassed;
            frm.ShowDialog();
        }

        private void tabLoginInfo_Click(object sender, EventArgs e)
        {

        }
    }
    }







