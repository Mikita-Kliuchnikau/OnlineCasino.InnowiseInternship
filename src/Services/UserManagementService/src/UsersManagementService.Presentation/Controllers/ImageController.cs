using Mapster;
using Microsoft.AspNetCore.Mvc;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;
using UsersManagementService.Presentation.Constants;
using UsersManagementService.Presentation.Models;

namespace UsersManagementService.Presentation.Controllers;

[Produces(MediaTypeConstants.Json)]
[Route("api/[controller]")]
public class ImagesController(IImagesService imagesService) : ControllerBase
{
    [HttpPost]
    public async Task<Guid> Create(ImageDto image, CancellationToken cancellationToken = default)
    {
        var imageModel = image.Adapt<CreateImageModel>();
        return await imagesService.CreateImageAsync(imageModel, cancellationToken);
    }

    [HttpPut]
    public async Task<Guid> Update(ImageDto image, CancellationToken cancellationToken = default)
    {
        var imageModel = image.Adapt<UpdateImageModel>();
        return await imagesService.UpdateImageAsync(imageModel, cancellationToken);
    }

    [HttpDelete("{id}")]
    public async Task<Guid> Delete(Guid id, CancellationToken cancellationToken)
    {
        return await imagesService.DeleteImageAsync(id, cancellationToken);
    }

}
