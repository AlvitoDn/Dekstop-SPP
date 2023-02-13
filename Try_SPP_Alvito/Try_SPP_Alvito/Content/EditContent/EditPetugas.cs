using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Try_SPP_Alvito.Content.EditContent
{
    public partial class EditPetugas : Form
    {
        Koneksi konn = new Koneksi();
        SqlDataAdapter sda;
        DataTable dt = new DataTable();
        public EditPetugas()
        {
            InitializeComponent();
        }

        private void EditPetugas_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dB_SPPDataSetPetugas.tb_petugas' table. You can move, or remove it, as needed.
            this.tb_petugasTableAdapter.Fill(this.dB_SPPDataSetPetugas.tb_petugas);
            SqlConnection con = konn.GetConn();
            sda = new SqlDataAdapter("Select * from tb_petugas", con);
            con.Open();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = String.Format("nama_petugas like '%{0}%'", textBoxSearch.Text);
            dataGridView1.DataSource = dv.ToTable();
        }
    }
}
