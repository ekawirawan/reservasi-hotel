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
    public partial class FormContainer : Form
    {
        public FormContainer()
        {
            InitializeComponent();
        }

        public void formSetMdi(Form form)
        {
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text.Equals("Home"))
            {
               
                formSetMdi(new FormHome());
            }

            if (e.ClickedItem.Text.Equals("Data Kamar"))
            {
                formSetMdi(new FormDataKamar());
            }

            if (e.ClickedItem.Text.Equals("Data Customer"))
            {
                formSetMdi(new FormDataCustomer());
            }

            if (e.ClickedItem.Text.Equals("Data Resepsionis")) {
                formSetMdi(new FormDataResepsionis());
            }

            if (e.ClickedItem.Text.Equals("Data Transaksi"))
            {
                formSetMdi(new FormDataTransaksi());
            }
        }
    }
}
