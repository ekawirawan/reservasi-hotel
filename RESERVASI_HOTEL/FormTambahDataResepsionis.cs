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
    public partial class FormTambahDataResepsionis : Form
    {
        public int id_resepsionis_edit = 0;

        public FormTambahDataResepsionis()
        {
            InitializeComponent();
        }

        private void btnSimpanResepsionis_Click(object sender, EventArgs e)
        {
            try
            {
                Koneksi.buka();
                SqlCommand sqlCommand = new SqlCommand();
                SqlCommand cmd = sqlCommand;
                cmd.Connection = Koneksi.sqlConn;
                SqlDataReader rd;


                cmd.CommandText = " SELECT * FROM Resepsionis WHERE username = @pUsername AND id_resepsionis != @pID ";
                cmd.Parameters.AddWithValue("pUsername", txtUsername.Text);
                cmd.Parameters.AddWithValue("pID", id_resepsionis_edit);
                rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    MessageBox.Show("Username sudah digunakan", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cmd.Dispose();
                rd.Close();
                cmd.Parameters.Clear();

                cmd.Connection = Koneksi.sqlConn;
                if (id_resepsionis_edit == 0) //jika insert data
                {
                    cmd.CommandText = " INSERT INTO Resepsionis (nama, username, password) "
                     + " VALUES(@pNama, @pUsername, @pPassword)";
                }
                else //jika update data
                {
                    cmd.CommandText = "UPDATE Resepsionis SET "
                    + "nama = @pNama, "
                    + "username = @pUsername, "
                    + "password = @pPassword "
                    + "WHERE id_resepsionis = @pID";
                    cmd.Parameters.AddWithValue("pID", id_resepsionis_edit);
                }

                cmd.Parameters.AddWithValue("pNama", txtNama.Text);
                cmd.Parameters.AddWithValue("pUsername", txtUsername.Text);
                cmd.Parameters.AddWithValue("pPassword", txtPassword.Text);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                Koneksi.tutup();
            }
            catch (Exception err)
            {
                MessageBox.Show($"Error saat menyimpan data {err}", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
