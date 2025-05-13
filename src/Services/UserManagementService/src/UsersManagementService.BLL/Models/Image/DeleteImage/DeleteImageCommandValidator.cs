using FluentValidation;
using UsersManagementService.BLL.Extensions;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.Models.Image.DeleteImage;

public class DeleteImageCommandValidator : AbstractValidator<DeleteImageCommand>
{
    public DeleteImageCommandValidator(IImagesRepository imagesRepository)
    {
        RuleFor(u => u.Id)
            .DoesImageExist(imagesRepository)
            .BaseIdRules();
    }
}