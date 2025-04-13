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
    public partial class frmUpdateApplicationFees: Form
    {
        int _AppID = -1;
        clsBussniessApplicationTypes app;
        public frmUpdateApplicationFees(int AppID)
        {
            InitializeComponent();
            _AppID = AppID;

        }

        private void UpdateApplicationFees_Load(object sender, EventArgs e)
        {
             app = clsBussniessApplicationTypes.FindApplicationType(_AppID);
            if (app != null)
            {
                lblID.Text = app.ApplicationTypeID.ToString();
                txtTitle.Text = app.ApplicationTypeTitle;
                txtFees.Text = app.ApplicationFees.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            app.ApplicationTypeTitle = txtTitle.Text;
            app.ApplicationFees = decimal.Parse(txtFees.Text);
            if (app.UpdateApplicationType())
            {
                MessageBox.Show("Application Type Updated Successfully", "Update Application Type", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Failed to Update Application Type", "Update Application Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
