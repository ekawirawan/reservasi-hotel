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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Koneksi.buka();
                SqlCommand sqlCommand = new SqlCommand();
                SqlCommand cmd = sqlCommand;
                cmd.Connection = Koneksi.sqlConn;

                SqlDataReader rd;


                cmd.CommandText = " SELECT * FROM Resepsionis WHERE username=@pUsername ";
                cmd.Parameters.AddWithValue("pUsername", txtUsername.Text);
                rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    MessageBox.Show("Login Berhasil", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    FormHome fh = new FormHome();
                    fh.WindowState = FormWindowState.Maximized;
                    fh.Show();

                    this.Close();
                } else
                {
                    MessageBox.Show("Username atau Password Salah", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch (Exception err)
            {
                MessageBox.Show("Terjadi kesalahan", "Error",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
