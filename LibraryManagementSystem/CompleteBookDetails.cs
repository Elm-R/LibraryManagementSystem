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
    public partial class CompleteBookDetails : Form
    {
        public CompleteBookDetails()
        {
            InitializeComponent();
        }

        private void CompleteBookDetails_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "";                                                   //To Add
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from IRBook where book_return_date IS NULL ";


            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataSet DS = new DataSet();
            DA.Fill(DS);

            // assign the data to the datagridview issued book
            dataGridView1.DataSource = DS.Tables[0];


            // assign the data to the datagridview returned book
            cmd.CommandText = "select * from IRBook where book_return_date IS NOT NULL ";

            SqlDataAdapter DA2 = new SqlDataAdapter(cmd);
            DataSet DS2 = new DataSet();
            DA2.Fill(DS2);

            dataGridView2.DataSource = DS2.Tables[0];

        }




    }
}
