using Mapster;
using UsersManagementService.BLL.Models.Image;
using UsersManagementService.BLL.Models.User;

namespace UsersManagementService.Presentation.Models;

public static class DtoMappingProfile
{
    public static void AddDtoMappingConfig(this IServiceCollection services)
    {
        TypeAdapterConfig<UserDto, CreateUserModel>.NewConfig()
            .Map(u => u.Id, src => src.Id)
            .Map(u => u.AuthId, src => src.AuthId)
            .Map(u => u.Username, src => src.Username)
            .Map(u => u.Email, src => src.Email);

        TypeAdapterConfig<UserDto, UpdateUserModel>.NewConfig() 
            .Map(u => u.Id, src => src.Id)
            .Map(u => u.AuthId, src => src.AuthId)
            .Map(u => u.Username, src => src.Username)
            .Map(u => u.Email, src => src.Email)
            .Map(u => u.Balance, src => src.Balance)
            .Map(u => u.VerificationStatus, src => src.VerificationStatus)
            .Map(u => u.IsBanned, src => src.IsBanned)
            .Map(u => u.FirstName, src => src.FirstName)
            .Map(u => u.SecondName, src => src.SecondName)
            .Map(u => u.LastName, src => src.LastName)
            .Map(u => u.BirthDate, src => src.BirthDate)
            .Map(u => u.PassportNumber, src => src.PassportNumber)
            .Map(u => u.IdentificationNumber, src => src.VerificationStatus);

        TypeAdapterConfig<ImageDto, ImageModel>.NewConfig()
            .Map(i => i.Id, src => src.Id)
            .Map(i => i.UserId, src => src.UserId)
            .Map(i => i.ImageUrl, src => src.ImageUrl)
            .Map(i => i.Type, src => src.Type);
    }
}
