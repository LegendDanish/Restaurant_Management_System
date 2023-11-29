using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHMS
{
    public partial class Task : Form
    {
        public Task()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int ProjectId = int.Parse(textBox2.Text);
            string Description = textBox3.Text;
            int AssigneeId = int.Parse(textBox4.Text);
            string Status = comboBox1.Text;
            String connectionString = @"Data Source=(local);Initial Catalog=SH;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a SqlCommand object and set parameters
                using (SqlCommand command = new SqlCommand("INSERT INTO Tasks (ProjectID, Description, AssigneeID, Status) VALUES (@ProjectID, @Description, @AssigneeID, @Status)", connection))

                {
                    command.Parameters.AddWithValue("@ProjectID", ProjectId);
                    command.Parameters.AddWithValue("@Description", Description);
                    command.Parameters.AddWithValue("@AssigneeID", AssigneeId);
                    command.Parameters.AddWithValue("@Status", Status);
                    

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
                string sqlQuery = "select * from Tasks where Active=1";
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
            Form ManagerDashboard = new ManagerDashboard();
            ManagerDashboard.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(local);Initial Catalog=SH;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create an SQL UPDATE statement to modify the specified project record
                using (SqlCommand command = new SqlCommand("Update Tasks set ProjectID = @ProjectID, Description = @Description, AssigneeID = @AssigneeID, Status = @Status where @TaskID=TaskID", connection))

                {
                    command.Parameters.AddWithValue("@TaskID", textBox1.Text);
                    command.Parameters.AddWithValue("@ProjectID", textBox2.Text);
                    command.Parameters.AddWithValue("@Description", textBox3.Text);
                    command.Parameters.AddWithValue("@AssigneeID", textBox4.Text);
                    command.Parameters.AddWithValue("@Status", comboBox1.Text);


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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns["Edit"].Index == e.ColumnIndex)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM  Tasks", con);

                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["TaskID"].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();

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
                    using (SqlCommand command = new SqlCommand("Update Tasks set Active = 0 where @TaskID=TaskID", connection))
                    {
                        command.Parameters.AddWithValue("@TaskID", dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());

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
                    }
                }
            }
        }
    }
}
