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
                string username = txtUsername.Text;
                string password = txtPassword.Text;

                Boolean isRegistered, isValidPassword;
                isRegistered = checkUsername(username);
                if (isRegistered)
                {
                    isValidPassword = checkPassword(password);

                    if (isValidPassword)
                    {
                        FormContainer fc = new FormContainer();
                        fc.Show();
                        this.Hide();

                        MessageBox.Show("Login Berhasil", "Success",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                    } else
                    {
                        MessageBox.Show("Username atau Password Salah", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                
            }
            catch (Exception err)
            {
                MessageBox.Show("Terjadi kesalahan" + err, "Error",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Boolean checkUsername(string username)
        {
            Koneksi.buka();
            SqlCommand sqlCommand = new SqlCommand();
            SqlCommand cmd = sqlCommand;
            cmd.Connection = Koneksi.sqlConn;

            SqlDataReader rd;

            cmd.CommandText = " SELECT * FROM Resepsionis WHERE username=@pUsername ";
            cmd.Parameters.AddWithValue("pUsername", username);
            rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                Koneksi.tutup();
                cmd.Dispose();
                return true;
            } else
            {
                Koneksi.tutup();
                cmd.Dispose();
                return false;
            }
        }

        public Boolean checkPassword(string password)
        {
            Koneksi.buka();
            SqlCommand sqlCommand = new SqlCommand();
            SqlCommand cmd = sqlCommand;
            cmd.Connection = Koneksi.sqlConn;

            SqlDataReader rd;

            cmd.CommandText = " SELECT * FROM Resepsionis WHERE password=@pPassword ";
            cmd.Parameters.AddWithValue("pPassword", password);
            rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                Koneksi.tutup();
                cmd.Dispose();
                return true;
            }
            else
            {
                Koneksi.tutup();
                cmd.Dispose();
                return false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
