using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Try_SPP_Alvito.Class;

namespace Try_SPP_Alvito.Content
{
    public partial class Layout : Form
    {
        private int borderSize = 2;
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        public Layout()
        {
            InitializeComponent();
            this.Padding = new Padding(borderSize);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonFullScreen_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState= FormWindowState.Minimized;
        }

        private void adjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    this.Padding = new Padding(0, 8, 8, 0);
                    break;
                case FormWindowState.Normal:
                    if (this.Padding.Top != borderSize)
                        this.Padding = new Padding(borderSize);
                    break;
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALSIZE = 0x0083;
            if (m.Msg == WM_NCCALSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }
            base.WndProc(ref m);
        }
        private void CollapseMenu()
        {
            if (this.panelSide.Width > 199)
            {
                panelSide.Width = 100;
                foreach (Button menuButton in panelSide.Controls.OfType<Button>())
                {
                    menuButton.Text = "";
                    menuButton.ImageAlign = ContentAlignment.MiddleCenter;
                    menuButton.Padding = new Padding(0);
                }
            }
            else
            {
                panelSide.Width = 200;
                foreach (Button menuButton in panelSide.Controls.OfType<Button>())
                {
                    menuButton.Text = menuButton.Tag.ToString();
                    menuButton.ImageAlign = ContentAlignment.MiddleCenter;
                    menuButton.Padding = new Padding(0);
                }
            }
        }

        private void loadForm(object Form)
        {
            if (this.mainPanel.Controls.Count > 0)
                this.mainPanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainPanel.Controls.Add(f);
            this.mainPanel.Tag = f;
            f.Show();
        }

        private void Layout_Load(object sender, EventArgs e)
        {
            loadForm(new Home());
            if (Session.UserId != "")
            {
                if (Session.UserId == "petugas")
                {
                    buttonPetugas.Hide();
                    buttonSiswa.Hide();
                    buttonKelas.Hide();
                    buttonSPP.Hide();
                }
                else if (Session.UserId == "siswa")
                {
                    buttonPetugas.Hide();
                    buttonSiswa.Hide();
                    buttonKelas.Hide();
                    buttonSPP.Hide();
                    buttonTrans.Hide();
                }
            }
            else
            {
                buttonLogout.Hide();
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Session.UserId = "";
            Login log = new Login();
            log.Show();
        }

        private void panelNav_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void buttonCollapse_Click(object sender, EventArgs e)
        {
            CollapseMenu();
        }

        private void Layout_Resize(object sender, EventArgs e)
        {
            adjustForm();
        }

        private void buttonPetugas_Click(object sender, EventArgs e)
        {
            loadForm(new Petugas());
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            loadForm(new Home());
        }

        private void buttonSiswa_Click(object sender, EventArgs e)
        {
            loadForm(new Siswa());
        }
    }
}
