using FluentValidation;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.Image;
using UsersManagementService.Common.Exceptions;

namespace UsersManagementService.BLL.Validators.ImagesValidators;

public class ImagesValidator(
    IValidator<ImageModel> imageModelValidator,
    IValidator<Guid> imageIdValidator) : IImagesValidator
{
    private readonly List<IValidator<ImageModel>> _imageModelValidator = [];
    private readonly List<IValidator<Guid>> _imageIdValidator = [];

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

        set => _imageModelValidator.Add(imageModelValidator);
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
        set => _imageIdValidator.Add(imageIdValidator);
    }
}