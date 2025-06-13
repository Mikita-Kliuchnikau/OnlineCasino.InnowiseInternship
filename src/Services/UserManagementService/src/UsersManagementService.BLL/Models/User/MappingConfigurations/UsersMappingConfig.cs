using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UsersManagementService.BLL.Models.Image;
using UsersManagementService.DAL.Entites.Core;
using UsersManagementService.DAL.Entites.Dto;

namespace UsersManagementService.BLL.Models.User.MappingConfigurations;

public static class UsersMappingConfig
{
    public static void AddUsersMappingConfig(this IServiceCollection services)
    {
        TypeAdapterConfig<UserEntity, UserViewModel>.NewConfig()
            .Map(vm => vm.Id, src => src.Id)
            .Map(vm => vm.AuthId, src => src.AuthId)
            .Map(vm => vm.Username, src => src.Username)
            .Map(vm => vm.Email, src => src.Email)
            .Map(vm => vm.Balance, src => src.Balance)
            .Map(vm => vm.VerificationStatus, src => src.VerificationStatus)
            .Map(vm => vm.IsBanned, src => src.IsBanned)
            .Map(vm => vm.CreatedAt, src => src.CreatedAt)
            .Map(vm => vm.FirstName, src => src.FirstName)
            .Map(vm => vm.SecondName, src => src.SecondName)
            .Map(vm => vm.LastName, src => src.LastName)
            .Map(vm => vm.BirthDate, src => src.BirthDate)
            .Map(vm => vm.PassportNumber, src => src.PassportNumber)
            .Map(vm => vm.IdentificationNumber, src => src.IdentificationNumber)
            .Map(vm => vm.Images, src => src.Images.Select(i => i.Adapt<ImageViewModel>()));

        TypeAdapterConfig<CreateUserModel, UserEntity>.NewConfig()
            .Map(u => u.AuthId, src => src.AuthId)
            .Map(u => u.Username, src => src.Username)
            .Map(u => u.Email, src => src.Email);

        TypeAdapterConfig<UpdateUserModel, UserEntity>.NewConfig()
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
            .Map(u => u.IdentificationNumber, src => src.IdentificationNumber);

        TypeAdapterConfig<GetPagedUsersQuery, PagedUsersFilter>.NewConfig()
            .Map(f => f.PageNumber, src => src.PageNumber)
            .Map(f => f.PageSize, src => src.PageSize);

        TypeAdapterConfig<PagedUsersProjection, PagedUsersViewModel>.NewConfig()
            .Map(vm => vm.PageNumber, src => src.PageNumber)
            .Map(vm => vm.TotalCount, src => src.TotalCount)
            .Map(vm => vm.UserViewModels, src => src.Users != null
                ? src.Users.Select(i => i.Adapt<UserViewModel>()).ToList()
                : null);

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}
