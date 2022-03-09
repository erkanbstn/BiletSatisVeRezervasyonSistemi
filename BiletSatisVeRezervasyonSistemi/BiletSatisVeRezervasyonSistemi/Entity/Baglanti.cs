using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiletSatisVeRezervasyonSistemi.Entity
{
    public static class Baglanti
    {
        public static BiletRezervasyonDBEntities db = new BiletRezervasyonDBEntities();
        public static SqlConnection bgl = new SqlConnection("Data Source=GEOPC\\SQLEXPRESS;Initial Catalog=BiletRezervasyonDB;Integrated Security=True;MultipleActiveResultSets=True;");
        public static string Kisi { get; set; }
        public static int KisiID { get; set; }
    }
}
