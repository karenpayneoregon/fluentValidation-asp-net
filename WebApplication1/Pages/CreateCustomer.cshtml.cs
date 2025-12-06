using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Serilog;
using WebApplication1.Classes;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
    public class CreateCustomerModel(Context context, IValidator<Customer> validator) : PageModel
    {
        public IActionResult OnGet()
        {

            PopulateDropdownViewData(context);

            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = null!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {


            var contact = await context.Contacts.FindAsync(Customer.ContactId);
            var contactType = await context.ContactTypes.FindAsync(Customer.ContactTypeIdentifier);
            var country = await context.Countries.FindAsync(Customer.CountryIdentifier);

            Customer.Contact = contact!;
            Customer.ContactTypeIdentifierNavigation = contactType!;
            Customer.CountryIdentifierNavigation = country!;

            Customer.ModifiedDate = DateTime.UtcNow;

            var result = await validator.ValidateAsync(Customer, options => options.IncludeRuleSets("Add"));

            if (!result.IsValid)
            {
                PopulateDropdownViewData(context);
                result.AddToModelState(ModelState, nameof(Customer));
                return Page();
            }

            //_context.Customers.Add(Customer);
            //await _context.SaveChangesAsync();

            Log.Information("Customer created successfully {@Customer}", Customer);
            
            return RedirectToPage("./Index");
        }

        /// <summary>
        /// Populates dropdown lists in the ViewData with data retrieved from the provided database dbContext.
        /// </summary>
        /// <param name="dbContext">
        /// The database dbContext used to fetch data for populating the dropdown lists.
        /// </param>
        /// <remarks>
        /// This method retrieves data for contacts, contact types, and countries from the database.
        /// It adds placeholder entries to each list to assist user selection.
        /// The populated dropdown lists are stored in the <see cref="PageModel.ViewData"/> dictionary
        /// using the keys "ContactId", "ContactTypeIdentifier", and "CountryIdentifier".
        /// </remarks>
        private void PopulateDropdownViewData(Context dbContext)
        {

            var (contacts, contactTypes, countries) = SelectOptions.GetDefaultSelections(dbContext);

            ViewData["ContactId"] = new SelectList(contacts, nameof(Contact.ContactId), nameof(Contact.FullName));
            ViewData["ContactTypeIdentifier"] = new SelectList(contactTypes, nameof(ContactType.ContactTypeIdentifier), nameof(ContactType.ContactTitle));
            ViewData["CountryIdentifier"] = new SelectList(countries, nameof(Country.CountryIdentifier), nameof(Country.Name));
        }

    }
}
