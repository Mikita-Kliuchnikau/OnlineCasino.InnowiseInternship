using Mapster;
using UsersManagementService.BLL.Models.Image;
using UsersManagementService.BLL.Models.User;
using UsersManagementService.Common.Exceptions;

namespace UsersManagementService.Presentation.Models;

public static class DtoMappingProfile
{
    public static void AddDtoMappingConfig(this IServiceCollection services)
    {
        TypeAdapterConfig<CreateUserDto, CreateUserModel>.NewConfig()
            .Map(u => u.AuthId, src => src.AuthId)
            .Map(u => u.Username, src => src.Username)
            .Map(u => u.Email, src => src.Email);

        TypeAdapterConfig<UpdateUserDto, UpdateUserModel>.NewConfig()
            .Map(u => u.AuthId, src => src.AuthId)
            .Map(u => u.Username, src => src.Username)
            .Map(u => u.Email, src => src.Email)
            .Map(u => u.Balance, src => src.Balance)
            .Map(u => u.VerificationStatus, src => src.VerificationStatus)
            .Map(u => u.FirstName, src => src.FirstName)
            .Map(u => u.SecondName, src => src.SecondName)
            .Map(u => u.LastName, src => src.LastName)
            .Map(u => u.BirthDate, src => src.BirthDate)
            .Map(u => u.PassportNumber, src => src.PassportNumber)
            .Map(u => u.IdentificationNumber, src => src.IdentificationNumber);

        TypeAdapterConfig<ImageDto, ImageModel>.NewConfig()
            .ConstructUsing(src => new ImageModel(
                src.UserId,
                src.Type,
                ConvertToMemoryStream(src.File),
                src.File.ContentType
            ));
    }

    private static MemoryStream ConvertToMemoryStream(IFormFile file)
    {
        if (file is null || file.Length == 0)
        {
            throw new NotFoundException("File doesn't exists", null!);
        }
        var memoryStream = new MemoryStream();
        using (var input = file.OpenReadStream())
        {
            input.CopyTo(memoryStream);
        }
        memoryStream.Position = 0;
        return memoryStream;
    }
}
