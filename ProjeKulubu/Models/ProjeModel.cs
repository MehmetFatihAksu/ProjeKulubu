using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjeKulubu.Models
{
    public class ProjeModel
    {
        public string Isim { get; set; }
        public string Konum { get; set; }
        public string Tip { get; set; }
        public int Butce { get; set; }
        public int Yil { get; set; }
        public string Aciklama { get; set; }
        public string SeoAlt { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string CalismaSaat { get; set; }
        public int BegeniSayisi { get; set; }
    }
}