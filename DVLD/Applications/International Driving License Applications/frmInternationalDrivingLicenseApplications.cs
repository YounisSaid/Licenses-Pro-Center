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
    public partial class frmInternationalDrivingLicenseApplications: Form
    {
        private int _UserID;
        private DataTable _dtInternationalLicenseApplications;

        public frmInternationalDrivingLicenseApplications(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void RefreshGrid()
        {
             _dtInternationalLicenseApplications = clsInternationalLicense.GetAllInternationalLicenses();
            dataGridView1.DataSource = _dtInternationalLicenseApplications;
            
            
            lblRecords.Text = "# " + _dtInternationalLicenseApplications.Rows.Count.ToString() + " Record(s)";

        }

        private void frmInternationalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            cmbFilter.Text = "None";
            
            RefreshGrid();
            SetDimensions();
        }
        void SetDimensions()
        {
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.Columns[4].Width = 150;
            dataGridView1.Columns[5].Width = 150;

        }
        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmNewInternationalDrivingLicense frmNewInternationalDrivingLicense = new frmNewInternationalDrivingLicense(_UserID);
            frmNewInternationalDrivingLicense.ShowDialog();
        }

        private void showPersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsInternationalLicense internationalLicense = clsInternationalLicense.Find(SelectedID);
            frmPersonInfo frmPersonInfo = new frmPersonInfo(internationalLicense.ApplicantPersonID);
            frmPersonInfo.ShowDialog();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseCard frmInternationalLicenseCard = new frmInternationalLicenseCard((int)dataGridView1.CurrentRow.Cells[0].Value);
            frmInternationalLicenseCard.ShowDialog();
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilter.SelectedIndex != 0)
            {
                txtFilter.Visible = true;
                txtFilter.Focus();
            }
            else
            {
                txtFilter.Visible = false;
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            txtFilter.Visible = true;

            switch (cmbFilter.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    {
                        FilterColumn = "ApplicationID";
                        break;
                    }
                    ;

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Is Active":
                    FilterColumn = "IsActive";
                    break;


                default:
                    FilterColumn = "None";
                    txtFilter.Visible = false;
                    break;
            }


            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
                lblRecords.Text = dataGridView1.Rows.Count.ToString();
                return;
            }



            _dtInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());

            lblRecords.Text = _dtInternationalLicenseApplications.Rows.Count.ToString();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow numbers only because all filters are numbers.
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
