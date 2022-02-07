using FluentValidation;
using MedicalCabinet.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCabinet.Library.Validator
{
    public class CaseOfIllnessValidator : AbstractValidator<CaseOfIllness>
    {
        public CaseOfIllnessValidator()
        {
            CascadeMode = CascadeMode.Stop;

            //Include(new PersonValidator());

            RuleFor(d => d.Diagnosis)
                .NotEmpty().WithMessage("Заполните поле с диагнозом!");
            RuleFor(d => d.Description)
                .NotEmpty().WithMessage("Заполните поле с описанием случая!");
        }
    }
}
