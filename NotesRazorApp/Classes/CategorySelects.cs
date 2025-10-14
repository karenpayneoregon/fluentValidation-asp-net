using Microsoft.AspNetCore.Mvc.Rendering;
using NotesRazorApp.Data;
using NotesRazorApp.Models;

namespace NotesRazorApp.Classes;

public class CategorySelects
{
    /// <summary>
    /// Creates a <see cref="SelectList"/> for categories, ordered by their names.
    /// </summary>
    /// <param name="context">The database context used to retrieve category data.</param>
    /// <returns>
    /// A <see cref="SelectList"/> containing categories, where the value field is the category ID
    /// and the text field is the category name.
    /// </returns>
    public static SelectList Create(Context context) =>
        new(
            context.Category.OrderBy(x => x.CategoryName).ToList(),
            nameof(Note.CategoryId),
            nameof(Note.Category.CategoryName));
}
