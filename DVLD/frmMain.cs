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
    public partial class Form1: Form
    {
        private int _UserID =-1;
        public Form1(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            //MessageBox.Show("Welcome User ID: " + UserID);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pepoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MangePepole mangePepole = new MangePepole();
            mangePepole.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMangeUsers mangeUsers = new FrmMangeUsers();
            mangeUsers.ShowDialog();
        }

        private void currentUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(_UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(_UserID);
            frm.ShowDialog();
        }

        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login frm = new Login();
            frm.FormClosed += (s, args) => this.Close();
            frm.Show();
            this.Hide();
        }

        private void mangeAoolicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMangeApplicationTypes frm = new frmMangeApplicationTypes();
            frm.ShowDialog();
        }

        private void mangeTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMangeTestTypes frm = new frmMangeTestTypes();
            frm.ShowDialog();
        }

        private void applicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newDrivingLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frmLocalDrivingLicenseApplication = new frmAddUpdateLocalDrivingLicenseApplication(_UserID);
            frmLocalDrivingLicenseApplication.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplication frmLocalDrivingLicenseApplication = new frmLocalDrivingLicenseApplication(_UserID);
            frmLocalDrivingLicenseApplication.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDrivers frmDrivers = new frmDrivers();

            frmDrivers.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalDrivingLicense frmNewInternationalDrivingLicense = new frmNewInternationalDrivingLicense(_UserID);
            frmNewInternationalDrivingLicense.ShowDialog();
        }

        private void lnternatonalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalDrivingLicenseApplications frmInternationalDrivingLicenseApplications = new frmInternationalDrivingLicenseApplications(_UserID);
            frmInternationalDrivingLicenseApplications.ShowDialog();
        }

        private void renewDrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLicense frmRenewLicense = new frmRenewLicense(_UserID);
            frmRenewLicense.ShowDialog();
        }

        private void replacementForLostOrDamgedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRelacement frmRelacementForLostOrDamgedLicenses = new frmRelacement(_UserID);
            frmRelacementForLostOrDamgedLicenses.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense frmDetainLicense = new frmDetainLicense(_UserID);
            frmDetainLicense.ShowDialog();
        }

        private void releaseLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseLicense frmReleaseLicense = new frmReleaseLicense(_UserID);
            frmReleaseLicense.ShowDialog();
        }

        private void mangeDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMangeDetainedLicenses frmMangeDetainedLicenses = new frmMangeDetainedLicenses(_UserID);
            frmMangeDetainedLicenses.ShowDialog();
        }
    }
}
