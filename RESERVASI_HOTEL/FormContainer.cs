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
        bool mouseDown;
        private Point offset;
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
            setMenuStrip(e, new FormReportTransaksi(), "Laporan Tahunan");
        }

       

        private void dataTransaksiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }


        //
        private void mouseDown_event(object sender, MouseEventArgs e)
        {
            offset.X = e.X;
            offset.Y = e.Y;
            mouseDown = true;
        }

        private void mouseMove_event(object sender, MouseEventArgs e)
        {
            if(mouseDown == true)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point(currentScreenPos.X - offset.X, currentScreenPos.Y - offset.Y);
            }
        }

        private void mouseUp_E(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

       
    }
}
