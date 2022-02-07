using FluentValidation;
using FluentValidation.Results;
using MedicalCabinet.UI.Helpers;
using MedicalCabinet.UI.View;
using System.Windows.Controls;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace MedicalCabinet.UI
{
    public static class ModelValidator
    {
        public static bool Validate<T>(AbstractValidator<T> validator, T validatedObject, Grid backgroundGrid)
        {
            ValidationResult results = validator.Validate(validatedObject);

            foreach (ValidationFailure failure in results.Errors)
                WindowsMaker.ShowPopUp(new CustomMsgBox("Ошибка", $"Произошла ошибка.\n{failure.ErrorMessage}"), backgroundGrid);

            return results.IsValid;
        }
    }
}
