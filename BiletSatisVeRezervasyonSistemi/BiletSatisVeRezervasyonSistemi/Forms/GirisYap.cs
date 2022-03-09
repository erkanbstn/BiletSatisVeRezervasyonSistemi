using BiletSatisVeRezervasyonSistemi.Entity;
using DevExpress.XtraEditors;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiletSatisVeRezervasyonSistemi.Forms
{
    public partial class GirisYap : MaterialForm
    {
        public GirisYap()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.DeepOrange200, TextShade.WHITE);
        }

        private void GirisYap_Load(object sender, EventArgs e)
        {

        }
        FYetkili f;
        Form1 f2;
        KayitOl f3;
        private void materialButton1_Click(object sender, EventArgs e)
        {
            var x = Baglanti.db.TYonetici.FirstOrDefault(b => b.Kullanici == materialTextBox1.Text && b.Sifre == materialTextBox2.Text);
            if (x != null)
            {
                f = new FYetkili();
                f.Show();
                this.Hide();
            }
            else
            {
                var x2 = Baglanti.db.TYolcu.FirstOrDefault(b => b.KullaniciAdi == materialTextBox1.Text && b.Parola == materialTextBox2.Text);
                if (x2 != null)
                {
                    Baglanti.Kisi = x2.KisiBilgi;
                    Baglanti.KisiID = x2.YolcuID;
                    f2 = new Form1();
                    f2.Show();
                    this.Hide();
                }
                else
                {
                    XtraMessageBox.Show("Hatalı Kullanıcı Adı Veya Şifre.", "Uyarı Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            if (f3 == null || f3.IsDisposed)
            {
                f3 = new KayitOl();
                f3.Show();
            }
            else
            {
                f3.Focus();
            }
        }
    }
}
