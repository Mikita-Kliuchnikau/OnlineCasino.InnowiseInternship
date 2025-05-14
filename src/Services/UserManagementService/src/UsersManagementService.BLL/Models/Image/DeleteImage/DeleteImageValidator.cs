using FluentValidation;
using UsersManagementService.BLL.Extensions;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.Models.Image.DeleteImage;

public class DeleteImageValidator : AbstractValidator<Guid>
{
    public DeleteImageValidator(IImagesRepository imagesRepository)
    {
        RuleFor(u => u)
            .DoesImageExist(imagesRepository)
            .BaseIdRules();
    }
}