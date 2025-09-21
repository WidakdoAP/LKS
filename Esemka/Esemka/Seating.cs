using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esemka
{
    public partial class Seating : Form
    {

        private int TableID;
        private int TransactionID;
        private string CostumerName;
        public Seating(int tableID, int transactionID, string costumerName)
        {
            InitializeComponent();
            TableID = tableID;
            TransactionID = transactionID;
            CostumerName = costumerName;
        }

        private void Seating_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["HovSedhep"].ConnectionString;


        }

    }
}
