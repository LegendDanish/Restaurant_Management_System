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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SHMS
{
    public partial class Clients : Form
    {
        public Clients()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string clientname = textBox2.Text;
            string Contactinfo = textBox3.Text;


            String connectionString = @"Data Source=(local);Initial Catalog=SH;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a SqlCommand object and set parameters
                using (SqlCommand command = new SqlCommand("INSERT INTO Clients (ClientName, ContactInfo) VALUES (@ClientName, @ContactInfo)", connection))

                {
                    command.Parameters.AddWithValue("@ClientName", clientname);
                    command.Parameters.AddWithValue("@ContactInfo", Contactinfo);



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
                string sqlQuery = "select * from Clients where Active=1";
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

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(local);Initial Catalog=SH;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create an SQL UPDATE statement to modify the specified project record
                using (SqlCommand command = new SqlCommand("Update Clients set ClientName = @ClientName, ContactInfo = @ContactInfo where @ClientID=ClientID", connection))

                {
                    command.Parameters.AddWithValue("@ClientID", textBox1.Text);
                    command.Parameters.AddWithValue("@ClientName", textBox2.Text);
                    command.Parameters.AddWithValue("@ContactInfo", textBox3.Text);



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
                SqlCommand cmd = new SqlCommand("SELECT * FROM  Clients", con);

                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["ClientId"].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

            }
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["DELETE"].Index)
            {
                string connectionString = @"Data Source=(local);Initial Catalog=SH;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create an SQL UPDATE statement to modify the specified project record
                    using (SqlCommand command = new SqlCommand("Update Clients set Active = 0 where @ClientID=ClientID", connection))
                    {
                        command.Parameters.AddWithValue("@ClientID", dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
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
