using System;

using System.Data;

using System.Windows.Forms;
using DVLD_BussnessLayer_;

namespace DVLD
{
    public partial class FrmMangeUsers: Form
    {
        public FrmMangeUsers()
        {
            InitializeComponent();
        }

        private void FrmMangeUsers_Load(object sender, EventArgs e)
        {
            RefreshGrid();
            SetDimensions();
        }
        void SetDimensions()
        {
                dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 100;


        }
        private void RefreshGrid()
        {
            DataTable dt = clsBussniessUser.GetAllUsers();
            dataGridView1.DataSource = dt;
            cmbFilter.SelectedIndex = 0;
            lblRecords.Text = "# " + dt.Rows.Count.ToString() + " Record(s)";
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            AddNewUser frm = new AddNewUser();
            frm.FormClosed += CloseandUpdate;
            frm.ShowDialog();
            //RefreshGrid();
        }
        private void CloseandUpdate(object sender, FormClosedEventArgs e)
        {
            RefreshGrid();
        }

        private void edToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            AddNewUser frm = new AddNewUser(selectedID);
            frm.FormClosed += CloseandUpdate;
            frm.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            if (MessageBox.Show("Are you sure you want to delete this user?", "Delete User", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                if(clsBussniessUser.DeleteUser(selectedID))
                {
                    MessageBox.Show("User Deleted Successfully","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("User cannot be Deleted Because It has Data Linked to it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            RefreshGrid();

        }

        private void userInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmUserInfo frm = new frmUserInfo(selectedID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            frmChangePassword frm = new frmChangePassword(selectedID);
            frm.ShowDialog();
        }

        
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            switch (cmbFilter.SelectedItem.ToString())
            {
                case "User ID":
                    if (int.TryParse(txtFilter.Text, out int userID))
                    {
                        dataGridView1.DataSource = clsBussniessUser.FilterByUserId(userID);
                    }
                    else
                    {
                        DataTable dt = clsBussniessUser.GetAllUsers();
                        dataGridView1.DataSource = dt;
                    }
                    break;
                case "User Name":
                    if (txtFilter.Text == "")
                    {
                        DataTable dt = clsBussniessUser.GetAllUsers();
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {

                        dataGridView1.DataSource = clsBussniessUser.FilterByUserName(txtFilter.Text);
                    }
                    break;
                case "Person ID":
                    if (int.TryParse(txtFilter.Text, out int personID))
                    {
                        dataGridView1.DataSource = clsBussniessUser.FilterByPersonId(personID);
                    }
                    else
                    {
                        DataTable dt = clsBussniessUser.GetAllUsers();
                        dataGridView1.DataSource = dt;
                    }
                    break;
                case "Is Active":
                    if (int.TryParse(txtFilter.Text, out int isActive))
                    {
                        dataGridView1.DataSource = clsBussniessUser.FilterByIsActive(isActive);
                    }
                    else
                    {
                        DataTable dt = clsBussniessUser.GetAllUsers();
                        dataGridView1.DataSource = dt;
                    }
                    break;
                    case "Full Name":
                    if (txtFilter.Text == "")
                    {
                        DataTable dt = clsBussniessUser.GetAllUsers();
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        dataGridView1.DataSource = clsBussniessUser.FilterByFullName(txtFilter.Text);
                    }
                    break;
                default:
                    break;
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
    }
}
