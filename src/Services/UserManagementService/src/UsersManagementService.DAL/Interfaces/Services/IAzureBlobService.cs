namespace UsersManagementService.DAL.Interfaces.Services;

public interface IAzureBlobService
{
    Task<string> UploadImageAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default);
}
