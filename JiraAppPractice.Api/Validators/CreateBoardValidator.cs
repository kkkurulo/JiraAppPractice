using FluentValidation;
using JiraAppPractice.Services.Dtos;

namespace JiraAppPractice.Api.Validators
{
    public class CreateBoardValidator : AbstractValidator<CreateBoardDto>
    {
        public CreateBoardValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(50);
        }
    }
}
