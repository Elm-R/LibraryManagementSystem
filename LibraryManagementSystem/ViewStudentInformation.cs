using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class ViewStudentInformation : Form
    {
        public ViewStudentInformation()
        {
            InitializeComponent();
        }

        private void ViewStudentInformation_Load(object sender, EventArgs e)
        {

            panel2.Visible = false;

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "";                           //To Add
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM NewStudent";
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataSet DS = new DataSet();
            DA.Fill(DS);

            dataGridView1.DataSource = DS.Tables[0];


        }

        private void txtSearchEnrollment_TextChanged(object sender, EventArgs e)
        {

            if (txtSearchEnrollment.Text != "")
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "";                                       //To Add
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                // LIKE operator is used with a wildcard % to find records that start with the text entered in txtBookName.
                cmd.CommandText = "SELECT * FROM NewStudent where enroll LIKE '" + txtSearchEnrollment.Text + "%' ";
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DataSet DS = new DataSet();
                DA.Fill(DS);

                dataGridView1.DataSource = DS.Tables[0];

            }

            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "";                                                   //To Add
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM NewStudent";
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DataSet DS = new DataSet();
                DA.Fill(DS);


            }



        }

        int sid;
        Int64 rowid;


        // fill in the textboxes in bottom panel upon cell click
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            // which row is being clicked?

            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                sid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());


            }
            panel2.Visible = true;

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "";                                           //To Add
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM NewStudent where stuid = " + sid + " ";
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataSet DS = new DataSet();
            DA.Fill(DS);

            rowid = Int64.Parse(DS.Tables[0].Rows[0][0].ToString());


            txtSName.Text = DS.Tables[0].Rows[0][1].ToString();
            txtEnrollment.Text = DS.Tables[0].Rows[0][2].ToString();
            txtDepartment.Text = DS.Tables[0].Rows[0][3].ToString();
            txtSemester.Text = DS.Tables[0].Rows[0][4].ToString();
            txtContact.Text = DS.Tables[0].Rows[0][5].ToString();
            txtEmail.Text = DS.Tables[0].Rows[0][6].ToString();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Update?", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
               == DialogResult.OK)
            {
                string sName = txtSName.Text;
                string sEnrollment = txtEnrollment.Text;
                string sDepartment = txtDepartment.Text;
                string sSemester = txtSemester.Text;
                Int64 sContact = Int64.Parse(txtContact.Text);
                string sEmail = txtEmail.Text;

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "";                                       //To Add
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "update NewStudent set sname = '" + sName + "' , enroll = '" + sEnrollment + "' , " +
                    "dep = '" + sDepartment + "' , sem = '" + sSemester + "' , contact = " + sContact + " , email = '" + sEmail + "' " +
                    " where stuid = " + sid + "  ";  // where: to make sure only the desired row is affected 
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DataSet DS = new DataSet();
                DA.Fill(DS);

                // reload the panel to see the effect immediately
                ViewStudentInformation_Load(this, null);
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ViewStudentInformation_Load(this, null);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Delete?", "Confirmation Dialog", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
               == DialogResult.OK)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "";                                       //To Add
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "delete from NewStudent where stuid = " + rowid + " ";
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DataSet DS = new DataSet();
                DA.Fill(DS);


            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Unsaved Data Will Be Lost?", "Are You Sure", MessageBoxButtons.OKCancel, 
                MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }


    }
}
