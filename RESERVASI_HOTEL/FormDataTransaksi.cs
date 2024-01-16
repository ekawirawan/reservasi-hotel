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
    public partial class FormDataTransaksi : Form
    {
        public FormDataTransaksi()
        {
            InitializeComponent();
            LoadDataTransaksi();
        }

        public void LoadDataTransaksi()
        {
            try
            {
                Koneksi.buka();

                SqlCommand cmd = new SqlCommand();
                SqlDataReader rd;

                dataGridView1.Rows.Clear();

                //cmd.CommandText = " SELECT Kamar.no_kamar FROM Transaksi JOIN Kamar ON Transaksi.id_kamar = Kamar.id_kamar WHERE Transaksi.id_kamar = @id_kamar ";
                //cmd.Parameters.AddWithValue("pNoKamar", "%" + txtCariDataTransaksi.Text + "%");
                cmd.CommandText = "SELECT Transaksi.*, Kamar.nomor_kamar FROM Transaksi JOIN Kamar ON Transaksi.id_kamar = Kamar.id_kamar WHERE Kamar.nomor_kamar LIKE @pNomorKamar";
                
                cmd.Parameters.AddWithValue("pNomorKamar", "%" + txtCariDataTransaksi.Text + "%");
                cmd.Connection = Koneksi.sqlConn;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    int newIndex = dataGridView1.Rows.Add();
                    int indexStart = newIndex + 1;
                    dataGridView1.Rows[newIndex].Cells[0].Value = indexStart.ToString();
                    dataGridView1.Rows[newIndex].Cells[1].Value = rd["id_transaksi"].ToString();
                    dataGridView1.Rows[newIndex].Cells[2].Value = rd["id_kamar"].ToString();
                    dataGridView1.Rows[newIndex].Cells[3].Value = rd["id_customer"].ToString();
                    dataGridView1.Rows[newIndex].Cells[4].Value = rd["lama_menginap"].ToString();
                    dataGridView1.Rows[newIndex].Cells[5].Value = rd["metode_pembayaran"].ToString();
                    dataGridView1.Rows[newIndex].Cells[6].Value = rd["tgl_transaksi"].ToString();
                    dataGridView1.Rows[newIndex].Cells[7].Value = rd["harga_sewa"].ToString();
                    dataGridView1.Rows[newIndex].Cells[8].Value = rd["harga_total"].ToString();
                    dataGridView1.Rows[newIndex].Cells[9].Value = rd["tgl_check_in"].ToString();
                    dataGridView1.Rows[newIndex].Cells[10].Value = rd["tgl_check_out"].ToString();
                    dataGridView1.Rows[newIndex].Cells[11].Value = rd["status_pemesanan"].ToString();
                    dataGridView1.Rows[newIndex].Cells[12].Value = "EDIT";
                    dataGridView1.Rows[newIndex].Cells[13].Value = "DELETE";
                }

                cmd.Dispose();
                rd.Close();
                Koneksi.tutup();
            }
            catch (Exception err)
            {
                MessageBox.Show("Terjadi error saat mengambil data" + err.Message, "Error",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 12) //jika klik EDIT
            {

                FormTambahDataTransaksi frm = new FormTambahDataTransaksi();

                frm.StartPosition = FormStartPosition.CenterParent;
                frm.Text = "Edit Data Transaksi";
                frm.id_transaksi_edit = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                frm.cmbKamar.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.cmbCustomer.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                frm.numLamaMenginap.Value = decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                frm.cmbMetodePembayaran.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                frm.dtpTanggalTransaksi.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
                frm.txtHargaPerMalam.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                frm.txtHargaTotal.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                frm.dtpTanggalCheckIn.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString());
                frm.dtpTanggalCheckOut.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString());
                frm.cmbStatusPesanan.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();


                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadDataTransaksi();
                } 
            }

            if (e.ColumnIndex == 13) //Jika klik DELETE
            {
                if (MessageBox.Show("Apakah yakin menghapus data ini?",
                    "Konfirmasi",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    return;
                }

                Koneksi.buka();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Koneksi.sqlConn;
                cmd.CommandText = "DELETE FROM Transaksi WHERE id_transaksi = @pID";
                cmd.Parameters.AddWithValue("pID", dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                Koneksi.tutup();
                LoadDataTransaksi();

            }
        }

        private void btnTambahTransaksi_Click(object sender, EventArgs e)
        {
            FormTambahDataTransaksi frm = new FormTambahDataTransaksi();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Text = "Insert Data Transaksi Baru";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataTransaksi();
            }
        }

        private void txtCariDataTransaksi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadDataTransaksi();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
