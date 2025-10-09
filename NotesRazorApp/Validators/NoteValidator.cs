using FluentValidation;
using NotesRazorApp.Models;

namespace NotesRazorApp.Validators;
public class NoteValidator : AbstractValidator<Note>
{
    public NoteValidator()
    {
        // BodyText: Required, min length 6
        RuleFor(x => x.BodyText)
            .NotEmpty().WithMessage("Note is required.")
            .MinimumLength(6).WithMessage("Note must be at least 6 characters long.");

        RuleFor(x => x.DueDate)
            .GreaterThanOrEqualTo(DateTime.Now)
            .When(x => x.DueDate.HasValue)
            .WithMessage($"Due date must be today or later than {DateTime.Now:d}.");

        // Completed: Required
        RuleFor(x => x.Completed)
            .NotNull().WithMessage("Done field is required.");

        // CategoryId: optional, but if present, must be > 0
        RuleFor(x => x.CategoryId)
            .GreaterThan(0).When(x => x.CategoryId.HasValue)
            .WithMessage("Category ID must be greater than zero if specified.");
    }
}

