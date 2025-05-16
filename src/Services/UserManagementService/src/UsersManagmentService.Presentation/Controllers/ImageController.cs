using Mapster;
using Microsoft.AspNetCore.Mvc;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;
using UsersManagmentService.Presentation.Constants;
using UsersManagmentService.Presentation.Models;

namespace UsersManagmentService.Presentation.Controllers;

[Produces(MediaTypeConstants.Json)]
[Route("api/[controller]")]
public class ImagesController(IImagesService imagesService) : ControllerBase
{
    [HttpPost]
    public async Task<Guid> Create(ImageDto image, CancellationToken cancellationToken = default)
    {
        var ImageModel = image.Adapt<CreateImageModel>();
        return await imagesService.CreateImageAsync(ImageModel, cancellationToken);
    }

    [HttpPut]
    public async Task<Guid> Update(ImageDto image, CancellationToken cancellationToken = default)
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
