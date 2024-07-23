using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.CoreBusiness.Dogrulamalar
{
    public class Urun_FiyatinEnvanterdenYuksekOldugunuDogrula : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var urun = validationContext.ObjectInstance as Urun;
            if (urun != null)
            {
                if(!FiyatDogrulama(urun))
                {
                    return new ValidationResult(
                        $"Ürünün fiyatı envanterin fiyatından (${toplamEnvanterFiyati(urun)}) daha yüksek olmalıdır!",
                        new List<string>() { validationContext.MemberName});
                }
                
            }
            return ValidationResult.Success;
        }

        private double toplamEnvanterFiyati(Urun urun)
        {
            if (urun == null || urun.UrunEnvanterleri == null) return 0;

            return urun.UrunEnvanterleri.Sum(x => x.Envanter?.Fiyat * x.EnvanterAdeti ?? 0);
        }

        private bool FiyatDogrulama(Urun urun)
        {
            if (urun.UrunEnvanterleri == null || urun.UrunEnvanterleri.Count <= 0) return true;
        
            if (toplamEnvanterFiyati(urun) > urun.Fiyat)
            {
                return false;
            }
            return true;
        }
    }
}
