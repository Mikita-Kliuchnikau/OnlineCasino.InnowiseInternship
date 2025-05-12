using UsersManagementService.BLL.Models.User.GetUser;

namespace UsersManagementService.BLL.Models.User.GetPagedUsers;

public record PagedUsersViewModel(int PageNumber, int TotalCount, List<UserViewModel>? UserViewModels);