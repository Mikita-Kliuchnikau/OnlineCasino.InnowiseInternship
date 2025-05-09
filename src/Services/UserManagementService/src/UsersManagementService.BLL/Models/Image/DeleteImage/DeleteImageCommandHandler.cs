using MediatR;
using UsersManagementService.BLL.Interfaces;

namespace UsersManagementService.BLL.Models.Image.DeleteImage;

public class DeleteImageCommandHandler(IImagesService imagesService)
    : IRequestHandler<DeleteImageCommand, Guid>
{
    public async Task<Guid> Handle(
        DeleteImageCommand request,
        CancellationToken cancellationToken)
    {
        return await imagesService.DeleteImageAsync(request, cancellationToken);
    }
}
