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
    public partial class AddNewUser: Form
    {
        private int _User_Id =-1;
        public AddNewUser(int UserID)
        {
            InitializeComponent();
            //lblMode.Text = "Person Information";
            _User_Id = UserID;
            mode = Mode.Update;
            groupBox2.Enabled = false;


        }

        public AddNewUser()
        {
            InitializeComponent();
            mode = Mode.Add;
            
        }
        enum Mode
        {
            Add,
            Update
        }
        Mode mode ;

        private void AddNewUser_Load(object sender, EventArgs e)
        {
            cmbFilter.SelectedIndex = 0;

            if (mode == Mode.Add)
            {
                lblMode.Text = "Add New User";
            return;

            }

            
               
            
           //MessageBox.Show(_User_Id.ToString());
            clsBussniessUser user = clsBussniessUser.GetUserById(_User_Id);
            
            if (user != null)
            {
                clsBussinessPerson personinfo = clsBussinessPerson.FindPersonByID(user.PersonID);
            
            
                //lblMode.Text = "Person Information";
                lblPersonID.Text = personinfo.ID.ToString();
                lblFullName.Text = personinfo.FullName;
                
               if(personinfo.Gender == 0)
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
            if (user != null)
            {
                lblUserID.Text = user.UserId.ToString();
                txtUserName.Text = user.UserName;
                txtPassword.Text = user.Password;
                txtConfirm.Text = user.Password;
                chxisActive.Checked = user.IsActive == 1 ? true : false;
            }
            if (mode == Mode.Update)
            {
                lblMode.Text = "Update User";
            }

        }

        private void tabPersonInfo_Click(object sender, EventArgs e)
        {

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
                lblFullName.Text = dataTable.Rows[0]["FirstName"].ToString() + " " + dataTable.Rows[0]["SecondName"].ToString() + " " + dataTable.Rows[0]["ThirdName"].ToString() + " " + dataTable.Rows[0]["LastName"].ToString();

                //byte  Gender = Convert.ToByte(dataTable.Rows[0]["Gender"]);
                string Gender = dataTable.Rows[0]["Gender"].ToString();
               


                if (Gender == "Male")
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
                    if (lblGender.Text == "Male")
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

        private void button1_Click(object sender, EventArgs e)
        {
            if(lblPersonID.Text == "N/A" || lblPersonID.Text == null)
            {
                MessageBox.Show("Please select a person first","Error",MessageBoxButtons.OK,MessageBoxIcon.Error); return;
            }
            if(clsBussniessUser.FindUserByPersonID(Convert.ToInt32(lblPersonID.Text)))
            {
                MessageBox.Show("This person already has a user account", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            tabControl1.SelectedTab = tabLoginInfo;
        }

        private void tabLoginInfo_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lblPersonID.Text == "N/A" || lblPersonID.Text == null)
            {
                MessageBox.Show("Please select a person first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            if (clsBussniessUser.FindUserByPersonID(Convert.ToInt32(lblPersonID.Text)) && mode == Mode.Add)
            {
                MessageBox.Show("This person already has a user account", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtConfirm.Text))
            {
                MessageBox.Show("Please fill all the required fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (mode == Mode.Add)
            {
                clsBussniessUser user = new clsBussniessUser();
                user.PersonID = Convert.ToInt32(lblPersonID.Text);
                user.UserName = txtUserName.Text;
                user.Password = txtPassword.Text;
                user.IsActive = chxisActive.Checked ? 1 : 0;
                if (user.Save())
                {
                    MessageBox.Show("User added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblUserID.Text = clsBussniessUser.GetUserByUserName(txtUserName.Text).ToString();

                }
                else
                {
                    MessageBox.Show("An error occured while adding the user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                clsBussniessUser user =  clsBussniessUser.GetUserById(_User_Id);

                user.PersonID = Convert.ToInt32(lblPersonID.Text);
                user.UserName = txtUserName.Text;
                user.Password = txtPassword.Text;
                user.IsActive = chxisActive.Checked ? 1 : 0;
                //user.UserId = Convert.ToInt32(lblUserID.Text);
                if (user.Save())
                {
                    MessageBox.Show("User updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("An error occured while updating the user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
            lblMode.Text = "Update User";
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            //MessageBox.Show(clsBussniessUser.isUserExists(txtUserName.Text).ToString());
            if (clsBussniessUser.isUserExists(txtUserName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "This User Name is Already Taken");
            }
            else if( string.IsNullOrEmpty(txtUserName.Text))
            {
                e.Cancel= true;
                errorProvider1.SetError(txtUserName, "Please Enter Your User Name");


            }

            else
            {
                e.Cancel = false;
                errorProvider1.Clear();


            }
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtConfirm_Validating_1(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text != txtConfirm.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirm, "Password and Confirm Password do not match");
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
        // Define the delegate to pass the number back
        public delegate void NumberPassedEventHandler(int PersonID);

        // Define the event based on the delegate
       static public event NumberPassedEventHandler OnPersonIDPassed;
        private int _PersonID =-1;
        public void frm_OnPersonIDPassed(int PersonID)
        {
            //if (OnPersonIDPassed != null)
            //{
            //    OnPersonIDPassed(PersonID);
            //}
            if(PersonID != null)
            {

            _PersonID = PersonID;
                cmbFilter.SelectedIndex = 1;
                txtFilter.Text = _PersonID.ToString();
            }

        }
        private void pictureBox14_Click(object sender, EventArgs e)
        {
            frmAddUpdatePersoncs frm = new frmAddUpdatePersoncs();
            frm.NumberPassed += frm_OnPersonIDPassed;
            frm.ShowDialog();
        }
    }
}
