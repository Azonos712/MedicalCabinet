using FluentValidation;
using MedicalCabinet.Library.Model;
using System;

namespace MedicalCabinet.Library.Validator
{
    public class AppointmentValidator : AbstractValidator<Appointment>
    {
        public AppointmentValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(a => a.CaseOfIllness)
                .NotEmpty().WithMessage("Случай болезни не выбран!");

            RuleFor(a => a.Date)
                .NotEmpty().WithMessage("Заполните поле с датой!")
                .Must(BeAValidDate).WithMessage("Указанная дата не корректна!");

            RuleFor(a => a.Time)
                .NotEmpty().WithMessage("Выберите время встречи!");

            RuleFor(a => a.Description)
                .NotEmpty().WithMessage("Заполните поле с описанием встречи!");

        }

        protected static bool BeAValidDate(DateTime date)
        {
            return DateTime.Now.Date <= date;
        }
    }
}
