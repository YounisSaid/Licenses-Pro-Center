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
    public partial class frmMangeDetainedLicenses: Form
    {
        private int _UserID = -1;
        private DataTable _dtDetainedLicenses;
        public frmMangeDetainedLicenses(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            frmDetainLicense frmDetainLicense = new frmDetainLicense(_UserID);
            frmDetainLicense.ShowDialog();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            frmReleaseLicense frmReleaseLicense = new frmReleaseLicense(_UserID);
            frmReleaseLicense.ShowDialog();
        }

        private void frmMangeDetainedLicenses_Load(object sender, EventArgs e)
        {
            cmbFilter.Text = "None";
            RefershGrid();
        }
        void RefershGrid()
        {
             _dtDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();
            dataGridView1.DataSource = _dtDetainedLicenses;
            lblRecords.Text = "# " + _dtDetainedLicenses.Rows.Count.ToString() + " Record(s)";
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {

            {
                string FilterColumn = "";
                //Map Selected Filter to real Column name 
                switch (cmbFilter.Text)
                {
                    case "Detain ID":
                        FilterColumn = "DetainID";
                        break;
                    case "License ID":
                        FilterColumn = "LicenseID";
                        break;
                    case "Is Released":
                        {
                            FilterColumn = "IsReleased";
                            break;
                        };
                        

                   

                    case "Release Application ID":
                        FilterColumn = "ReleaseApplicationID";
                        break;

                    default:
                        FilterColumn = "None";
                        break;
                }


                //Reset the filters in case nothing selected or filter value conains nothing.
                if (txtFilter.Text.Trim() == "" || FilterColumn == "None")
                {
                    _dtDetainedLicenses.DefaultView.RowFilter = "";
                    lblRecords.Text = dataGridView1.Rows.Count.ToString();
                    return;
                }


                if (FilterColumn == "DetainID" || FilterColumn == "ReleaseApplicationID" || FilterColumn == "LicenseID")
                    //in this case we deal with numbers not string.
                    _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());
                else
                    _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilter.Text.Trim());

                lblRecords.Text = _dtDetainedLicenses.Rows.Count.ToString();

            }
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

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase DriverID or PersonID is selected.
            if (cmbFilter.Text == "Detain ID" || cmbFilter.Text == "Release Application ID" || cmbFilter.Text == "LicenseID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
