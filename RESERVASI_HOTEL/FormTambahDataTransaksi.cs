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
    public partial class FormTambahDataTransaksi : Form
    {
        public int id_transaksi_edit = 0;

        public FormTambahDataTransaksi()
        {
            InitializeComponent();
            LoadDataCustomer();
            LoadDataKamar();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtNama_TextChanged(object sender, EventArgs e)
        {

        }

        public void LoadDataCustomer()
        {
            Koneksi.buka();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Koneksi.sqlConn;

            cmd.CommandText = " SELECT * FROM Customer ORDER BY nama DESC";
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                string id_customer = rd["id_customer"].ToString();
                string nama = rd["nama"].ToString();
                string no_telp = rd["no_telp"].ToString();
                cmbCustomer.Items.Add(id_customer + "-" + nama + "-" + no_telp);
            }

            rd.Close();
            cmd.Dispose();
            Koneksi.tutup();
        }


        public void LoadDataKamar()
        {
            Koneksi.buka();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Koneksi.sqlConn;
            cmd.CommandText = " SELECT * FROM Kamar WHERE stok > 0 ORDER BY jenis_kamar";

            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                string id_kamar = rd["id_kamar"].ToString();
                string nomor_kamar = rd["nomor_kamar"].ToString();
                string kapasitas = rd["kapasitas"].ToString();
                string jenis_kamar = rd["jenis_kamar"].ToString();
                string harga_per_malam = rd["harga_per_malam"].ToString();
                cmbKamar.Items.Add(id_kamar + "-" + nomor_kamar + "-" + kapasitas + "-" + jenis_kamar + "-" + harga_per_malam);
            }

            rd.Close();
            cmd.Dispose();
            Koneksi.tutup();

        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbKamar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] vSplit = cmbKamar.Text.Split('-');
            txtHargaPerMalam.Text = vSplit[4];
            numLamaMenginap.Value = 1;
            int hargaPerMalam = int.Parse(txtHargaPerMalam.Text);
            decimal hargaTotal = hargaPerMalam * numLamaMenginap.Value;
            txtHargaTotal.Text = hargaTotal.ToString();
        }

        private void numLamaMenginap_ValueChanged(object sender, EventArgs e)
        {
            int hargaPerMalam = int.Parse(txtHargaPerMalam.Text);
            decimal totalBiaya = hargaPerMalam * numLamaMenginap.Value;

            txtHargaTotal.Text = totalBiaya.ToString();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {

            try
            {
                Koneksi.buka();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Koneksi.sqlConn;

                string[] vSplit = cmbCustomer.Text.Split('-');
                string id_customer = vSplit[0];
                string[] vSplitKamar = cmbCustomer.Text.Split('-');
                string id_kamar = vSplitKamar[0];

                if (id_transaksi_edit == 0)
                {
                    //untuk tambah data transaksi
                    cmd.CommandText = " INSERT INTO Transaksi (id_customer, id_kamar, lama_menginap, " 
                        + " metode_pembayaran, tgl_transaksi, harga_sewa, harga_total, tgl_check_in, tgl_check_out, status_pemesanan) "
                        + " VALUES (@pIdCustomer, @pIdKamar,  @pLamaMenginap, @pMetodePembayaran, @pTglTransaksi, " 
                        + " @pHargaSewa, @pHargaTotal, @pTglCheckIn, @pTglCheckOut, @pStatusPemesanan) ";


                    int stockNow = checkTotalStockKamar(id_kamar);
                    int totalStock = stockNow - 1;
                    cmd.CommandText = " UPDATE Kamar SET stok = " + totalStock.ToString() + " where id_kamar = @pIdKamar ";


                    MessageBox.Show("Data transaksi berhasil disimpan");
                }
                else
                {
                    //untuk update data transaksi
                    cmd.CommandText = "UPDATE Transaksi SET "
                     + "id_customer = @pIdCustomer, "
                     + "id_kamar = @pIdKamar, "
                     + "lama_menginap = @pLamaMenginap, "
                     + "harga_total = @pHargaTotal "
                     + "harga_per_malam = @pHargaPerMalam, "
                     + "metode_pembayaran = @pMetodePembayaran "
                     + "tgl_transaksi = @pTglTransaksi "
                     + "tgl_check_in = @pTglCheckIn "
                     + "tgl_check_out = @pTglCheckOut "
                     + "status_pemesanan = @pStatusPemesanan "
                     + "WHERE id_kamar = @pID";

                    cmd.Parameters.AddWithValue("pID", id_transaksi_edit);

                    MessageBox.Show("Data transaksi berhasil diubah");
                }

                cmd.Parameters.AddWithValue("pIdCustomer", int.Parse(id_customer));
                cmd.Parameters.AddWithValue("pIdKamar", int.Parse(id_kamar));
                cmd.Parameters.AddWithValue("pLamaMenginap", numLamaMenginap.Value);
                cmd.Parameters.AddWithValue("pMetodePembayaran", cmbMetodePembayaran.Text);
                cmd.Parameters.AddWithValue("pHargaSewa", int.Parse(txtHargaPerMalam.Text));
                cmd.Parameters.AddWithValue("pHargaTotal", int.Parse(txtHargaTotal.Text));
                cmd.Parameters.AddWithValue("pTglTransaksi", dtpTanggalTransaksi.Value);
                cmd.Parameters.AddWithValue("pTglCheckIn", dtpTanggalCheckIn.Value);
                cmd.Parameters.AddWithValue("pTglCheckOut", dtpTanggalCheckOut.Value);
                cmd.Parameters.AddWithValue("pStatusPemesanan", cmbStatusPesanan.Text);
                cmd.ExecuteNonQuery();

                Koneksi.tutup();
                cmd.Dispose();

                //clear input setelah save
                cmbCustomer.Text = "";
                cmbKamar.Text = "";
                numLamaMenginap.Value = 0;
                txtHargaTotal.Text = "";
                cmbMetodePembayaran.Text = "";
                cmbStatusPesanan.Text = "";
            }
            catch(Exception err)
            {
                MessageBox.Show("Terjadi error saat menambahkan data" + err.Message, "Error",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           

        }

        //untuk update jumlah stock
        public int checkTotalStockKamar(string id_kamar)
        {
            Koneksi.buka();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Koneksi.sqlConn;

            cmd.CommandText = " SELECT * FROM Kamar WHERE id_kamar = " + id_kamar;
            SqlDataReader rd = cmd.ExecuteReader();
            string stock = "";
            int stockInt = 0;
            while (rd.Read())
            {
                stock = rd["stock"].ToString();
                stockInt = int.Parse(stock);

            }

            rd.Close();
            cmd.Dispose();
            Koneksi.tutup();

            return stockInt;
        }
    }
}
