using FluentValidation;
using UsersManagementService.BLL.Extensions;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.Models.Image.UpdateImage;

public class UpdateImageModelValidator : AbstractValidator<UpdateImageModel>
{
    public UpdateImageModelValidator(IImagesRepository imagesRepository)
    {
        RuleFor(u => u.Id)
            .DoesImageExist(imagesRepository)
            .BaseIdRules();
        RuleFor(u => u.UserId)
            .BaseIdRules();
        RuleFor(u => u.ImageUrl)
            .BaseStringRules();
    }
}