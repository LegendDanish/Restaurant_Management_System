using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SHMS
{
    public partial class Projects : Form
    {
        public Projects()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Title = textBox1.Text;
            string Description = textBox2.Text;
            int clientId = int.Parse(textBox3.Text);
            DateTime Start_date = DateTime.Parse(dateTimePicker1.Text);
            DateTime deadline = DateTime.Parse(dateTimePicker2.Text);
            String connectionString = @"Data Source=(local);Initial Catalog=SH;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a SqlCommand object and set parameters
                using (SqlCommand command = new SqlCommand("INSERT INTO Projects (Title, Description, ClientID, start_date, deadline) VALUES (@Title, @Description, @ClientID, @start_date, @deadline)", connection))

                {
                    command.Parameters.AddWithValue("@Title", Title);
                    command.Parameters.AddWithValue("@Description", Description);
                    command.Parameters.AddWithValue("@ClientID", clientId);
                    command.Parameters.AddWithValue("@start_date", Start_date);
                    command.Parameters.AddWithValue("@deadline", deadline);


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

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(local);Initial Catalog=SH;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create an SQL UPDATE statement to modify the specified project record
                using (SqlCommand command = new SqlCommand("Update Projects set Title = @Title, Description = @Description, ClientID = @ClientID, start_date = @start_date, deadline = @deadline where @ProjectID=ProjectID", connection))

                {
                    command.Parameters.AddWithValue("@ProjectID", textBox4.Text);
                    command.Parameters.AddWithValue("@Title", textBox1.Text);
                    command.Parameters.AddWithValue("@Description", textBox2.Text);
                    command.Parameters.AddWithValue("@ClientID", textBox3.Text);
                    command.Parameters.AddWithValue("@start_date", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("@deadline", dateTimePicker2.Value);


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

        private void button4_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(local);Initial Catalog=SH;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create an SQL UPDATE statement to modify the specified project record
                string sqlQuery = "select * from Projects";
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

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns["Edit"].Index == e.ColumnIndex)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM  Projects", con);
                
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["ProjectId"].Value.ToString();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                dateTimePicker2.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[7].Value);

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
                    using (SqlCommand command = new SqlCommand("Update Projects set Active = 0 where @ProjectID=ProjectID", connection))
                    {
                        command.Parameters.AddWithValue("@ProjectID", dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                        // Perform the delete operation based on the selected row
                        dataGridView1.Rows.RemoveAt(e.RowIndex);
                        MessageBox.Show("Deleted successfully!");
                    }
                }
            }
        }
    }
}

