using System;
using System.Data;
using System.Windows.Forms;
using BussnessLayer;

namespace DVLD
{
    public partial class MangePepole: Form
    {
        public MangePepole()
        {
            InitializeComponent();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePersoncs frmAddUpdatePersoncs = new frmAddUpdatePersoncs();

            frmAddUpdatePersoncs.FormClosed += CloseandUpdate;
            frmAddUpdatePersoncs.ShowDialog();

           

        }
        private void CloseandUpdate(object sender, FormClosedEventArgs e)
        {
            RefreshGrid();
        }

        private void MangePepole_Load(object sender, EventArgs e)
        {
            RefreshGrid();

        }
        void RefreshGrid()
        {
            DataTable dt = clsBussinessPerson.GetAllPepole();
            dataGridView1.DataSource = dt;
            cmbFilter.SelectedIndex = 0;
            lblRecords.Text = "# " + dt.Rows.Count.ToString() + " Record(s)";

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedID = (int)dataGridView1.CurrentRow.Cells[0].Value;
           
            frmAddUpdatePersoncs frmAddUpdatePersoncs = new frmAddUpdatePersoncs(selectedID);

            frmAddUpdatePersoncs.FormClosed += CloseandUpdate;
            frmAddUpdatePersoncs.ShowDialog();


        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ( MessageBox.Show("Are you sure you want to delete this person?", "Delete Person", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                if(clsBussinessPerson.DeletePerson((int)dataGridView1.CurrentRow.Cells[0].Value))
                {

                MessageBox.Show("Person Deleted Successfully", "Delete Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshGrid();
                }
                else
                {
                    MessageBox.Show("Person cannot be Deleted Because It has Data Linked to it","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            
        }

       

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            frmPersonInfo personInfo = new frmPersonInfo((int)dataGridView1.CurrentRow.Cells[0].Value);
            personInfo.ShowDialog();
        }

        private void FillFilterCombo()
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            switch (cmbFilter.SelectedItem.ToString())
            {
                case "ID":
                    if (int.TryParse(txtFilter.Text, out int personID))
                    {
                        dataGridView1.DataSource = clsBussinessPerson.FilterByPersonID(personID);
                    }
                    else
                    {
                        // Handle invalid input, e.g., clear the DataSource or show a message
                        DataTable dt = clsBussinessPerson.GetAllPepole();
                        dataGridView1.DataSource = dt;
                    }
                    break;
                case "First Name":
                    dataGridView1.DataSource = clsBussinessPerson.FilterByFirstName(txtFilter.Text);
                    break;
                case "Secound Name":
                    dataGridView1.DataSource = clsBussinessPerson.FilterBySecoundName(txtFilter.Text);
                    break;
                case "Third Name":
                    dataGridView1.DataSource = clsBussinessPerson.FilterByThirdName(txtFilter.Text);
                    break;
                case "Last Name":
                    dataGridView1.DataSource = clsBussinessPerson.FilterByLastName(txtFilter.Text);
                    break;
                case "National No.":
                    dataGridView1.DataSource = clsBussinessPerson.FilterByNationalNo(txtFilter.Text);
                    break;
                case "Phone":
                    dataGridView1.DataSource = clsBussinessPerson.FilterByPhone(txtFilter.Text);
                    break;
                case "Email":
                    dataGridView1.DataSource = clsBussinessPerson.FilterByEmail(txtFilter.Text);
                    break;
                case "Address":
                    dataGridView1.DataSource = clsBussinessPerson.FilterByAddress(txtFilter.Text);
                    break;
                case "Country":
                    dataGridView1.DataSource = clsBussinessPerson.FilterByCountry(txtFilter.Text);
                    break;
                case "Gender":
                    dataGridView1.DataSource = clsBussinessPerson.FilterByGender(txtFilter.Text);
                    break;
                case "Date Of Birth":
                    if (DateTime.TryParse(txtFilter.Text, out DateTime dateOfBirth))
                    {
                        dataGridView1.DataSource = clsBussinessPerson.FilterByDateOfBirth(dateOfBirth);
                    }
                    else
                    {
                        // Handle invalid date input, e.g., clear the DataSource or show a message
                        DataTable dt = clsBussinessPerson.GetAllPepole();
                        dataGridView1.DataSource = dt;
                        //MessageBox.Show("Please enter a valid date.");
                    }
                    break;
                default:
                    break;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
