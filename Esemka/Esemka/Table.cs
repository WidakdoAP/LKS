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
using System.Xml.Serialization;
using System.Data.SqlClient;

namespace Esemka
{
    public partial class Table : Form
    {
        public Table()
        {
            InitializeComponent();
        }

        private void Table_Load(object sender, EventArgs e)
        {
            btnLoad();
            LoadTable();
        }

        private void TableButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag !=  null)
            {
                int TableID = (int)btn.Tag;
                string connStr = ConfigurationManager.ConnectionStrings["HovSedhep"].ConnectionString;
                string query = @"Select TransactionID, CustomerName
                                 From Transactions
                                 Where TableID = @TableID and Status = 'Ongoing'";

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TableID", TableID);

                        var reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            int TransactionID = reader.GetInt32(0);
                            string CostumerName = reader.GetString(1);

                            Seating seat = new Seating(TableID, TransactionID, CostumerName);
                            seat.ShowDialog();
                            LoadTable();
                        }
                        else
                        {
                            Reserve reserve = new Reserve(TableID);
                            reserve.ShowDialog();
                            LoadTable();
                        }
                    }
                }
            }
        }

        private void LoadTable()
        {
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["HovSedhep"].ConnectionString;
                string query = @"SELECT t.TableID, t.Name, t.Capacity, tr.TransactionID, tr.CustomerName, tr.Status
                                 FROM RestaurantTables t
                                 LEFT JOIN Transactions tr
                                 ON t.TableID = tr.TableID
                                 AND tr.Status IN ('Ongoing')";

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        var reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            int TableID = reader.GetInt32(0);
                            object CustomerObj = reader.IsDBNull(4) ? null : reader.GetString(4);

                            bool Occupied = CustomerObj != null;

                            foreach (Control c in gbButton.Controls)
                            {
                                if (c is Button btn && btn.Tag != null && (int)btn.Tag == TableID)
                                {
                                    btn.BackColor = Occupied ? Color.Yellow : Color.LightGray;
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Sybau " + ex.Message);
            }
        }

        private void btnLoad()
        {
            btnA1.Tag = 1;
            btnA2.Tag = 2;
            btnA3.Tag = 3;
            btnA4.Tag = 4;
            btnB1.Tag = 5;
            btnB2.Tag = 6;
            btnC1.Tag = 7;
            btnC2.Tag = 8;

            foreach (Control c in gbButton.Controls)
            {
                if (c is Button btn && btn.Tag != null)
                {
                    btn.Click += TableButton_Click;
                }
            }
        }

        private void btnA1_Click(object sender, EventArgs e)
        {

        }
    }
}
