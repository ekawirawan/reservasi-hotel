using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RESERVASI_HOTEL
{
    class Koneksi
    {
        private static string connString;
        public static SqlConnection sqlConn;

        public static void buka()
        {
            connString = @"data source=LAPTOP-13S08GJJ\SQLEXPRESS; initial catalog=HOTEL; integrated security=true;";
            sqlConn = new SqlConnection(connString);
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }
        }

        public static void tutup()
        {
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                sqlConn.Close();
            }
        }
    }
}
