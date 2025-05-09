using FluentValidation;
using UsersManagementService.BLL.Extensions.ValidatingExtentions;

namespace UsersManagementService.BLL.Models.Image.DeleteImage;

public class DeleteImageCommandValidator : AbstractValidator<DeleteImageCommand>
{
    public DeleteImageCommandValidator()
    {
        RuleFor(u => u.Id)
            .CommonIdRules();
    }
}