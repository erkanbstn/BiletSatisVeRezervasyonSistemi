using BiletSatisVeRezervasyonSistemi.Entity;
using DevExpress.XtraEditors;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiletSatisVeRezervasyonSistemi.Forms
{
    public partial class FYetkili : MaterialForm
    {
        public FYetkili()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.DeepOrange200, TextShade.WHITE);
        }
        Depo<TYolcu> y = new Depo<TYolcu>();
        Depo<TOtobus> t = new Depo<TOtobus>();
        Depo<TKoltuk> k = new Depo<TKoltuk>();
        Depo<TYonetici> yo = new Depo<TYonetici>();
        Depo<TSefer> s = new Depo<TSefer>();
        void YolcuList()
        {
            materialComboBox2.DataSource = (from y in Baglanti.db.TOtobus
                                            select new
                                            {
                                                y.OtobusID,
                                                y.No
                                            }).ToList();
            materialComboBox5.DataSource = (from y in Baglanti.db.TOtobus
                                            select new
                                            {
                                                y.OtobusID,
                                                y.No
                                            }).ToList();
            materialComboBox2.DisplayMember = "No";
            materialComboBox2.ValueMember = "OtobusID";
            materialComboBox5.DisplayMember = "No";
            materialComboBox5.ValueMember = "OtobusID";
            gridControl2.DataSource = y.Listele("Select YolcuID,KisiBilgi,Telefon,KullaniciAdi from TYolcu");
            gridControl3.DataSource = t.Listele("Select * from TOtobus");
            gridControl1.DataSource = t.Listele("Select * from TKoltuk");
            gridControl4.DataSource = t.Listele("Select * from TYonetici");
            gridControl5.DataSource = t.Listele("Exec BiletListe");
            gridControl6.DataSource = t.Listele("Exec RezervasyonListe");
            gridControl7.DataSource = t.Listele("Exec SeferListesi");
        }

        private void FYetkili_Load(object sender, EventArgs e)
        {
            YolcuList();
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            TYolcu t = new TYolcu();
            t.KisiBilgi = materialTextBox1.Text;
            t.Telefon = materialMaskedTextBox1.Text;
            t.KullaniciAdi = materialTextBox8.Text;
            t.Parola = "123";
            y.TEkle(t);
            XtraMessageBox.Show("Yolcu Başarıyla Eklendi", "Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            YolcuList();
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            if (materialTextBox2.Text != "")
            {
                int id = Convert.ToInt32(materialTextBox2.Text);
                var x = y.TIdGetir(id);
                y.TSil(x);
                XtraMessageBox.Show("Yolcu Başarıyla Silindi", "Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                YolcuList();
            }
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow r = gridView2.GetDataRow(e.FocusedRowHandle);
            if (r != null)
            {
                materialTextBox1.Text = r[1].ToString();
                materialTextBox2.Text = r[0].ToString();
                materialTextBox8.Text = r[3].ToString();
                materialMaskedTextBox1.Text = r[2].ToString();
            }
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            if (materialTextBox2.Text != "")
            {
                int id = Convert.ToInt32(materialTextBox2.Text);
                var x = y.TIdGetir(id);
                x.KisiBilgi = materialTextBox1.Text;
                x.KullaniciAdi = materialTextBox8.Text;
                x.Telefon = materialMaskedTextBox1.Text;
                y.TGuncelle(x);
                XtraMessageBox.Show("Yolcu Başarıyla Güncellendi", "Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                YolcuList();

            }
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            YolcuList();
        }

        private void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow r = gridView3.GetDataRow(e.FocusedRowHandle);
            if (r != null)
            {
                materialTextBox4.Text = r[0].ToString();
                materialTextBox5.Text = r[1].ToString();
            }
        }

        private void materialButton8_Click(object sender, EventArgs e)
        {
            TOtobus d = new TOtobus();
            d.No = y.KodUret();
            t.TEkle(d);
            XtraMessageBox.Show("Otobüs Başarıyla Eklendi", "Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            YolcuList();

        }

        private void materialButton7_Click(object sender, EventArgs e)
        {
            if (materialTextBox4.Text != "")
            {
                int id = Convert.ToInt32(materialTextBox4.Text);
                var x = t.TIdGetir(id);
                t.TSil(x);
                XtraMessageBox.Show("Otobüs Başarıyla Silindi", "Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                YolcuList();

            }
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            YolcuList();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow r = gridView1.GetDataRow(e.FocusedRowHandle);
            if (r != null)
            {
                materialTextBox6.Text = r[1].ToString();
                materialComboBox2.SelectedIndex = int.Parse(r[2].ToString());
                materialTextBox7.Text = r[0].ToString();
            }
        }

        private void materialButton10_Click(object sender, EventArgs e)
        {
            TKoltuk d = new TKoltuk();
            d.KoltukNo = materialTextBox6.Text;
            d.Otobus = int.Parse(materialComboBox2.SelectedValue.ToString());
            k.TEkle(d);
            XtraMessageBox.Show("Koltuk Başarıyla Eklendi", "Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            YolcuList();
        }

        private void materialButton6_Click(object sender, EventArgs e)
        {
            YolcuList();
        }

        private void materialButton11_Click(object sender, EventArgs e)
        {
            if (materialTextBox7.Text != "")
            {
                int id = Convert.ToInt32(materialTextBox7.Text);
                var x = k.TIdGetir(id);
                k.TGuncelle(x);
                XtraMessageBox.Show("Koltuk Başarıyla Güncellendi", "Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                YolcuList();

            }
        }

        private void gridView4_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow r = gridView4.GetDataRow(e.FocusedRowHandle);
            if (r != null)
            {
                materialTextBox3.Text = r[2].ToString();
                materialTextBox9.Text = r[1].ToString();
                materialTextBox10.Text = r[0].ToString();
            }
        }

        private void materialButton13_Click(object sender, EventArgs e)
        {
            TYonetici d = new TYonetici();
            d.Kullanici = materialTextBox9.Text;
            d.Sifre = materialTextBox3.Text;
            yo.TEkle(d);
            XtraMessageBox.Show("Yönetici Başarıyla Eklendi", "Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            YolcuList();

        }

        private void materialButton12_Click(object sender, EventArgs e)
        {
            if (materialTextBox10.Text != "")
            {
                int id = Convert.ToInt32(materialTextBox10.Text);
                var x = yo.TIdGetir(id);
                yo.TSil(x);
                XtraMessageBox.Show("Yönetici Başarıyla Silindi", "Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                YolcuList();

            }
        }

        private void materialButton14_Click(object sender, EventArgs e)
        {
            YolcuList();
        }

        private void materialButton9_Click(object sender, EventArgs e)
        {
            if (materialTextBox10.Text != "")
            {
                int id = Convert.ToInt32(materialTextBox10.Text);
                var x = yo.TIdGetir(id);
                x.Kullanici = materialTextBox9.Text;
                x.Sifre = materialTextBox3.Text;
                yo.TGuncelle(x);
                XtraMessageBox.Show("Yönetici Başarıyla Güncellendi", "Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                YolcuList();
            }
        }
        string AktifEt(bool durum)
        {
            string raddurum;
            if (durum)
            {
                raddurum = "Aktif";
            }
            else
            {
                raddurum = "Pasif";
            }
            return raddurum;
        }
        void AktifKontrol(string durum)
        {
            if (durum == "Aktif")
            {
                materialRadioButton1.Checked = true;
                materialRadioButton2.Checked = false;
            }
            else
            {
                materialRadioButton1.Checked = false;
                materialRadioButton2.Checked = true;
            }
        }

        private void gridView7_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow r = gridView7.GetDataRow(e.FocusedRowHandle);
            if (r != null)
            {
                materialTextBox11.Text = r[0].ToString();
                materialComboBox6.Text = r[1].ToString();
                materialComboBox5.Text = r[2].ToString();
                materialComboBox3.Text = r[3].ToString();
                materialComboBox4.Text = r[4].ToString();
                dateEdit1.EditValue = r[5].ToString();
                AktifKontrol(r[6].ToString());

            }
        }

        private void materialButton17_Click(object sender, EventArgs e)
        {
            TSefer d = new TSefer();
            d.Otobus = int.Parse(materialComboBox5.SelectedValue.ToString());
            d.SeferTarih = DateTime.Parse(dateEdit1.EditValue.ToString());
            d.VarisSaat = materialComboBox4.Text;
            d.SeferSaat = materialComboBox3.Text;
            d.Guzergah = materialComboBox6.Text;
            d.Durum = AktifEt(materialRadioButton1.Checked);
            s.TEkle(d);
            XtraMessageBox.Show("Sefer Başarıyla Eklendi", "Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            YolcuList();
        }

        private void materialButton19_Click(object sender, EventArgs e)
        {
            gridControl7.DataSource = t.Listele("Exec SeferListesiAktif");
        }

        private void materialButton15_Click(object sender, EventArgs e)
        {
            YolcuList();
        }

        private void materialButton18_Click(object sender, EventArgs e)
        {

            if (materialTextBox10.Text != "")
            {
                int id = Convert.ToInt32(materialTextBox11.Text);
                var d = s.TIdGetir(id);
                d.Otobus = int.Parse(materialComboBox5.SelectedValue.ToString());
                d.SeferTarih = DateTime.Parse(dateEdit1.EditValue.ToString());
                d.VarisSaat = materialComboBox4.Text;
                d.SeferSaat = materialComboBox3.Text;
                d.Guzergah = materialComboBox6.Text;
                d.Durum = AktifEt(materialRadioButton1.Checked);
                s.TGuncelle(d);
                XtraMessageBox.Show("Sefer Başarıyla Güncellendi", "Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                YolcuList();
            }
        }

        private void materialButton16_Click(object sender, EventArgs e)
        {
            if (materialTextBox11.Text != "")
            {
                int id = Convert.ToInt32(materialTextBox11.Text);
                var x = s.TIdGetir(id);
                s.TSil(x);
                XtraMessageBox.Show("Sefer Başarıyla Silindi", "Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                YolcuList();

            }
        }

        private void materialButton20_Click(object sender, EventArgs e)
        {

            if (materialTextBox4.Text != "")
            {
                int id = Convert.ToInt32(materialTextBox4.Text);
                var x = t.TIdGetir(id);
                x.No = materialTextBox5.Text;
                t.TGuncelle(x);
                XtraMessageBox.Show("Otobüs Başarıyla Güncellendi", "Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                YolcuList();

            }
        }

        public bool Gonder(string kime, string kimden,string sifre, string konu, string icerik)
        {
            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress(kimden);  
            ePosta.To.Add(kime);  
            ePosta.Subject = konu;
            ePosta.Body = icerik;
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential(kimden, sifre);  
            smtp.Port = 587;  
            smtp.Host = "smtp.gmail.com"; 
            smtp.EnableSsl = true;  
            object userState = ePosta;
            bool kontrol = true;
            try
            {
                smtp.SendAsync(ePosta, (object)ePosta); 
            }
            catch (SmtpException ex)
            {
                kontrol = false;
                MessageBox.Show(ex.Message, "Mail Gönderme Hatası"); 
            }
            return kontrol;
        }
        private void materialButton21_Click(object sender, EventArgs e)
        {
            Gonder(materialButton12.Text, materialTextBox14.Text, materialTextBox13.Text,materialMultiLineTextBox1.Text, materialMultiLineTextBox2.Text);
        }
    }
}
