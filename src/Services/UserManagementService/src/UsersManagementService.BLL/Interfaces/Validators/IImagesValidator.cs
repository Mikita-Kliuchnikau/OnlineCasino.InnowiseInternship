using FluentValidation;
using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.DeleteImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;

namespace UsersManagementService.BLL.Interfaces.Validators;
public interface IImagesValidator 
{
    public IValidator<CreateImageCommand> CreateImageCommandValidator { get; init; }
    public IValidator<DeleteImageCommand> DeleteImageCommandValidator { get; init; }
    public IValidator<UpdateImageCommand> UpdateImageCommandValidator { get; init; }
}