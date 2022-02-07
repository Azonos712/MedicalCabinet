using FluentValidation;
using MedicalCabinet.Library.Model;

namespace MedicalCabinet.Library.Validator
{
    public class NoteValidator : AbstractValidator<Note>
    {
        public NoteValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(n => n.Title)
                .NotEmpty().WithMessage("Заполните поле с названием заметки!");

            RuleFor(n => n.Description)
                .NotEmpty().WithMessage("Заполните поле с описанием заметки!");
        }
    }
}
