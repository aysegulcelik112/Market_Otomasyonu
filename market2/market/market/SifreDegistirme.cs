using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using market.controller;
using market.enumaration;
using market.model;

namespace market
{
    public partial class SifreDegistirme : Form
    {
        public SifreDegistirme()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // İmleci bir sonraki TextBox'a taşı
                this.SelectNextControl((Control)sender, true, true, true, true);
                e.Handled = true;
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // İmleci bir sonraki TextBox'a taşı
                this.SelectNextControl((Control)sender, true, true, true, true);
                e.Handled = true;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // İmleci bir sonraki TextBox'a taşı
                this.SelectNextControl((Control)sender, true, true, true, true);
                e.Handled = true;
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // İmleci bir sonraki TextBox'a taşı
                this.SelectNextControl((Control)sender, true, true, true, true);
                e.Handled = true;
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // İmleci bir sonraki TextBox'a taşı
                this.SelectNextControl((Control)sender, true, true, true, true);
                e.Handled = true;
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // İmleci bir sonraki TextBox'a taşı
                this.SelectNextControl((Control)sender, true, true, true, true);
                e.Handled = true;
            }
        }

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // İmleci bir sonraki TextBox'a taşı
                this.SelectNextControl((Control)sender, true, true, true, true);
                e.Handled = true;
            }

        }

        private void SifreDegistirme_Load(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            List<LoginTable > loginTableList =controller.getLoginTable();
            grpbox_mailAlani.Enabled = false;
            grpbox_sifreDegistir.Enabled = false;
            foreach (LoginTable lt in loginTableList)
            {
                comboBox1.Items.Add(lt.guvenlikSorusu.ToString());
            }
            comboBox1.SelectedIndex = 0;//comboboxta 0.index seçili olaeak gelsin istiyorsan 0a eşitledin
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            LoginStatus result= controller.doAuthentication(txt_kullaniciAdi.Text.Trim().ToLower (), comboBox1.SelectedItem.ToString(), txt_guvenlikcevabi.Text);//trim() methodu kullanıcıın girdiği değerin sağındaki ve solundaki boşlukları siemey yarar. tolower()methodu ise kullanıcın girdiği değeri küçük harfe cevirir.
            if(result ==LoginStatus.basarili)
            {
                grpbox_mailAlani.Enabled = true;
            }
            else if(result ==LoginStatus.basarisiz)
            {
                MessageBox.Show("girdiğiniz bilgileri kontrol ediniz...", "hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                MessageBox.Show("tüm alanalrı doldurunuz lutfennn...", "uyarı", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
        int code;
        private void button3_Click(object sender, EventArgs e)
        {
            Controller controller = new Controller() ;
            string emailAdres = controller.checkEmailAdres(txt_kullaniciAdi.Text);
            if (emailAdres == txt_mailAdres.Text )
            {


                Random rdn = new Random();
                code = rdn.Next(111111, 999999);
                SmtpClient smtp = new SmtpClient();

                // smtp.Credentials = new System.Net.NetworkCredential("omerhattab112@outlook.com", "omer112hattab");//  SmtpClient  mail gönderme protokodür.smtp.live.com --hostumuzudr . bu outlokun kullandığı host hani bideklki local hsot gibi düşünebilriisn. 587 --- ise benim portumdur yani outlook post oalrak da bunun kullanıyormus
                System.Net.NetworkCredential cred = new System.Net.NetworkCredential("omerhattab112@outlook.com", "omer112hattab");

                smtp.Port = 587;
                smtp.Host = "smtp.outlook.com";
                // o parantez içine maili gönderen kişinin posta adresini yazıyorsunuz ve o kişinin şifresini
                SmtpClient server = new SmtpClient("outlook.com");
                smtp.EnableSsl = true;// ggüvenli soket demek. bizler maili göndeririken güvenlik katmanını aktif etmek için kullanırızı
             
                smtp.Credentials = cred;

                MailAddress mailAlan = new MailAddress(txt_mailAdres.Text,txt_kullaniciAdi .Text );
                MailAddress mailGonderen = new MailAddress("omerhattab112@outlook.com");
                MailMessage mesaj = new MailMessage();
                mesaj.To.Add(mailAlan); //maili alacak kişi kim yani onu  belirtiyoruz burda. to='a,'e  demek ya
                mesaj.From = mailGonderen;// maili  gönderen kişinin kim oludğu belirtiliyor .from 'dan, 'den demek ya.
                mesaj.Subject = "Şifre Değiştirme "; //mesajın konusu ne ise buraya onu yazıyoruz yani
                mesaj.Body = "Şifrenizi Değiştirmek İçin Doğrulama Kodununuz : " + code;


                
                smtp.Send(mesaj);//mesaji gönder demek
                smtp.Dispose();
                MessageBox.Show("doğrulama kodu gönderildi", "bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("hesabınıza bağlı mail adresini giriniz!!! ", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == code.ToString())
            {
                grpbox_sifreDegistir.Enabled = true;

            }
            else
            {
                MessageBox.Show("dogrulama kodu yanlıştır", "hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            if(textBox3.Text ==textBox3.Text )
            {
                LoginStatus result =controller.changePassword(txt_kullaniciAdi.Text, textBox3.Text);
                if(result ==LoginStatus.basarili)
                {
                    MessageBox.Show("sifreniz basarılı bir şekilde değiştirildi","bilgilendirme",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("gerekli alanalrı doldurunuz", "uyarı", MessageBoxButtons.OK , MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("iki şifre birbiriyle aynı değğildir", "hata", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }
    }
}
