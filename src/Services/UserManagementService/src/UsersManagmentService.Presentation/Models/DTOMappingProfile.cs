using Mapster;
using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;
using UsersManagementService.BLL.Models.User.CreateUser;
using UsersManagementService.BLL.Models.User.UpdateUser;

namespace UsersManagmentService.Presentation.Models;

public static class DTOMappingProfile
{
    public static void AddDTOMappingConfig(this IServiceCollection services)
    {
        TypeAdapterConfig<UserDTO, CreateUserModel>.NewConfig()
            .Map(u => u.Id, src => src.Id)
            .Map(u => u.AuthId, src => src.AuthId)
            .Map(u => u.Username, src => src.Username)
            .Map(u => u.Email, src => src.Email);

        TypeAdapterConfig<UserDTO, UpdateUserModel>.NewConfig() 
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

        TypeAdapterConfig<ImageDTO, CreateImageModel>.NewConfig()
            .Map(i => i.Id, src => src.Id)
            .Map(i => i.UserId, src => src.UserId)
            .Map(i => i.ImageUrl, src => src.ImageUrl)
            .Map(i => i.Type, src => src.Type);

        TypeAdapterConfig<ImageDTO, UpdateImageModel>.NewConfig()
            .Map(i => i.Id, src => src.Id)
            .Map(i => i.UserId, src => src.UserId)
            .Map(i => i.ImageUrl, src => src.ImageUrl)
            .Map(i => i.Type, src => src.Type);
    }
}
