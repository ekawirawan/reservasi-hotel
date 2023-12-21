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
    public partial class FormTambahDataKamar : Form
    {
        public int id_kamar_edit = 0;

        public FormTambahDataKamar()
        {
            InitializeComponent();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                Koneksi.buka();

                SqlCommand sqlCommand = new SqlCommand();
                SqlCommand cmd = sqlCommand;
                cmd.Connection = Koneksi.sqlConn;

                if (id_kamar_edit == 0)//jika insert data
                {
                    cmd.CommandText = "INSERT INTO Kamar( nomor_kamar, kapasitas, jenis_kamar, harga_per_malam, stok)"
                     + " VALUES(@pNoKamar, @pKapasitas, @pJenisKamar, @pHargaPerMalam, @pStok)";
                }
                else //jika update data
                {
                    cmd.CommandText = "UPDATE Kamar SET "
                     + "nomor_kamar = @pNoKamar, "
                     + "kapasitas = @pKapasitas, "
                     + "jenis_kamar = @pJenisKamar, "
                     + "harga_per_malam = @pHargaPerMalam, "
                     + "stok = @pStok "
                     + "WHERE id_kamar = @pID";
                    cmd.Parameters.AddWithValue("pID", id_kamar_edit);
                }

                cmd.Parameters.AddWithValue("pNoKamar", txtNoKamar.Text);
                cmd.Parameters.AddWithValue("pKapasitas", numKapasitas.Value);
                cmd.Parameters.AddWithValue("pJenisKamar", cmbJenisKamar.Text);
                cmd.Parameters.AddWithValue("pHargaPerMalam", int.Parse(txtHargaPerMalam.Text));
                cmd.Parameters.AddWithValue("pStok", numStok.Value);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                Koneksi.tutup();
            }
            catch (Exception err)
            {
                MessageBox.Show($"Terjadi error saat menginputkan data{err}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
