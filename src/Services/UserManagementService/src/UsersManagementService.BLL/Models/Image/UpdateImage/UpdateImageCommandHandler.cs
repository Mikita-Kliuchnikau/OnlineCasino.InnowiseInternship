using MediatR;
using UsersManagementService.BLL.Interfaces;

namespace UsersManagementService.BLL.Models.Image.UpdateImage;

public class UpdateImageCommandHandler(IImagesService imagesService)
    : IRequestHandler<UpdateImageCommand, Guid>
{
    public async Task<Guid> Handle(
        UpdateImageCommand request,
        CancellationToken cancellationToken)
    {
        return await imagesService.UpdateImageAsync(request, cancellationToken);
    }
}
