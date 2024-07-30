using System.ComponentModel.DataAnnotations;
using EYS.WebApp.ViewModels;

namespace EYS.WebApp.ViewModelsValidations
{
    public class Satis_YeterliUrunAdeti : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var satmaViewModel = validationContext.ObjectInstance as SatmaViewModel;
            if (satmaViewModel is not null)
            {
                if (satmaViewModel.Urun is not null)
                {
                    if (satmaViewModel.Urun.Adet < satmaViewModel.SatilacakAdet)
                    {
                        return new ValidationResult($"Yeterli ürün adeti yok, yalnızca {satmaViewModel.Urun.Adet} adet ürün bulunuyor.",
                            new[] { validationContext.MemberName });
                    }
                } 
            }

            return ValidationResult.Success;
        }
    }
}
