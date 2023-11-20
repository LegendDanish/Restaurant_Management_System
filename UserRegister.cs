using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace SHMS
{
    public partial class UserRegister : Form
    {
        public UserRegister()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string FirstName = textBox1.Text;
            string LastName = textBox2.Text;
            string Email = textBox3.Text;
            string Role = comboBox1.Text;
            String Contact = textBox5.Text;
            string Password = textBox6.Text;
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Role) || string.IsNullOrEmpty(Contact) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            // Check if the email is valid
            if (!IsValidEmail(Email))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            // Check if the password is strong enough
            if (!IsStrongPassword(Password))
            {
                MessageBox.Show("Please enter a strong password. Password must be at least 8 characters long and contain a mix of upper and lowercase letters, numbers, and symbols.");
                return;
            }

            // Establish connection to the database
            String connectionString = @"Data Source=(local);Initial Catalog=SH;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a SqlCommand object and set parameters
                using (SqlCommand command = new SqlCommand("INSERT INTO Person (first_name, last_name, email, Role, contact, password) VALUES (@first_name, @last_name, @email, @Role, @contact, @password)", connection))

                {
                    command.Parameters.AddWithValue("@first_name", FirstName);
                    command.Parameters.AddWithValue("@last_name", LastName);
                    command.Parameters.AddWithValue("@email", Email);
                    command.Parameters.AddWithValue("@Role", Role);
                    command.Parameters.AddWithValue("@contact", Contact);
                    command.Parameters.AddWithValue("@password", Password);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record inserted successfully!");
                    }
                    else
                    {
                        MessageBox.Show("An error occurred while inserting the record.");
                    }
                }
            }

        }
            private bool IsValidEmail(string email)
            {
                try
                {
                    var address = new System.Net.Mail.MailAddress(email);
                    return address.Address == email;
                }
                catch
                {
                    return false;
                }
            }

            private bool IsStrongPassword(string password)
            {
                if(password.Length >= 8 && password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit) && password.Any(char.IsSymbol))
            {
                return true;
            }
            else
            {
                return false;
            }
            }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form UserLogin = new UserLogin();
            UserLogin.Show();
            this.Hide();
        }
    }
    }

