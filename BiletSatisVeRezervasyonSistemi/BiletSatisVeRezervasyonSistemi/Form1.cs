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

namespace BiletSatisVeRezervasyonSistemi
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.DeepOrange200, TextShade.WHITE);
        }

        Depo<TRezervasyon> y = new Depo<TRezervasyon>();
        Depo<TBilet> b = new Depo<TBilet>();
        Depo<TYolcu> m = new Depo<TYolcu>();
        void Liste()
        {

            int id = Baglanti.KisiID;
            var x = m.TIdGetir(id);
            materialTextBox5.Text = x.KullaniciAdi;
            materialTextBox7.Text = x.Parola;
            materialMaskedTextBox1.Text = x.Telefon;



            materialComboBox1.DataSource = (from y in Baglanti.db.TOtobus
                                            select new
                                            {
                                                y.OtobusID,
                                                y.No
                                            }).ToList();
            materialComboBox2.DataSource = (from y in Baglanti.db.TKoltuk
                                            select new
                                            {
                                                y.KoltukID,
                                                y.KoltukNo,
                                            }).ToList();

            materialComboBox1.DisplayMember = "No";
            materialComboBox1.ValueMember = "OtobusID";

            materialComboBox2.DisplayMember = "KoltukNo";
            materialComboBox2.ValueMember = "KoltukID";

            materialComboBox3.DataSource = (from y in Baglanti.db.TKoltuk
                                            select new
                                            {
                                                y.KoltukID,
                                                y.KoltukNo,
                                            }).ToList();

            materialComboBox4.DataSource = (from y in Baglanti.db.TOtobus
                                            select new
                                            {
                                                y.OtobusID,
                                                y.No
                                            }).ToList();

            materialComboBox4.DisplayMember = "No";
            materialComboBox4.ValueMember = "OtobusID";

            materialComboBox3.DisplayMember = "KoltukNo";
            materialComboBox3.ValueMember = "KoltukID";

            gridControl2.DataSource = (from y in Baglanti.db.TRezervasyon
                                       select new
                                       {
                                           y.RezervasyonID,
                                           y.TKoltuk.KoltukNo,
                                           y.TOtobus.No,
                                           y.Tarih,
                                           y.TYolcu.KisiBilgi
                                       }).Where(b => b.KisiBilgi == Baglanti.Kisi).ToList();

            gridControl1.DataSource = (from y in Baglanti.db.TBilet
                                       select new
                                       {
                                           y.BiletID,
                                           y.No,
                                           y.OlusumTarih,
                                           y.TYolcu.KisiBilgi,
                                       }).Where(b => b.KisiBilgi == Baglanti.Kisi).ToList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Liste();
        }


        private void materialButton1_Click(object sender, EventArgs e)
        {

            var otobusid = Baglanti.db.TOtobus.Where(b => b.No == materialComboBox1.Text).Select(n => n.OtobusID).FirstOrDefault();
            int koltuk = Convert.ToInt32(materialComboBox2.Text);
            var x = Baglanti.db.TBilet.Where(n => n.Otobus == otobusid && n.Koltuk == koltuk).Select(b => b.Koltuk).FirstOrDefault();

            var otobusid2 = Baglanti.db.TOtobus.Where(b => b.No == materialComboBox1.Text).Select(n => n.OtobusID).FirstOrDefault();
            var x2 = Baglanti.db.TSefer.Where(n => n.Otobus == otobusid2).Select(b => b.Guzergah).FirstOrDefault();
            if (x2 != null)
            {
                if (koltuk != x)
                {
                    materialTextBox2.Text = y.KodUret();
                    DialogResult d = XtraMessageBox.Show($"Rezervasyon Bilet Numaranız : {materialTextBox2.Text} Onaylıyor musunuz?", "Sistem Uyarı Mesajı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (d == DialogResult.Yes)
                    {
                        TRezervasyon f = new TRezervasyon();
                        f.Kisi = Convert.ToInt32(materialTextBox1.Text);
                        f.Koltuk = int.Parse(materialComboBox2.SelectedValue.ToString());
                        f.Otobus = int.Parse(materialComboBox1.SelectedValue.ToString());
                        f.Tarih = DateTime.Parse(dateEdit1.EditValue.ToString());
                        y.TEkle(f);

                        TBilet t = new TBilet();
                        t.Kisi = Convert.ToInt32(materialTextBox1.Text);
                        t.Otobus = int.Parse(materialComboBox1.SelectedValue.ToString());
                        t.Koltuk = int.Parse(materialComboBox2.SelectedValue.ToString());
                        t.Durum = "Rezerve Edildi";
                        t.OlusumTarih = DateTime.Parse(dateEdit1.EditValue.ToString());
                        t.No = materialTextBox2.Text;
                        b.TEkle(t);
                        XtraMessageBox.Show("Rezervasyon Başarıyla Oluşturuldu", "Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                    }
                }
                else
                {
                    XtraMessageBox.Show("Seçtiğiniz Koltuk Doludur. Lütfen Farklı Bir Koltuk Seçiniz.", "Uyarı Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                XtraMessageBox.Show("Seçtiğiniz Otobüs Seferi Henüz Oluşturulmamıştır. Lütfen Farklı Bir Otobüs Seçiniz.", "Uyarı Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            var otobusid = Baglanti.db.TOtobus.Where(b => b.No == materialComboBox1.Text).Select(n => n.OtobusID).FirstOrDefault();
            int koltuk = Convert.ToInt32(materialComboBox2.Text);
            var x = Baglanti.db.TBilet.Where(n => n.Otobus == otobusid && n.Koltuk == koltuk).Select(b => b.Koltuk).FirstOrDefault();


            var otobusid2 = Baglanti.db.TOtobus.Where(b => b.No == materialComboBox4.Text).Select(n => n.OtobusID).FirstOrDefault();
            var x2 = Baglanti.db.TSefer.Where(n => n.Otobus == otobusid2).Select(b => b.Guzergah).FirstOrDefault();
            if (x2 != null)
            {
                if (koltuk != x)
                {
                    materialTextBox3.Text = y.KodUret();
                    DialogResult d = XtraMessageBox.Show($"Bilet Numaranız : {materialTextBox3.Text} Onaylıyor musunuz?", "Sistem Uyarı Mesajı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (d == DialogResult.Yes)
                    {
                        TBilet t = new TBilet();
                        t.Kisi = Convert.ToInt32(materialTextBox4.Text);
                        t.Otobus = int.Parse(materialComboBox4.SelectedValue.ToString());
                        t.Koltuk = int.Parse(materialComboBox3.SelectedValue.ToString());
                        t.Durum = "Satıldı";
                        t.OlusumTarih = DateTime.Parse(dateEdit2.EditValue.ToString());
                        t.No = materialTextBox3.Text;
                        b.TEkle(t);
                        XtraMessageBox.Show("Bilet Başarıyla Satın Alındı.", "Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                    }
                }
                else
                {
                    XtraMessageBox.Show("Seçtiğiniz Koltuk Doludur. Lütfen Farklı Bir Koltuk Seçiniz.", "Uyarı Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                XtraMessageBox.Show("Seçtiğiniz Otobüs Seferi Henüz Oluşturulmamıştır. Lütfen Farklı Bir Otobüs Seçiniz.", "Uyarı Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            int id = Baglanti.KisiID;
            var x = m.TIdGetir(id);
            x.KullaniciAdi = materialTextBox5.Text;
            x.Parola = materialTextBox7.Text;
            x.Telefon = materialMaskedTextBox1.Text;
            m.TGuncelle(x);
            XtraMessageBox.Show("Profil Bilgileriniz Başarıyla Güncellendi.", "Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            if (materialTextBox6.Text != "")
            {
                int id = Convert.ToInt32(materialTextBox6.Text);
                var x = y.TIdGetir(id);
                y.TSil(x);
                XtraMessageBox.Show("Rezervasyon Bilgileriniz Başarıyla İptal Edildi.", "Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show("Lütfen Rezervasyon ID Alanını Doldurunuz.", "Uyarı Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            if (materialTextBox8.Text != "")
            {
                int id = Convert.ToInt32(materialTextBox8.Text);
                var x = b.TIdGetir(id);
                x.Durum = "Bilet İptal Edildi";
                b.TGuncelle(x);
                XtraMessageBox.Show("Bilet Bilgileriniz Başarıyla İptal Edildi.", "Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show("Lütfen Bilet ID Alanını Doldurunuz.", "Uyarı Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
