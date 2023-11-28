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
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string firstname = textBox2.Text;
            string lastname = textBox3.Text;
            string contactinfo = textBox4.Text;
            string skills = textBox5.Text;
            String connectionString = @"Data Source=(local);Initial Catalog=SH;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a SqlCommand object and set parameters
                using (SqlCommand command = new SqlCommand("INSERT INTO Employees (FirstName, LastName, ContactInfo, Skills) VALUES (@FirstName, @LastName, @ContactInfo, @Skills)", connection))

                {
                    command.Parameters.AddWithValue("@FirstName",firstname );
                    command.Parameters.AddWithValue("LastName", lastname);
                    command.Parameters.AddWithValue("@ContactInfo", contactinfo);
                    command.Parameters.AddWithValue("@Skills", skills);
                   


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

        private void button4_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(local);Initial Catalog=SH;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create an SQL UPDATE statement to modify the specified project record
                string sqlQuery = "select * from Employees";
                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection))
                {
                    DataTable dataTable = new DataTable();

                    // Fill the DataTable with data from the database
                    adapter.Fill(dataTable);

                    // Assuming you have a DataGridView named dataGridView1 on your form
                    // Set the DataTable as the DataGridView's DataSource
                    dataGridView1.DataSource = dataTable;
                }
            }
        }
    }
}
