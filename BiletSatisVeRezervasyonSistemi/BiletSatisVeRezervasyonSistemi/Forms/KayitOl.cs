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
    public partial class KayitOl : MaterialForm
    {
        public KayitOl()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.DeepOrange200, TextShade.WHITE);
        }

        private void KayitOl_Load(object sender, EventArgs e)
        {

        }

        Depo<TYolcu> y = new Depo<TYolcu>();
        private void materialButton1_Click(object sender, EventArgs e)
        {
            TYolcu t = new TYolcu();
            t.KisiBilgi = materialTextBox4.Text;
            t.KullaniciAdi= materialTextBox1.Text;
            t.Parola= materialTextBox2.Text;
            t.Telefon = maskedTextBox1.Text;
            y.TEkle(t);
            XtraMessageBox.Show("Sisteme Başarıyla Kayıt Olundu.", "Uyarı Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
