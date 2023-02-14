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
        Koneksi Koneksi = new Koneksi();
        SqlDataAdapter sda;
        DataTable dt = new DataTable();
        public EditPetugas()
        {
            InitializeComponent();
        }

        private void EditPetugas_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dB_SPPDataSetPetugas.tb_petugas' table. You can move, or remove it, as needed.
            SqlConnection conn = Koneksi.GetConn();
            sda = new SqlDataAdapter("Select * from tb_petugas", conn);
            conn.Open();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = String.Format("nama like '%{0}%'", textBoxSearch.Text);
            dataGridView1.DataSource = dv.ToTable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = Koneksi.GetConn();
            if (Validation())
            {
                SqlCommand cmd = new SqlCommand("Insert into [tb_petugas] (username,nama,password,level) values ('"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"','"+comboBox1.Text+"')",conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Tambah Data Berhasil","berhasil",MessageBoxButtons.OK);
                refreshData();
                conn.Close();
                
            }
        }

        private bool Validation()
        {
            foreach(TextBox txtbox in panelBottom.Controls.OfType<TextBox>())
            {
                if(txtbox.Text == string.Empty)
                {
                    MessageBox.Show("Tidak Boleh Ada Data Yang Kosong", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
        public void refreshData()
        {
            SqlConnection conn = Koneksi.GetConn();
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from tb_petugas";
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox2.Text == string.Empty)
            {
                MessageBox.Show("Mohon Masukan Nama Yang Ingin Dihapus", "gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlConnection conn = Koneksi.GetConn();
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from tb_petugas where nama = '" + textBox2.Text + "'";
                cmd.ExecuteNonQuery(); 
                refreshData();
                conn.Close();
                MessageBox.Show("Data berhasil dihapus","Berhasil",MessageBoxButtons.OK);
            }
        }
    }
}
