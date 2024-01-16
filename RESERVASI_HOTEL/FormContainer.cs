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
            defaultChildForm();
        }

        public void defaultChildForm()
        {
            FormHome form = new FormHome();
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }

        public void setMenuStrip(ToolStripItemClickedEventArgs e, Form form, string nameItemToolStrip)
        {
            if (e.ClickedItem.Text.Equals(nameItemToolStrip))
            {
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            setMenuStrip(e, new FormHome(), "Home");
            setMenuStrip(e, new FormDataKamar(), "Data Kamar");
            setMenuStrip(e, new FormDataCustomer(), "Data Customer");
            setMenuStrip(e, new FormDataResepsionis(), "Data Resepsionis");
            setMenuStrip(e, new FormDataTransaksi(), "Data Transaksi");
        }

        private void FormContainer_Load(object sender, EventArgs e)
        {

        }

        private void dataTransaksiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
