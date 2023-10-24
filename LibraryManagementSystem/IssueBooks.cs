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
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace LibraryManagementSystem
{
    public partial class IssueBooks : Form
    {
        public IssueBooks()
        {
            InitializeComponent();
        }

        private void IssueBooks_Load(object sender, EventArgs e)
        {
            // connection
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "";                                                   //To Add
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd = new SqlCommand("SELECT bName FROM NewBook ", con);
            SqlDataReader Sdr = cmd.ExecuteReader();

            // insert data into the combo box

            while (Sdr.Read())
            {
                for (int i = 0; i < Sdr.FieldCount; i++)
                {
                    comboBoxBooks.Items.Add(Sdr.GetString(i));

                }

            }

            Sdr.Close();

            con.Close();



        }

        int count;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // fill the text boxes with the relevant information from the database
            

            if (!string.IsNullOrEmpty(txtEnrollment.Text))
            {
                string eid = txtEnrollment.Text;

                using (SqlConnection con = new SqlConnection(""))                                      //To Add
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM NewStudent WHERE enroll = @enroll", con))
                    {
                        cmd.Parameters.AddWithValue("@enroll", eid);

                        using (SqlDataAdapter DA = new SqlDataAdapter(cmd))
                        {
                            DataSet DS = new DataSet();
                            DA.Fill(DS);

                            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                            {
                                txtName.Text = DS.Tables[0].Rows[0]["sname"].ToString();
                                txtDep.Text = DS.Tables[0].Rows[0]["dep"].ToString();
                                txtSem.Text = DS.Tables[0].Rows[0]["sem"].ToString();
                                txtContact.Text = DS.Tables[0].Rows[0]["contact"].ToString();
                                txtEmail.Text = DS.Tables[0].Rows[0]["email"].ToString();
                            }
                            else
                            {
                                //  case where no matching record was found

                                txtName.Clear();
                                txtDep.Clear();
                                txtSem.Clear();
                                txtContact.Clear();
                                txtEmail.Clear();

                                MessageBox.Show("Invalid Enrollment No", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        // Count how many books have been issued to a certain enrollment number
                        cmd.CommandText = "SELECT COUNT(std_enroll) FROM IRBook WHERE std_enroll = @enroll AND book_return_date IS NULL";

                        int count; //  local count variable

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                count = reader.GetInt32(0); // Get the count value from the query result

                            }
                            else
                            {
                                count = 0; // Set count to 0 if no records were found
                            }
                        }

                        // Assign the count value to the global count variable
                        this.count = count;
                    }
                }
            }
            else
            {
                //  case where txtEnrollment.Text is empty

                txtName.Clear();
                txtDep.Clear();
                txtSem.Clear();
                txtContact.Clear();
                txtEmail.Clear();

                MessageBox.Show("Field is Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        private void btnIssueBook_Click(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(txtName.Text))
            {
                // Check if a book is selected and the student hasn't reached the maximum limit.
                if (comboBoxBooks.SelectedIndex != -1 && count <= 2)
                {
                   

                    // Get data from input fields
                    string enroll = txtEnrollment.Text;
                    string sname = txtName.Text;
                    string sdep = txtDep.Text;
                    string sem = txtSem.Text;
                    Int64 contact = Int64.Parse(txtContact.Text);
                    string email = txtEmail.Text;
                    string bookname = comboBoxBooks.Text;
                    string bookIssueDate = dateTimePicker.Text;

                    using (SqlConnection con = new SqlConnection(""))                            //To Add
                    {
                        con.Open();

                        // Use parameterized query to prevent SQL injection
                        string query = "INSERT INTO IRBook (std_name, std_enroll, std_dep, std_sem, std_contact, std_email, book_name, book_issue_date) " +
                                       "VALUES (@sname, @enroll, @sdep, @sem, @contact, @email, @bookname, @bookIssueDate)";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            // Add parameters to the query
                            cmd.Parameters.AddWithValue("@sname", sname);
                            cmd.Parameters.AddWithValue("@enroll", enroll);
                            cmd.Parameters.AddWithValue("@sdep", sdep);
                            cmd.Parameters.AddWithValue("@sem", sem);
                            cmd.Parameters.AddWithValue("@contact", contact);
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@bookname", bookname);
                            cmd.Parameters.AddWithValue("@bookIssueDate", bookIssueDate);

                            try
                            {
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Book Issue", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                //  to make sure the effects are reflected immediately after each issue attempt

                                txtName.Clear();
                                txtDep.Clear();
                                txtSem.Clear();
                                txtContact.Clear();
                                txtEmail.Clear();

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Select a Book or Maximum Number of Books Reached", "No Book Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Enter a Valid Enrollment No", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }





        }

        private void txtEnrollment_TextChanged(object sender, EventArgs e)
        {
            if (txtEnrollment.Text == "")
            {
                txtName.Clear();
                txtDep.Clear();
                txtSem.Clear();
                txtContact.Clear();
                txtEmail.Clear();


            }


        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtEnrollment.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButtons.OKCancel,
               MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();

            }

        }



    }
}
