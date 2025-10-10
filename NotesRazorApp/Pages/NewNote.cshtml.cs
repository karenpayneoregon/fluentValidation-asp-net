using Azure.Core;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NotesRazorApp.Classes;
using NotesRazorApp.Data;
using NotesRazorApp.Models;

namespace NotesRazorApp.Pages;

public class NewNoteModel(Context context, IValidator<Note> validator) : PageModel
{
    public IActionResult OnGet()
    {

        Note = new Note()
        {
            DueDate = DateTime.Now
        };
            
        SetupCategories();

        return Page();

    }

    [BindProperty]
    public Note Note { get; set; } = new();

    public async Task<IActionResult> OnPostAsync()
    {

        ValidationResult result = await validator.ValidateAsync(Note);
       
        if (!result.IsValid)
        {
            SetupCategories();
            
            result.AddToModelState(ModelState, nameof(Note));
            
            return Page();
            
        }

        context.Note.Attach(Note);

        await context.SaveChangesAsync();

        return RedirectToPage("ViewNotes");
    }

    private void SetupCategories()
    {
        ViewData[nameof(Category.CategoryName)] = new SelectList(
            context.Category.OrderBy(x => x.CategoryName).ToList(),
            nameof(Note.CategoryId),
            nameof(Note.Category.CategoryName));
    }
}