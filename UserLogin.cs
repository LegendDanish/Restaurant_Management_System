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

                    // Create an SQL SELECT statement to retrieve role based on email and password
                    string roleQuery = "SELECT role FROM Person WHERE email = @email AND password = @password";

                    // Create a SqlCommand object and set parameters
                    using (SqlCommand roleCommand = new SqlCommand(roleQuery, connection))
                    {
                        roleCommand.Parameters.AddWithValue("@email", Email);
                        roleCommand.Parameters.AddWithValue("@password", Password);

                        // Execute the SELECT query to retrieve the role
                        object roleResult = roleCommand.ExecuteScalar();

                        if (roleResult != null) // Check if a role is retrieved
                        {
                            string userRole = roleResult.ToString();

                            // Check the role and open the corresponding form
                            if (userRole == "Manager")
                            {
                                MessageBox.Show("Login successful as Manager!");
                                Form managerDashboard = new ManagerDashboard();
                                managerDashboard.Show();
                                this.Hide();
                            }
                            else if (userRole == "Client")
                            {
                                MessageBox.Show("Login successful as Client!");
                          
                            // Open Client Dashboard or perform actions for clients
                        }
                            else if (userRole == "Employee")
                            {
                                MessageBox.Show("Login successful as Employee!");
                                // Open Employee Dashboard or perform actions for employees
                            }
                            else
                            {
                                MessageBox.Show("Invalid role. Please contact support.");
                            }
                        }
                        else
                        {
                            // User credentials are invalid, display an error message
                            MessageBox.Show("Invalid username or password. Please try again.");
                        }
                    }
                }
            

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    } 

