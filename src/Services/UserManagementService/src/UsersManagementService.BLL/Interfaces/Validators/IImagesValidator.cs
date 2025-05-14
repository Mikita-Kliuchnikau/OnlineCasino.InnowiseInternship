using FluentValidation;
using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;

namespace UsersManagementService.BLL.Interfaces.Validators;
public interface IImagesValidator 
{
    public IValidator<CreateImageModel> CreateImageModelValidator { get; init; }
    public IValidator<Guid> DeleteImageValidator { get; init; }
    public IValidator<UpdateImageModel> UpdateImageModelValidator { get; init; }
}