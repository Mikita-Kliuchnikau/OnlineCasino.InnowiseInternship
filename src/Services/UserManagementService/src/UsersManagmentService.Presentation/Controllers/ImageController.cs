using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;
using UsersManagmentService.Presentation.Models;

namespace UsersManagmentService.Presentation.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
public class ImageController(IImagesService imagesService) : ControllerBase
{
    [HttpPost]
    public async Task<Guid> Create(ImageDTO image, CancellationToken cancellationToken = default)
    {
        var ImageModel = image.Adapt<CreateImageModel>();
        return await imagesService.CreateImageAsync(ImageModel, cancellationToken);
    }

    [HttpPut]
    public async Task<Guid> Update(ImageDTO image, CancellationToken cancellationToken = default)
    {
        var ImageModel = image.Adapt<UpdateImageModel>();
        return await imagesService.UpdateImageAsync(ImageModel, cancellationToken);
    }

    [HttpDelete("{id}")]
    public async Task<Guid> Delete(Guid id, CancellationToken cancellationToken)
    {
        return await imagesService.DeleteImageAsync(id, cancellationToken);
    }

}
