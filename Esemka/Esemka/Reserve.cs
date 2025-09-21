using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Esemka
{
    public partial class Reserve : Form
    {
        private int TableID;
        private int TableCapacity;
        public Reserve(int tableID)
        {
            InitializeComponent();
            this.TableID = tableID;
        }

        private void Reserve_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["HovSedhep"].ConnectionString;
            string cmbQuery = @"Select EmployeeID, Name From Employees Where Role = 'Waitress'";
            string nudQuery = @"Select Capacity From RestaurantTables Where TableID = @TableID";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                DataTable dt = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmbQuery, conn))
                {
                    
                    adapter.Fill(dt);
                }
                using (SqlCommand cmd = new SqlCommand(nudQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@TableID", TableID);
                    TableCapacity = (int)cmd.ExecuteScalar();
                }
                nudPax.Maximum = TableCapacity;
                nudPax.Minimum = 1;
                nudPax.Value = 1;

                cmbWaitress.DataSource = dt;
                cmbWaitress.DisplayMember = "Name";
                cmbWaitress.ValueMember = "EmployeeID";
            } 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
