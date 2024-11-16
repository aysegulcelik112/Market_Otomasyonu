using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace market
{
    public partial class meyveSebzePanel : Form
    {
        public meyveSebzePanel()
        {
            
            InitializeComponent();
            txt_islem.Text = "0";
        }

        private void grp_hesapMakinesi_Enter(object sender, EventArgs e)
        {

        }

        private void secilen_tuslar(object sender, EventArgs e)
        {
            if (txt_islem.Text == "0")
            {
                txt_islem.Text = "";
            }
            txt_islem.Text += (Button)sender;
        }

        private void combo_kameraAc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
