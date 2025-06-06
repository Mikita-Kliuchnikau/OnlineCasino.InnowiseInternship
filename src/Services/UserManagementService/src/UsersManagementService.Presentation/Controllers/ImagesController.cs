using Mapster;
using Microsoft.AspNetCore.Mvc;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Models.Image;
using UsersManagementService.Common.Constants;
using UsersManagementService.Presentation.Models;

namespace UsersManagementService.Presentation.Controllers;

[Produces(MediaTypeConstants.Json)]
[Route("api/[controller]")]
public class ImagesController(IImagesService imagesService) : ControllerBase
{
    [HttpPost]
    public async Task<Guid> Create([FromForm] ImageDto image, CancellationToken cancellationToken = default)
    {
        var imageModel = image.Adapt<ImageModel>();
        return await imagesService.CreateImageAsync(imageModel, cancellationToken);
    }

    [HttpDelete("{id}")]
    public async Task<Guid> Delete(Guid id, CancellationToken cancellationToken)
    {
        return await imagesService.DeleteImageAsync(id, cancellationToken);
    }
}
