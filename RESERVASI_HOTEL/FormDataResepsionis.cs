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
    public partial class FormDataResepsionis : Form
    {
        public FormDataResepsionis()
        {
            InitializeComponent();
            LoadDataResepsionis();
        }

        public void LoadDataResepsionis()
        {
            try
            {
                Koneksi.buka();

                SqlCommand cmd = new SqlCommand();
                SqlDataReader rd;

                dataGridView1.Rows.Clear();
                cmd.CommandText = " SELECT * FROM Resepsionis WHERE username LIKE @pUsername ORDER BY id_resepsionis ";

                cmd.Parameters.AddWithValue("pUsername", "%" + txtSearch.Text + "%");
                cmd.Connection = Koneksi.sqlConn;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    int newIndex = dataGridView1.Rows.Add();
                    int indexStart = newIndex + 1;
                    dataGridView1.Rows[newIndex].Cells[0].Value = indexStart.ToString();
                    dataGridView1.Rows[newIndex].Cells[1].Value = rd["id_resepsionis"].ToString();
                    dataGridView1.Rows[newIndex].Cells[2].Value = rd["nama"].ToString();
                    dataGridView1.Rows[newIndex].Cells[3].Value = rd["username"].ToString();
                    dataGridView1.Rows[newIndex].Cells[4].Value = rd["password"].ToString();
                    dataGridView1.Rows[newIndex].Cells[5].Value = "EDIT";
                    dataGridView1.Rows[newIndex].Cells[6].Value = "DELETE";
                }

                cmd.Dispose();
                rd.Close();
                Koneksi.tutup();
            }
            catch (Exception err)
            {
                MessageBox.Show($"Terjadi kesalahan pada saat memuat data {err}", "Error",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadDataResepsionis();
            }

        }

        private void btnTambahResepsionis_Click(object sender, EventArgs e)
        {
            FormTambahDataResepsionis ftdr = new FormTambahDataResepsionis();
            ftdr.StartPosition = FormStartPosition.CenterParent;
            ftdr.Text = "Tambah Data Resepsionis";
            if (ftdr.ShowDialog() == DialogResult.OK)
            {
                LoadDataResepsionis();
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                FormTambahDataResepsionis ftds = new FormTambahDataResepsionis();
                ftds.StartPosition = FormStartPosition.CenterParent;
                ftds.Text = "Edit Data Mobil";
                ftds.id_resepsionis_edit = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                ftds.txtNama.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                ftds.txtUsername.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                ftds.txtPassword.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                if (ftds.ShowDialog() == DialogResult.OK)
                {
                    LoadDataResepsionis();
                }

            }
            if (e.ColumnIndex == 6) //Jika klik DELETE
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
                    cmd.CommandText = "DELETE FROM Resepsionis WHERE id_resepsionis = @pID";
                    cmd.Parameters.AddWithValue("pID", dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    Koneksi.tutup();
                    LoadDataResepsionis();//Reload data mobil setelah di update
                }
                catch (Exception err)
                {
                    MessageBox.Show("Terjadi error saat menghapus data", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }
    }
}
