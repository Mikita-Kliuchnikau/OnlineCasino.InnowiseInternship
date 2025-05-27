namespace UsersManagementService.BLL.Models.User;

public record PagedUsersViewModel(int PageNumber, int TotalCount, List<UserViewModel>? UserViewModels);