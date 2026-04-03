using FluentValidation;
using OrderService.Commands;

namespace OrderService.Validators;

public sealed class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
{
    public CheckoutOrderCommandValidator()
    {
        // --- User & Order Info ---
        RuleFor(o => o.UserName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(70).WithMessage("{PropertyName} must not exceed 70 characters.");

        RuleFor(o => o.TotalPrice)
            .NotNull().WithMessage("{PropertyName} is required.")
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} should not be negative.");

        // --- Personal Info ---
        RuleFor(o => o.FirstName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(o => o.LastName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(o => o.EmailAddress)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .EmailAddress().WithMessage("{PropertyName} is not a valid email address format.")
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        // --- Address Info ---
        RuleFor(o => o.AddressLine)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");

        RuleFor(o => o.Country)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(o => o.State)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(o => o.ZipCode)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(10).WithMessage("{PropertyName} must not exceed 10 characters.")
            .Matches(@"^[a-zA-Z0-9\- ]*$").WithMessage("{PropertyName} contains invalid characters.");

        // --- Payment Method ---
        // Assuming 0 is an invalid/unselected enum value
        RuleFor(o => o.PaymentMethod)
            .NotNull().WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("{PropertyName} must be a valid payment type.");

        // --- Credit Card Info ---
        RuleFor(o => o.CardName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(o => o.CardNumber)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            // Ensures only digits are passed, and length is between 13 and 19 characters (Visa, Mastercard, Amex, etc.)
            .Matches(@"^[0-9]{13,19}$").WithMessage("{PropertyName} is not a valid card number.");

        RuleFor(o => o.Expiration)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            // Standard MM/YY or MM/YYYY format
            .Matches(@"^(0[1-9]|1[0-2])\/([0-9]{2}|[0-9]{4})$").WithMessage("{PropertyName} must be in MM/YY or MM/YYYY format.")
            // Custom logic to ensure the card isn't expired
            .Must(BeAValidFutureDate).WithMessage("{PropertyName} date must be in the future.");

        RuleFor(o => o.Cvv)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            // Standard CVV is 3 digits, Amex is 4 digits
            .Matches(@"^[0-9]{3,4}$").WithMessage("{PropertyName} must be 3 or 4 digits.");
    }

    // Custom validator to check if the expiration date is actually in the future
    private bool BeAValidFutureDate(string? expiration)
    {
        if (string.IsNullOrWhiteSpace(expiration)) return false;

        var parts = expiration.Split('/');
        if (parts.Length != 2) return false;
        if (!int.TryParse(parts[0], out int month) || month < 1 || month > 12) return false;
        if (!int.TryParse(parts[1], out int year)) return false;

        // Handle 2-digit year (e.g., "25" becomes 2025)
        if (year < 100)
            year += 2000;

        // Card is valid through the end of the expiration month
        var expirationDate = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);

        return expirationDate >= DateTime.Today;
    }
}
