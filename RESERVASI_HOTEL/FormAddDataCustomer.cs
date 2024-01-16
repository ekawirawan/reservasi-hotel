using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RESERVASI_HOTEL
{
    public partial class FormAddDataCustomer : Form
    {
        public int id_customer_edit = 0;

        public FormAddDataCustomer()
        {
            InitializeComponent();
        }

        private void btnSimpanDataCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                Koneksi.buka();

                SqlCommand sqlCommand = new SqlCommand();
                SqlCommand cmd = sqlCommand;
                cmd.Connection = Koneksi.sqlConn;

                if (id_customer_edit == 0)//jika insert data
                {
                    cmd.CommandText = " INSERT INTO Customer (nama, alamat, no_telp) "
                     + " VALUES(@pNama, @pAlamat, @pNo_telp) ";
                }
                else //jika update data
                {
                    cmd.CommandText = " UPDATE Customer SET "
                     + "nama = @pNama, "
                     + "alamat = @pAlamat, "
                     + "no_telp = @pNo_telp "
                     + "WHERE id_customer = @pID";
                    cmd.Parameters.AddWithValue("pID", id_customer_edit);
                }

                cmd.Parameters.AddWithValue("pNama", txtNamaCustomer.Text);
                cmd.Parameters.AddWithValue("pAlamat", txtAlamatCustomer.Text);
                cmd.Parameters.AddWithValue("pNo_telp", txtNoTelp.Text);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                Koneksi.tutup();
            }
            catch (Exception err)
            {
                MessageBox.Show($"Terjadi error saat menginputkan data {err}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
