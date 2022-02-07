using FluentValidation;
using MedicalCabinet.Library.Model;
using System;

namespace MedicalCabinet.Library.Validator
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(d => d.LastName)
                .NotEmpty().WithMessage("Заполните поле с фамилией!")
                .Must(BeWithoutSpaces).WithMessage("Фамилия содержит пробелы!");

            RuleFor(d => d.FirstName)
                .NotEmpty().WithMessage("Заполните поле с именем!")
                .Must(BeWithoutSpaces).WithMessage("Имя содержит пробелы!");

            RuleFor(d => d.MiddleName)
                .NotEmpty().WithMessage("Заполните поле с отчеством!")
                .Must(BeWithoutSpaces).WithMessage("Отчество содержит пробелы!");

            RuleFor(d => d.BirthDay)
                .NotEmpty().WithMessage("Заполните поле с датой!")
                .Must(BeAValidDate).WithMessage("Указанная дата не корректна!");
        }

        protected static bool BeWithoutSpaces(string str) => !str.Contains(' ');

        protected static bool BeAValidDate(DateTime date)
        {
            return !(DateTime.Now.AddYears(-100).Date >= date || date >= DateTime.Now.AddYears(-18).Date);
        }
    }
}
