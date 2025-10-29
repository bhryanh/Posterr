using FluentValidation;
using Posterr.Application.DTOs;

namespace Posterr.Application.Validators;

public class CreatePostDtoValidator : AbstractValidator<CreatePostDto>
{
    public CreatePostDtoValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Post content cannot be empty")
            .MaximumLength(777).WithMessage("Post content cannot exceed 777 characters");

        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("Author ID is required");
    }
}