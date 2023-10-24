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

namespace LibraryManagementSystem
{
    public partial class AddBooks : Form
    {
        public AddBooks()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // if not all textboxes contain text
            if (txtBookName.Text != "" && txtAuthor.Text != "" && txtPublication.Text != "" &&
                txtPrice.Text != "" && txtQuantity.Text != "")
            {

                try
                {
                    using (SqlConnection con = new SqlConnection(""))                       //To Add
                    {
                        con.Open();

                        //  parameterized queries 
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO NewBook (bName, bAuthor, bPubl, bPDate, bPrice, bQuan) " +
                            "values (@bName, @bAuthor, @Publication, @bDate, @price, @quantity)", con))
                        {
                            cmd.Parameters.AddWithValue("@bName", txtBookName.Text);
                            cmd.Parameters.AddWithValue("@bAuthor", txtAuthor.Text);
                            cmd.Parameters.AddWithValue("@Publication", txtPublication.Text);
                            cmd.Parameters.AddWithValue("@bDate", dateTimePicker1.Value); // Using Value property to get DateTime object
                            cmd.Parameters.AddWithValue("@price", Int64.Parse(txtPrice.Text));
                            cmd.Parameters.AddWithValue("@quantity", Int64.Parse(txtQuantity.Text));

                            cmd.ExecuteNonQuery();
                        }
                    }
                    // Make sure data is inserted
                    MessageBox.Show("Data Saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtBookName.Clear();
                    txtAuthor.Clear();
                    txtPublication.Clear();
                    txtPrice.Clear();
                    txtQuantity.Clear();



                }


                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            else
            {
                MessageBox.Show("Empty textbox not allowed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("This will delete your unsaved data", "Cancel", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                == DialogResult.OK)
            {
                this.Close();
            }

        }



    }
}
