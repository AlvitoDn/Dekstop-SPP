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

namespace Try_SPP_Alvito.Content
{
    public partial class Siswa : Form
    {
        Koneksi conn = new Koneksi();
        SqlDataAdapter sda;
        DataTable dt = new DataTable();
        public Siswa()
        {
            InitializeComponent();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = String.Format("nama like '%{0}%'", textBoxSearch.Text);
            dataGridView1.DataSource = dv.ToTable();
        }

        private void Siswa_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dB_SPPDataSet.tb_siswa' table. You can move, or remove it, as needed.
            this.tb_siswaTableAdapter.Fill(this.dB_SPPDataSet.tb_siswa);
            SqlConnection konek = conn.GetConn();
            sda = new SqlDataAdapter("select * from tb_siswa" , konek);
            konek.Open();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
