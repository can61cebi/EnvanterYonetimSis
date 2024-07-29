using EYS.WebApp.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace EYS.WebApp.ViewModelsValidations
{
    public class Uretim_YeterliEnvanterAdeti : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var UretmeViewModel = validationContext.ObjectInstance as UretmeViewModel;
            if (UretmeViewModel != null)
            {
                if (UretmeViewModel.Urun != null && UretmeViewModel.Urun.UrunEnvanterleri != null)
                {
                    foreach (var ue in UretmeViewModel.Urun.UrunEnvanterleri)
                    {
                        if (ue.Envanter != null &&
                            ue.EnvanterAdeti * UretmeViewModel.UretmeAdeti > ue.Envanter.Adet)
                        {
                            return new ValidationResult($"Envanter ({ue.Envanter.EnvanterIsim}), {UretmeViewModel.UretmeAdeti} adet ürün üretmek için yetersiz.",
                                new[] { validationContext.MemberName });
                        }
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}