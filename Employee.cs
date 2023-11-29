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
                string sqlQuery = "select * from Employees where Active=1";
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns["Edit"].Index == e.ColumnIndex)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM  Employees ", con);

                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["EmployeeId"].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();


            }
            /* if (e.ColumnIndex == dataGridView1.Columns["EDIT"].Index)
             {
                 textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["ProjectID"].Value.ToString();
                 textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                 textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                 textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                 dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                 dateTimePicker2.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
             }*/

            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["DELETE"].Index)
            {
                string connectionString = @"Data Source=(local);Initial Catalog=SH;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create an SQL UPDATE statement to modify the specified project record
                    using (SqlCommand command = new SqlCommand("Update Employees set Active = 0 where @EmployeeID=EmployeeID", connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeID", int.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()));
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            dataGridView1.Rows.RemoveAt(e.RowIndex);
                            MessageBox.Show("Deleted successfully!");
                        }
                        else
                        {
                            MessageBox.Show("An error occurred while deleting the record.");
                        }
                        // Perform the delete operation based on the selected row
                       
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(local);Initial Catalog=SH;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create an SQL UPDATE statement to modify the specified project record
                using (SqlCommand command = new SqlCommand("Update Employees set FirstName = @FirstName, LastName = @LastName, ContactInfo = @ContactInfo, Skills = @Skills where @EmployeeID=EmployeeID", connection))

                {
                    command.Parameters.AddWithValue("@EmployeeID", textBox1.Text);
                    command.Parameters.AddWithValue("@FirstName", textBox2.Text);
                    command.Parameters.AddWithValue("@LastName", textBox3.Text);
                    command.Parameters.AddWithValue("@ContactInfo", textBox4.Text);
                    command.Parameters.AddWithValue("@Skills", textBox5.Text);


                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record updated successfully!");
                    }
                    else
                    {
                        MessageBox.Show("An error occurred while updating the record.");
                    }
                }

            }
        }
    }
}
