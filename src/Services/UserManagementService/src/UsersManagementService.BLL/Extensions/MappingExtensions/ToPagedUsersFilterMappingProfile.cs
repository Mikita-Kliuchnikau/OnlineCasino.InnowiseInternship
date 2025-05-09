using UsersManagementService.BLL.Models.User.Queries.GetPagedUsers;
using UsersManagementService.DAL.Entites.DTO;

namespace UsersManagementService.BLL.Extensions.MappingExtensions
{
    public static class ToPagedUsersFilterMappingProfile
    {
        public static PagedUsersFilter ToPagedUsersFilter(this GetPagedUsersQuery getPagedUsersQuery)
        {
            return new PagedUsersFilter
            {
                PageNumber = getPagedUsersQuery.PageNumber,
                PageSize = getPagedUsersQuery.PageSize,
            };
        }
    }
}