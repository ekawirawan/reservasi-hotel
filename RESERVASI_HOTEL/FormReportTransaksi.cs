using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RESERVASI_HOTEL
{
    public partial class FormReportTransaksi : Form
    {
        public FormReportTransaksi()
        {
            InitializeComponent();
        }

        private void FormReportTransaksi_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.DataTableTransaksi' table. You can move, or remove it, as needed.
            this.DataTableTransaksiTableAdapter.Fill(this.DataSet1.DataTableTransaksi);

            this.reportViewer1.RefreshReport();
        }
    }
}
