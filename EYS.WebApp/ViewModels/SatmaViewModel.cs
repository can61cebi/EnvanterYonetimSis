using EYS.CoreBusiness;
using EYS.WebApp.ViewModelsValidations;
using System.ComponentModel.DataAnnotations;

namespace EYS.WebApp.ViewModels
{
    public class SatmaViewModel
    {
        [Required]
        public string SatisNumarasi { get; set; } = string.Empty;

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Bir ürün seçmelisiniz.")]
        public int UrunId { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Satılacak adet 1 veya 1'den büyük olmalıdır.")]
        [Satis_YeterliUrunAdeti]
        public int SatilacakAdet { get; set; }

        [Range (minimum: 0, maximum: int.MaxValue, ErrorMessage = "Satış fiyatı 0 veya 0'dan büyük olmalıdır.")]
        public double AdetFiyati { get; set; }

        public Urun? Urun { get; set; }
    }
}
