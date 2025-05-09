using UsersManagementService.BLL.Models.User.Queries.GetUser;

namespace UsersManagementService.BLL.Models.User.Queries.GetPagedUsers;

public record PagedUsersViewModel(int PageNumber, int TotalCount, List<UserViewModel>? UserViewModels);