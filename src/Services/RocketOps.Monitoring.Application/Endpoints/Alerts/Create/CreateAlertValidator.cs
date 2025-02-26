using FastEndpoints;
using FluentValidation;

namespace RocketOps.Monitoring.Application.Endpoints.Alerts.Create;

public class CreateAlertValidator : Validator<CreateAlertRequest>
{
    public CreateAlertValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

        RuleFor(x => x.Severity)
            .NotEmpty().WithMessage("Severity is required")
            .Must(BeValidSeverity).WithMessage("Severity must be Low, Medium, High, or Critical");

        RuleFor(x => x.ServiceName)
            .NotEmpty().WithMessage("Service name is required");
    }

    private bool BeValidSeverity(string severity)
    {
        var validSeverities = new[] { "Low", "Medium", "High", "Critical" };
        return validSeverities.Contains(severity, StringComparer.OrdinalIgnoreCase);
    }
}
