using UsersManagementService.BLL.Models.User.Queries.GetPagedUsers;
using UsersManagementService.BLL.Models.User.Queries.GetUser;
using UsersManagementService.DAL.Entites.Core;
using UsersManagementService.DAL.Entites.DTO;

namespace UsersManagementService.BLL.Extensions.MappingExtensions;

public static class ToUserViewModelMappingProfile
{
    public static PagedUsersViewModel ToPagedUsersViewModel(this PagedUsersProjection pagedUsersProjection)
    {
        return new PagedUsersViewModel
        (
            PageNumber: pagedUsersProjection.PageNumber,
            TotalCount: pagedUsersProjection.TotalCount,
            UserViewModels: [.. pagedUsersProjection.Users
                .Select(u => u.ToUserViewModel())]
        );
    }

    public static UserViewModel ToUserViewModel(this UserEntity userEntity)
    {
        return new UserViewModel(
            Id: userEntity.Id,
            Username: userEntity.Username,
            Email: userEntity.Email,
            Balance: userEntity.Balance,
            VerificationStatus: userEntity.VerificationStatus,
            IsBanned: userEntity.IsBanned,
            IsDeleted: userEntity.IsDeleted,
            FirstName: userEntity.FirstName,
            SecondName: userEntity.SecondName,
            LastName: userEntity.LastName,
            BirthDate: userEntity.BirthDate,
            PassportNumber: userEntity.PassportNumber,
            IdentificationNumber: userEntity.IdentificationNumber,
            Images: [.. userEntity.Images.Select(i => i.ToImageViewModel())]
        );
    }
}