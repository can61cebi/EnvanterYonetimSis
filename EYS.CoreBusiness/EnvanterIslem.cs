using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.CoreBusiness
{
    public class EnvanterIslem
    {
        public int EnvanterIslemId { get; set; }

        public string almaSayisi { get; set; } = string.Empty;

        [Required]
        public int EnvanterId { get; set; }

        [Required]
        public int OncekiAdet { get; set; }

        [Required]
        public EnvanterIslemTipi AksiyonTipi { get; set; }

        [Required]
        public int SonrakiAdet { get; set; }

        public double AdetFiyati { get; set; }

        [Required]
        public DateTime IslemZamani { get; set; }

        [Required]
        public string AlanKisi { get; set; } = string.Empty;

        public Envanter? Envanter { get; set; }
    }
}
