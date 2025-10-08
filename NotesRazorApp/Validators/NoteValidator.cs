using FluentValidation;
using NotesRazorApp.Models;

namespace NotesRazorApp.Validators;
public sealed class NoteValidator : AbstractValidator<Note>
{
    public NoteValidator()
    {
        
    }
}
