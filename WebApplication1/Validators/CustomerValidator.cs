using WebApplication1.Models;
using FluentValidation;


namespace WebApplication1.Validators;


public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        // ----------- Add -----------
        RuleSet("Add", () =>
        {
            RuleFor(c => c.CompanyName)
                .NotEmpty().WithMessage("Company name is required.")
                .MaximumLength(100);

            RuleFor(c => c.ContactId)
                .NotNull().WithMessage("Contact ID is required.")
                .GreaterThan(0).WithMessage("Please select a valid contact.");

            RuleFor(c => c.Street)
                .NotEmpty().WithMessage("Street is required.")
                .MaximumLength(100);

            RuleFor(c => c.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50);

            RuleFor(c => c.PostalCode)
                .NotEmpty().WithMessage("Postal code is required.")
                .MaximumLength(20);

            RuleFor(c => c.CountryIdentifier)
                .NotNull().WithMessage("Country identifier is required.")
                .NotEqual(-1).WithMessage("Country identifier must be a valid selection.");

            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^[0-9\-\+\(\)\s]+$").WithMessage("Invalid phone number format.");

            RuleFor(c => c.ContactTypeIdentifier)
                .NotNull().WithMessage("Contact type identifier is required.")
                .NotEqual(-1).WithMessage("Contact type must be a valid selection.");

            RuleFor(c => c.CityPostal)
                .MaximumLength(50)
                .When(c => !string.IsNullOrEmpty(c.CityPostal));
        });

        // ----------- Edit / Delete -----------
        RuleSet("Edit", () =>
        {
            Include(new CustomerValidatorBase());
        });

        RuleSet("Delete", () =>
        {
            RuleFor(c => c.CustomerIdentifier)
                .GreaterThan(0)
                .WithMessage("Customer identifier is required for deletion.");
        });
    }

    private class CustomerValidatorBase : AbstractValidator<Customer>
    {
        public CustomerValidatorBase()
        {
            RuleFor(c => c.CustomerIdentifier)
                .GreaterThan(0)
                .WithMessage("Customer identifier must be greater than zero.");

            RuleFor(c => c.CompanyName)
                .NotEmpty().WithMessage("Company name is required.")
                .MaximumLength(100);

            RuleFor(c => c.ContactId)
                .NotNull().WithMessage("Contact ID is required.");

            RuleFor(c => c.Street)
                .NotEmpty().WithMessage("Street is required.")
                .MaximumLength(100);

            RuleFor(c => c.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50);

            RuleFor(c => c.PostalCode)
                .NotEmpty().WithMessage("Postal code is required.")
                .MaximumLength(20);

            RuleFor(c => c.CountryIdentifier)
                .NotNull().WithMessage("Country identifier is required.");

            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^[0-9\-\+\(\)\s]+$").WithMessage("Invalid phone number format.");

            RuleFor(c => c.ContactTypeIdentifier)
                .NotNull().WithMessage("Contact type identifier is required.");

            RuleFor(c => c.CityPostal)
                .MaximumLength(50)
                .When(c => !string.IsNullOrEmpty(c.CityPostal));
        }
    }
}
