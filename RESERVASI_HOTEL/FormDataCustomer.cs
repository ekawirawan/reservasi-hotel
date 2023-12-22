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
    public partial class FormDataCustomer : Form
    {
        public FormDataCustomer()
        {
            InitializeComponent();
            LoadDataCustomer();
        }

        public void LoadDataCustomer()
        {
            try
            {
                Koneksi.buka();

                SqlCommand cmd = new SqlCommand();
                SqlDataReader rd;

                dataGridView1.Rows.Clear();
                cmd.CommandText = " SELECT * FROM Customer WHERE nama LIKE @pNama ORDER BY id_customer ";
                cmd.Parameters.AddWithValue("pNama", "%" + txtCariNamaCustomer.Text + "%");
                cmd.Connection = Koneksi.sqlConn;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    int newIndex = dataGridView1.Rows.Add();
                    int indexStart = newIndex + 1;
                    dataGridView1.Rows[newIndex].Cells[0].Value = indexStart.ToString();
                    dataGridView1.Rows[newIndex].Cells[1].Value = rd["id_customer"].ToString();
                    dataGridView1.Rows[newIndex].Cells[2].Value = rd["nama"].ToString();
                    dataGridView1.Rows[newIndex].Cells[3].Value = rd["alamat"].ToString();
                    dataGridView1.Rows[newIndex].Cells[4].Value = rd["no_telp"].ToString();
                    dataGridView1.Rows[newIndex].Cells[5].Value = "EDIT";
                    dataGridView1.Rows[newIndex].Cells[6].Value = "DELETE";
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


        private void button1_Click(object sender, EventArgs e)
        {
            FormAddDataCustomer frm = new FormAddDataCustomer();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Text = "Insert Data Customer Baru";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataCustomer();
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5) //jika klik EDIT
            {
                FormAddDataCustomer frm = new FormAddDataCustomer();
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.Text = "Edit Data Customer";
                frm.id_customer_edit = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                frm.txtNamaCustomer.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.txtAlamatCustomer.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                frm.txtNoTelp.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadDataCustomer();
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

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Koneksi.sqlConn;
                cmd.CommandText = "DELETE FROM Customer WHERE id_customer = @pID";
                cmd.Parameters.AddWithValue("pID", dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                Koneksi.tutup();
                LoadDataCustomer();



            }
        }

        private void txtCariNamaCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadDataCustomer();
            }
        }
    }
}
