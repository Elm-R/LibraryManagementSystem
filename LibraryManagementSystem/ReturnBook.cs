using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace LibraryManagementSystem
{
    public partial class ReturnBook : Form
    {
        public ReturnBook()
        {
            InitializeComponent();
        }

        private void btnSearchStudent_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "";                                       //To Add
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from IRBook where std_enroll = '" + txtEnterEnroll.Text + "'" +
                "and book_return_date IS NULL ";
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataSet DS = new DataSet();
            DA.Fill(DS);

            if (DS.Tables[0].Rows.Count != 0)
            {
                dataGridView1.DataSource = DS.Tables[0];

            }

            else
            {
                MessageBox.Show("Invalid ID or No Book Issued", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void ReturnBook_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            txtEnterEnroll.Clear();


        }

        string bname;
        string bdate;
        Int64 rowid;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            // which row is being clicked?

            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                rowid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                bname = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                bdate = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            }

            panel2.Visible = true;

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "";                                           //To Add


            // assign the values of the variables to the text boxes


            txtBookName.Text = bname;
            txtBookIssueDate.Text = bdate;


        }

        private void btnReturn_Click(object sender, EventArgs e)
        {

            // SqlConnection
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "";                                               //To Add

            // SqlCommand
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            try
            {
                con.Open();

                // parameterized query
                cmd.CommandText = "UPDATE IRBook SET book_return_date = @returnDate WHERE std_enroll = @enroll AND id = @rowid";

                // Add parameters
                cmd.Parameters.AddWithValue("@returnDate", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@enroll", txtEnterEnroll.Text);
                cmd.Parameters.AddWithValue("@rowid", rowid);

                // Execute the query
                cmd.ExecuteNonQuery();

                MessageBox.Show("Return Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                con.Close();
            }

            // Refresh the data if necessary
            ReturnBook_Load(this, null);


        }


        private void txtEnterEnroll_TextChanged(object sender, EventArgs e)
        {
            if (txtEnterEnroll.Text == "")
            {
                panel2.Visible = false;
                dataGridView1.DataSource = null;
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtEnterEnroll.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel2.Visible=false;
        }




    }
}
