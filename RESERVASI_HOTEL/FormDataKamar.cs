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
    public partial class FormDataKamar : Form
    {
        public FormDataKamar()
        {
            InitializeComponent();
            LoadDataKamar();
        }

        public void LoadDataKamar()
        {
            try
            {
                Koneksi.buka();

                SqlCommand cmd = new SqlCommand();
                SqlDataReader rd;

                dataGridView1.Rows.Clear();
                cmd.CommandText = " SELECT * FROM Kamar WHERE nomor_kamar LIKE @pNoKamar ORDER BY id_kamar ";
                cmd.Parameters.AddWithValue("pNoKamar", "%" + txtCariNoKamar.Text + "%");
                cmd.Connection = Koneksi.sqlConn;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    int newIndex = dataGridView1.Rows.Add();
                    int indexStart = newIndex + 1;
                    dataGridView1.Rows[newIndex].Cells[0].Value = indexStart.ToString();
                    dataGridView1.Rows[newIndex].Cells[1].Value = rd["id_kamar"].ToString();
                    dataGridView1.Rows[newIndex].Cells[2].Value = rd["nomor_kamar"].ToString();
                    dataGridView1.Rows[newIndex].Cells[3].Value = rd["kapasitas"].ToString();
                    dataGridView1.Rows[newIndex].Cells[4].Value = rd["jenis_kamar"].ToString();
                    dataGridView1.Rows[newIndex].Cells[5].Value = rd["harga_per_malam"].ToString();
                    dataGridView1.Rows[newIndex].Cells[6].Value = rd["stok"].ToString();
                    dataGridView1.Rows[newIndex].Cells[7].Value = "EDIT";
                    dataGridView1.Rows[newIndex].Cells[8].Value = "DELETE";
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
    

        private void btnTambahKamar_Click(object sender, EventArgs e)
        {
            FormTambahDataKamar frmtk = new FormTambahDataKamar();
            frmtk.StartPosition = FormStartPosition.CenterParent;
            frmtk.Text = "Tambah Kamar";
            if (frmtk.ShowDialog() == DialogResult.OK)
            {
                LoadDataKamar();
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7) //jika klik EDIT
            {
                FormTambahDataKamar frmtk = new FormTambahDataKamar();
                frmtk.StartPosition = FormStartPosition.CenterParent;
                frmtk.Text = "Edit Data Kamar";
                frmtk.id_kamar_edit = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                frmtk.txtNoKamar.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                frmtk.numKapasitas.Value = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                frmtk.cmbJenisKamar.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                frmtk.txtHargaPerMalam.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                frmtk.numStok.Value = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());

                if (frmtk.ShowDialog() == DialogResult.OK)
                {
                    LoadDataKamar();
                }
            }

            if (e.ColumnIndex == 8) //Jika klik DELETE
            {
                if (MessageBox.Show("Apakah yakin menghapus data ini?",
                    "Konfirmasi",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    return;
                }

                Koneksi.buka();

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = Koneksi.sqlConn;
                    cmd.CommandText = "DELETE FROM Kamar WHERE id_kamar = @pID";
                    cmd.Parameters.AddWithValue("pID", dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    Koneksi.tutup();
                    LoadDataKamar();//Reload data mobil setelah di update
                }
                catch (Exception err)
                {
                    MessageBox.Show("Terjadi error saat mnghapus data", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtCariNoKamar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadDataKamar();
            }
        }
    }
}
