using FluentValidation;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.DeleteImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;

namespace UsersManagementService.BLL.Models.Image;

public class ImagesValidator(
    IValidator<CreateImageCommand> createImageCommandValidator,
    IValidator<DeleteImageCommand> deleteImageCommandValidator,
    IValidator<UpdateImageCommand> updateImageCommandValidator) : IImagesValidator
{
    public IValidator<CreateImageCommand> CreateImageCommandValidator { get; init; } = createImageCommandValidator;
    public IValidator<DeleteImageCommand> DeleteImageCommandValidator { get; init; } = deleteImageCommandValidator;
    public IValidator<UpdateImageCommand> UpdateImageCommandValidator { get; init; } = updateImageCommandValidator;
}