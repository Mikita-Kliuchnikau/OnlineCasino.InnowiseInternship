using FluentValidation;
using UsersManagementService.BLL.Models.Image;

namespace UsersManagementService.BLL.Interfaces.Validators;
public interface IImagesValidator 
{
    public IValidator<ImageModel> GetImageModelValidatorOrThrow();
    public IValidator<Guid> GetImageIdValidatorOrThrow();
}