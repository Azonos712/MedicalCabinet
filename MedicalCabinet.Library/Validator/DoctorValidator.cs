using FluentValidation;
using MedicalCabinet.Library.Model;

namespace MedicalCabinet.Library.Validator
{
    public class DoctorValidator : AbstractValidator<Doctor>
    {
        public DoctorValidator()
        {
            CascadeMode = CascadeMode.Stop;

            Include(new PersonValidator());

            RuleFor(d => d.Position)
                .NotEmpty().WithMessage("Заполните поле с должностью!");
        }
    }
}
