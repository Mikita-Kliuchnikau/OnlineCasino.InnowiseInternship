using FluentValidation;
using UsersManagementService.BLL.Extensions;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.Validators.ImagesValidators;

public class ImageIdValidator : AbstractValidator<Guid>
{
    public ImageIdValidator(IImagesRepository imagesRepository)
    {
        RuleFor(id => id)
            .BaseIdRules()
            .DoesImageExist(imagesRepository);
    }
}