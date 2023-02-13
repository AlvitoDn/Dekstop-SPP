using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Try_SPP_Alvito.Class;
using Try_SPP_Alvito.Content;
using static System.Collections.Specialized.BitVector32;

namespace Try_SPP_Alvito
{
    public partial class Login : Form
    {
        Koneksi con = new Koneksi();
        Layout lay = new Layout();
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = con.GetConn();
            conn.Open();
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Mohon Masukan Username Atau Password Dengan Benar!!");
            }
            else
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select username, password, level from tb_petugas where username = '" + textBox1.Text + "' and password = '" + textBox2.Text + "'", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["level"].ToString() == "admin")
                        {
                            this.Hide();
                            Session.UserId = "admin";
                            lay.Show();
                        }
                        else if (dr["level"].ToString() == "petugas")
                        {
                            this.Hide();
                            Session.UserId = "petugas";
                            lay.Show();
                        }
                        else if (dr["level"].ToString() == "siswa")
                        {
                            this.Hide();
                            Session.UserId = "siswa";
                            lay.Show();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Username atau Password Salah!!");
                }
            }
            conn.Close();
        }
    }
}
