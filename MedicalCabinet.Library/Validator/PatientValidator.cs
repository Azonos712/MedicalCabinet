using FluentValidation;
using MedicalCabinet.Library.Model;

namespace MedicalCabinet.Library.Validator
{
    public class PatientValidator : AbstractValidator<Patient>
    {
        public PatientValidator()
        {
            CascadeMode = CascadeMode.Stop;

            Include(new PersonValidator());

            RuleFor(d => d.Insurance)
                .NotEmpty().WithMessage("Заполните поле со страховкой!");
        }
    }
}
