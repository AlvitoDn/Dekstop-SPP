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
using Try_SPP_Alvito.Content.EditContent;

namespace Try_SPP_Alvito.Content
{
    public partial class Petugas : Form
    {
        Koneksi konn = new Koneksi();
        SqlDataAdapter sda;
        DataTable dt = new DataTable();
        
        public Petugas()
        {
            InitializeComponent();
        }

        private void Petugas_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dB_SPPDataSetPet.tb_petugas' table. You can move, or remove it, as needed.
            this.tb_petugasTableAdapter1.Fill(this.dB_SPPDataSetPet.tb_petugas);
            // TODO: This line of code loads data into the 'dB_SPPDataSetPetugas.tb_petugas' table. You can move, or remove it, as needed.
            SqlConnection con = konn.GetConn();
            sda = new SqlDataAdapter("Select * from tb_petugas", con);
            con.Open();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = String.Format("nama_petugas like '%{0}%'", textBoxSearch.Text);
            dataGridView1.DataSource = dv.ToTable();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            EditPetugas edp = new EditPetugas();
            edp.Show();
        }
    }
}
