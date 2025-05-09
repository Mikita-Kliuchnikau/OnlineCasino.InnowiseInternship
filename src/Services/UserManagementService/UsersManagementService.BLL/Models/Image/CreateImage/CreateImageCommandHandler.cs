using MediatR;
using UsersManagementService.BLL.Interfaces;

namespace UsersManagementService.BLL.Models.Image.CreateImage;

public class CreateImageCommandHandler(IImagesService imagesService)
    : IRequestHandler<CreateImageCommand, Guid>
{
    public async Task<Guid> Handle(
        CreateImageCommand request, 
        CancellationToken cancellationToken)
    {
        return await imagesService.CreateImageAsync(request, cancellationToken);
    }
}