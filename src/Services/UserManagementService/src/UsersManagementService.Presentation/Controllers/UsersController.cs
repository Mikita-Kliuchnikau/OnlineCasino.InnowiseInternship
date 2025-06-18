using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Models.User;
using UsersManagementService.Common.Constants;
using UsersManagementService.Presentation.Models;

namespace UsersManagementService.Presentation.Controllers;

[Produces(MediaTypeConstants.Json)]
[Route("api/[controller]")]
public class UsersController(IUsersService usersService) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<PagedUsersViewModel> Get(
        [FromQuery] int page, 
        [FromQuery] int pageSize, 
        CancellationToken cancellationToken = default)
    {
        var usersQuery = new GetPagedUsersQuery(page, pageSize);
        return await usersService.GetPagedUsersAsync(usersQuery, cancellationToken);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<UserViewModel> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await usersService.GetUserByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    [Authorize]
    public async Task<Guid> Create([FromBody] CreateUserDto user, CancellationToken cancellationToken = default)
    {
        var userModel = user.Adapt<CreateUserModel>();
        return await usersService.CreateUserAsync(userModel, cancellationToken);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<Guid> Update(Guid id, [FromBody] UpdateUserDto user, CancellationToken cancellationToken = default)
    {
        var userModel = user.Adapt<UpdateUserModel>() with { Id = id };
        return await usersService.UpdateUserAsync(userModel, cancellationToken);
    }

    [HttpPatch("{id}")]
    [Authorize("ban:users")]
    public async Task<Guid> Ban(Guid id, bool isBanned, CancellationToken cancellationToken = default)
    {
        return await usersService.BanUserAsync(id, isBanned, cancellationToken);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<Guid> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        return await usersService.DeleteUserAsync(id, cancellationToken);
    }
}
