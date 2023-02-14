using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Try_SPP_Alvito
{
    public class Koneksi
    {
        public SqlConnection GetConn()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=LAPTOP-IR8LBEM3\\MYSERVER;Initial Catalog=DB_SPP;Integrated Security=True;";
            return conn;
        }
    }
}
