using FluentValidation;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.Image;
using UsersManagementService.Common.Exceptions;

namespace UsersManagementService.BLL.Validators.ImagesValidators;

public class ImagesValidator(
    IEnumerable<IValidator<ImageModel>> imageModelValidator,
    IEnumerable<IValidator<Guid>> imageIdValidator) : IImagesValidator
{
    private readonly IEnumerable<IValidator<ImageModel>> _imageModelValidator = imageModelValidator;
    private readonly IEnumerable<IValidator<Guid>> _imageIdValidator = imageIdValidator;

    public IValidator<ImageModel> ImageModelValidator
    {
        get
        {
            var validator = _imageModelValidator.FirstOrDefault(v => v is ImageModelValidator);
            if (validator is null)
            {
                throw new NotFoundException(nameof(validator), null!);
            }
            return validator;
        }
    }
    public IValidator<Guid> ImageIdValidator
    {
        get
        {
            var validator = _imageIdValidator.FirstOrDefault(v => v is ImageIdValidator);
            if (validator is null)
            {
                throw new NotFoundException(nameof(validator), null!);
            }
            return validator;
        }
    }
}