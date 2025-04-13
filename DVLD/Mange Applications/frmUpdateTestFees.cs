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
    public partial class frmUpdateTestFees: Form
    {
        private int _ID = -1;
        clsBussniessTestTypes testType;
        public frmUpdateTestFees(int ID)
        {
            InitializeComponent();
            _ID = ID;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void frmUpdateTestFees_Load(object sender, EventArgs e)
        {
            testType = clsBussniessTestTypes.Find(_ID);
            if (testType != null)
            {
                lblID.Text = testType.ID.ToString();
                txtTitle.Text = testType.TestTypeTitle;
                txtDescreption.Text = testType.TestTypeDescription;
                txtFees.Text = testType.TestTypeFees.ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            testType.TestTypeTitle = txtTitle.Text;
            testType.TestTypeDescription = txtDescreption.Text;
            testType.TestTypeFees = int.Parse(txtFees.Text);
            if (testType.Update())
            {
                MessageBox.Show("Test Type Updated Successfully","",MessageBoxButtons.OK,MessageBoxIcon.Information);
               
            }
            else
            {
                MessageBox.Show("Error Updating Test Type", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
