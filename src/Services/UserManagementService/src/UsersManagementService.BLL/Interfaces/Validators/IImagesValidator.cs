using FluentValidation;
using UsersManagementService.BLL.Models.Image;

namespace UsersManagementService.BLL.Interfaces.Validators;
public interface IImagesValidator 
{
    public IValidator<ImageModel> ImageModelValidator { get; set; }
    public IValidator<Guid> ImageIdValidator { get; set; }
}