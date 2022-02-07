using FluentValidation;
using MedicalCabinet.Library.Model;

namespace MedicalCabinet.Library.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator(bool isLogIn)
        {
            CascadeMode = CascadeMode.Stop;

            if (!isLogIn)
                RuleFor(u => u.Email)
                    .NotEmpty().WithMessage("Заполните поле с электронной почтой!")
                    .Must(BeWithoutSpaces).WithMessage("Почтовый адрес содержит пробелы!")
                    .EmailAddress().WithMessage("Указанная почта не корректна!");

            RuleFor(u => u.Login)
                .NotEmpty().WithMessage("Заполните поле с логином!")
                .Must(BeWithoutSpaces).WithMessage("Логин должен быть без пробелов!")
                .Length(4, 10).WithMessage("Неправильная длина логина!");
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Заполните поле с паролем!")
                .Must(BeWithoutSpaces).WithMessage("Пароль не должен содержать пробелы!")
                .Length(6, 20).WithMessage("Неправильная длина пароля!");

            if (!isLogIn)
                RuleFor(u => u.PasswordConfirmation)
                    .Equal(u => u.Password).WithMessage("Пароли не совпадают!");

        }

        protected static bool BeWithoutSpaces(string str) => !str.Contains(' ');
    }
}
