using FluentValidation;
using JiraAppPractice.Services.Dtos;

namespace JiraAppPractice.Api.Validators;

public class CreateItemValidator : AbstractValidator<CreateJiraItemDto>
{
    public CreateItemValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(50);
        RuleFor(x => x.Description)
            .MaximumLength(200);
    }
}
