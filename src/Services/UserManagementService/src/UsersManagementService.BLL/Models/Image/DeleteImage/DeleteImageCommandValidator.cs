using FluentValidation;
using UsersManagementService.BLL.Extensions;

namespace UsersManagementService.BLL.Models.Image.DeleteImage;

public class DeleteImageCommandValidator : AbstractValidator<DeleteImageCommand>
{
    public DeleteImageCommandValidator()
    {
        RuleFor(u => u.Id)
            .BaseIdRules();
    }
}