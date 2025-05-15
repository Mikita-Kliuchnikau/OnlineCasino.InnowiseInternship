using FluentValidation;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;

namespace UsersManagementService.BLL.Models.Image;

public class ImagesValidator(
    IValidator<CreateImageModel> createImageModelValidator,
    IValidator<Guid> deleteImageValidator,
    IValidator<UpdateImageModel> updateImageModelValidator) : IImagesValidator
{
    public IValidator<CreateImageModel> CreateImageModelValidator { get; init; } = createImageModelValidator;
    public IValidator<Guid> DeleteImageValidator { get; init; } = deleteImageValidator;
    public IValidator<UpdateImageModel> UpdateImageModelValidator { get; init; } = updateImageModelValidator;
}