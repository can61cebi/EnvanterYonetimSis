using System.ComponentModel.DataAnnotations;

namespace EYS.WebApp.ViewModels
{
    public class SatinAlmaViewModel
    {
        [Required]
        public string almaSayisi { get; set; } = string.Empty;

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage ="Bir envanter seçmelisiniz.")]
        public int EnvanterId { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Adet 1 veya daha fazla olmalıdır.")]
        public int SatinAlmaAdeti { get; set; }
        public double EnvaterFiyati { get; set; }

    }
}