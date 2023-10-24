using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Xml.Linq;

namespace LibraryManagementSystem
{
    public partial class AddStudent : Form
    {
        public AddStudent()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Confirm?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtEnrollment.Clear();
            txtDepartment.Clear();
            txtSemester.Clear();
            txtContact.Clear();
            txtEmail.Clear();


        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            string name = txtName.Text;
            string enrollment = txtEnrollment.Text;
            string department = txtDepartment.Text;
            string semester = txtSemester.Text;
            string contactStr = txtContact.Text;
            string email = txtEmail.Text;

            if (name!= "" && enrollment != "" && department != "" && semester != "" && contactStr != "" && email != "") 
            
            { 

            if (Int64.TryParse(contactStr, out long contact))
            {
                try
                {
                    string connectionString = "";                                        //To Add
                        using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string query = "INSERT INTO NewStudent (sname, enroll, dep, sem, contact, email) " +
                                       "VALUES (@name, @enrollment, @department, @semester, @contact, @email)";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@enrollment", enrollment);
                            cmd.Parameters.AddWithValue("@department", department);
                            cmd.Parameters.AddWithValue("@semester", semester);
                            cmd.Parameters.AddWithValue("@contact", contact);
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Show that data is inserted
                    MessageBox.Show("Data Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid contact number. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            }   
            else
            {
                MessageBox.Show("Empty Fields Not Allowed", "Suggest", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


        }





    }
}
