using EYS.CoreBusiness;
using EYS.WebApp.ViewModelsValidations;
using System.ComponentModel.DataAnnotations;

namespace EYS.WebApp.ViewModels
{
    public class UretmeViewModel
    {
        [Required]
        public string uretmeSayisi { get; set; } = string.Empty;

        [Required]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage ="Bir ürün seçmelisiniz.")]
        public int UrunId { get; set; }

        [Required]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Adet 1 veya daha fazla olmalıdır.")]
        [Uretim_YeterliEnvanterAdeti]
        public int UretmeAdeti { get; set; }

        public Urun? Urun { get; set; } = null;
    }
}