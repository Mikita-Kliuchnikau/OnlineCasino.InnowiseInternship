using Mapster;
using Microsoft.AspNetCore.Mvc;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Models.User.CreateUser;
using UsersManagementService.BLL.Models.User.GetPagedUsers;
using UsersManagementService.BLL.Models.User.GetUser;
using UsersManagementService.BLL.Models.User.UpdateUser;
using UsersManagmentService.Presentation.Constants;
using UsersManagmentService.Presentation.Models;

namespace UsersManagmentService.Presentation.Controllers;

[Produces(MediaTypeConstants.Json)]
[Route("api/[controller]")]
public class UsersController(IUsersService usersService) : ControllerBase
{
    [HttpGet]
    public async Task<PagedUsersViewModel> Get(
        [FromQuery] int page, 
        [FromQuery] int pageSize, 
        CancellationToken cancellationToken = default)
    {
        var usersQuery = new GetPagedUsersQuery(page, pageSize);
        return await usersService.GetPagedUsersAsync(usersQuery, cancellationToken);
    }

    [HttpGet("{id}")]
    public async Task<UserViewModel> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await usersService.GetUserByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task<Guid> Create([FromBody] UserDto user, CancellationToken cancellationToken = default)
    {
        var userModel = user.Adapt<CreateUserModel>();
        return await usersService.CreateUserAsync(userModel, cancellationToken);
    }

    [HttpPut]
    public async Task<Guid> Update([FromBody] UserDto user, CancellationToken cancellationToken = default)
    {
        var userModel = user.Adapt<UpdateUserModel>();
        return await usersService.UpdateUserAsync(userModel, cancellationToken);
    }

    [HttpDelete("{id}")]
    public async Task<Guid> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        return await usersService.DeleteUserAsync(id, cancellationToken);
    }
}
