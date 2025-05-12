using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UsersManagementService.BLL.Models.User.GetPagedUsers;
using UsersManagementService.BLL.Models.User.GetUser;
using UsersManagementService.BLL.Models.User.UpdateUser;
using UsersManagementService.DAL.Entites.Core;
using UsersManagementService.DAL.Entites.DTO;

namespace UsersManagementService.BLL.Models.User;

public static class UsersMappingConfig
{
    public static void AddUsersMappingConfig(this IServiceCollection services)
    {
        TypeAdapterConfig<UserEntity, UserViewModel>.NewConfig()
            .Map(vm => vm.Id, src => src.Id)
            .Map(vm => vm.Username, src => src.Username)
            .Map(vm => vm.Email, src => src.Email)
            .Map(vm => vm.Balance, src => src.Balance)
            .Map(vm => vm.VerificationStatus, src => src.VerificationStatus)
            .Map(vm => vm.IsBanned, src => src.IsBanned)
            .Map(vm => vm.IsDeleted, src => src.IsDeleted)
            .Map(vm => vm.FirstName, src => src.FirstName)
            .Map(vm => vm.SecondName, src => src.SecondName)
            .Map(vm => vm.LastName, src => src.LastName)
            .Map(vm => vm.BirthDate, src => src.BirthDate)
            .Map(vm => vm.PassportNumber, src => src.PassportNumber)
            .Map(vm => vm.IdentificationNumber, src => src.VerificationStatus)
            .Map(vm => vm.Images, src => src.Images != null
                ? src.Images.Select(i => i.Adapt<ImageViewModel>()).ToList()
                : null);

        TypeAdapterConfig<UpdateUserCommand, UserEntity>.NewConfig()
            .Map(u => u.Id, src => src.Id)
            .Map(u => u.AuthId, src => src.AuthId)
            .Map(u => u.Username, src => src.Username)
            .Map(u => u.Email, src => src.Email)
            .Map(u => u.Balance, src => src.Balance)
            .Map(u => u.VerificationStatus, src => src.VerificationStatus)
            .Map(u => u.IsBanned, src => src.IsBanned)
            .Map(u => u.IsDeleted, src => src.IsDeleted)
            .Map(u => u.FirstName, src => src.FirstName)
            .Map(u => u.SecondName, src => src.SecondName)
            .Map(u => u.LastName, src => src.LastName)
            .Map(u => u.BirthDate, src => src.BirthDate)
            .Map(u => u.PassportNumber, src => src.PassportNumber)
            .Map(u => u.IdentificationNumber, src => src.VerificationStatus);

        TypeAdapterConfig<UpdateUserCommand, UserEntity>.NewConfig()
            .Map(u => u.Id, src => src.Id)
            .Map(u => u.AuthId, src => src.AuthId)
            .Map(u => u.Username, src => src.Username)
            .Map(u => u.Email, src => src.Email)
            .Map(u => u.Balance, src => src.Balance)
            .Map(u => u.VerificationStatus, src => src.VerificationStatus)
            .Map(u => u.IsBanned, src => src.IsBanned)
            .Map(u => u.IsDeleted, src => src.IsDeleted)
            .Map(u => u.FirstName, src => src.FirstName)
            .Map(u => u.SecondName, src => src.SecondName)
            .Map(u => u.LastName, src => src.LastName)
            .Map(u => u.BirthDate, src => src.BirthDate)
            .Map(u => u.PassportNumber, src => src.PassportNumber)
            .Map(u => u.IdentificationNumber, src => src.VerificationStatus)
            .Map(u => u.Images, src => src.Images != null
                ? src.Images.Select(i => i.Adapt<ImageEntity>()).ToList()
                : null);

        TypeAdapterConfig<GetPagedUsersQuery, PagedUsersFilter>.NewConfig()
            .Map(f => f.PageNumber, src => src.PageNumber)
            .Map(f => f.PageSize, src => src.PageSize);

        TypeAdapterConfig<PagedUsersProjection, PagedUsersViewModel>.NewConfig()
            .Map(vm => vm.PageNumber, src => src.PageNumber)
            .Map(f => f.TotalCount, src => src.TotalCount)
            .Map(f => f.UserViewModels, src => src.Users != null
                ? src.Users.Select(i => i.Adapt<UserViewModel>()).ToList()
                : null);

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}
