﻿using System.ComponentModel.DataAnnotations;

namespace EYS.CoreBusiness
{
    public class Urun
    {
        public int UrunId { get; set; }

        [Required(ErrorMessage = "Envanter ismi girilmelidir.")]
        [StringLength(130, ErrorMessage = "Envanter ismi en fazla 130 karakter olmalıdır.")]
        public string UrunIsim { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Adet 0 ya da daha büyük olmalıdır.")]
        public int Adet {  get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Fiyat 0 ya da daha büyük olmalıdır.")]
        public double Fiyat { get; set; }
    }
}
