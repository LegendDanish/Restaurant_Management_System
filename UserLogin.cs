using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHMS
{
    public partial class UserLogin : Form
    {
        public UserLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Retrieve user input from textboxes
            string Email = textBox1.Text;
            string Password = textBox2.Text;

            // Establish connection to the database
            String connectionString = @"Data Source=(local);Initial Catalog=SH;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create an SQL SELECT statement to validate user credentials
                string sqlQuery = "SELECT * FROM Person WHERE email = @email AND password = @password";

                // Create a SqlCommand object and set parameters
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@email", Email);
                    command.Parameters.AddWithValue("@password", Password);

                    // Execute the SELECT query and check if any records are found
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            // User credentials are valid, display a success message
                            MessageBox.Show("Login successful!");
                        }
                        else
                        {
                            // User credentials are invalid, display an error message
                            MessageBox.Show("Invalid username or password. Please try again.");
                        }
                    }
                }
            }
        }
    }
    } 

