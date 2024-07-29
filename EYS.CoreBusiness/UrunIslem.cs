using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.CoreBusiness
{
    public class UrunIslem
    {
        public int UrunIslemId { get; set; }

        public string satmaSayisi { get; set; } = string.Empty;

        public string UretimNumarasi { get; set; } = string.Empty;

        [Required]
        public int UrunId { get; set; }

        [Required]
        public int OncekiAdet { get; set; }

        [Required]
        public UrunIslemTipi AksiyonTipi { get; set; }

        [Required]
        public int SonrakiAdet { get; set; }

        public double? AdetFiyati { get; set; }

        [Required]
        public DateTime IslemZamani { get; set; }

        [Required]
        public string AlanKisi { get; set; } = string.Empty;

        public Urun? urun { get; set; }
    }
}
