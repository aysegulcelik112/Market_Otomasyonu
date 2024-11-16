using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using market.enumaration;
using market.controller;
using market.model;

namespace market
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txt_kullaniciAdi_KeyDown(object sender, KeyEventArgs e)
        {
            
                if (e.KeyCode == Keys.Enter)
                {
                    // İmleci bir sonraki TextBox'a taşı
                    this.SelectNextControl((Control)sender, true, true, true, true);
                    e.Handled = true;
                }
            

        }
        private void txt_sifre_KeyDown(object sender, KeyEventArgs e)
        {
           
                if (e.KeyCode == Keys.Enter)
                {
                    // İmleci bir sonraki TextBox'a taşı
                    this.SelectNextControl((Control)sender, true, true, true, true);
                    e.Handled = true;
                }
            
        }

        private void btn_girisYap_Click(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            User result = controller.login(txt_kullaniciAdi.Text, txt_sifre.Text);
            if(result !=null && result .status ==LoginStatus.basarili && result .yetki =="admin")
            {
                //admin sayfasına gönder
                AdminPanel admin = new AdminPanel();
                admin.Show();
                this.Hide();
            }
            else if(result != null &&result.status ==LoginStatus .basarili && result .yetki =="kasiyer")
            {
                KasiyerPanel kasiyer = new KasiyerPanel();
                kasiyer.Show();
                this.Hide();


            }
            else if (result != null && result.status == LoginStatus.basarisiz   )
            {
                MessageBox.Show("Kullanıcı adı veya  şifre hatalı ", "hata  ", MessageBoxButtons.OK, MessageBoxIcon.Error  );


            }
            else
            {
                MessageBox.Show("eksik parametre hatası ", "uyarı ", MessageBoxButtons.OK, MessageBoxIcon.Warning );

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            SifreDegistirme sD = new SifreDegistirme();
            sD.Show();
            this.Hide();
        }

      
    }
}
