using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BiletSatisVeRezervasyonSistemi.Entity
{
    public class Depo<T> where T : class, new()
    {
        public List<T> HepsiniListele()
        {
            return Baglanti.db.Set<T>().ToList();
        }
        public void TEkle(T p)
        {
            Baglanti.db.Set<T>().Add(p);
            Baglanti.db.SaveChanges();
        }
        public void TSil(T p)
        {
            Baglanti.db.Set<T>().Remove(p);
            Baglanti.db.SaveChanges();
        }
        public T TIdGetir(int id)
        {
            return Baglanti.db.Set<T>().Find(id);
        }
        public void TGuncelle(T p)
        {
            Baglanti.db.SaveChanges();
        }
        public T TAra(Expression<Func<T, bool>> where)
        {
            return Baglanti.db.Set<T>().FirstOrDefault(where);
        }

        public List<T> TIDListele(Expression<Func<T, bool>> filter)
        {
            return Baglanti.db.Set<T>().Where(filter).ToList();
        }

        public string KodUret()
        {
            Random r = new Random();
            string[] takipno = { "A", "B", "C", "D", "E" };
            int k1, k2, k3, s1, s2, s3;
            k1 = r.Next(0, takipno.Length);
            k2 = r.Next(0, takipno.Length);
            k3 = r.Next(0, takipno.Length);
            s1 = r.Next(100, 1000);
            s2 = r.Next(10, 99);
            s3 = r.Next(10, 99);
            string kod = s1.ToString() + takipno[k1] + s2.ToString() + takipno[k2] + s3.ToString() + takipno[k3];
            return kod;
        }

        public DataTable Listele(string sorgu)
        {
            if (Baglanti.bgl.State != ConnectionState.Open)
            {
                Baglanti.bgl.Open();
            }
            SqlDataAdapter ad = new SqlDataAdapter(sorgu,Baglanti.bgl);
            DataTable d = new DataTable();
            ad.Fill(d);
            return d;
        }
    }
}
