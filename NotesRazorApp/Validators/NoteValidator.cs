using FluentValidation;
using NotesRazorApp.Models;

namespace NotesRazorApp.Validators;
/// <summary>
/// Provides validation rules for the <see cref="Note"/> model.
/// </summary>
public class NoteValidator : AbstractValidator<Note>
{
    public NoteValidator()
    {
        
        RuleFor(x => x.BodyText)
            .NotEmpty().WithMessage("Note is required.")
            .MinimumLength(6).WithMessage("Note must be at least 6 characters long.");

        RuleFor(x => x.DueDate)
            .Must(dueDate =>
            {
                if (!dueDate.HasValue)
                    return true; // skip if null (handled elsewhere)

                var today = DateTime.Today;
                var minDate = today.AddDays(-30);
                var maxDate = today.AddMonths(12);
                return dueDate.Value >= minDate && dueDate.Value <= maxDate;
            })
            .WithMessage("Due date must be within the past 30 days or up to 12 months in the future.");

        RuleFor(x => x.Completed)
            .NotNull().WithMessage("Done field is required.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).When(x => x.CategoryId.HasValue)
            .WithMessage("Category ID must be greater than zero if specified.");
    }
}

