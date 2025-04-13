using System;
using System.Windows.Forms;
using System.IO;
using DVLD_BussnessLayer_;

namespace DVLD
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            LoadCredentials();
        }
        private string filePath = "rememberme.txt"; // File to store login info
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void SaveCredentials(string username, string password)
        {
           
                File.WriteAllText(filePath, $"{username}\n{password}");
           
        }
        private void LoadCredentials()
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length >= 2)
                {
                    txtUserName.Text = lines[0];
                    txtPassword.Text = lines[1]; 
                    chxRememberMe.Checked = true;
                }
            }
        }

        private void ClearCredentials()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            int userId = clsBussniessUser.Login(txtUserName.Text, txtPassword.Text);
            if (userId == -1)
            {
                MessageBox.Show("Invalid User Name or Password or User is not Active","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                if (chxRememberMe.Checked)
                {
                    SaveCredentials(txtUserName.Text, txtPassword.Text);
                }
                else
                {
                    ClearCredentials();
                }
                Form1 frm = new Form1(userId);
                frm.FormClosed += (s, args) => this.Close();
                frm.Show();
                this.Hide();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chxShowPassword.Checked ? '\0' : '*';
        }
    }
}
