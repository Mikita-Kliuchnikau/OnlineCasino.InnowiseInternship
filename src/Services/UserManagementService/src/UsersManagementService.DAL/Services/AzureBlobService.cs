using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using UsersManagementService.DAL.Interfaces.Services;
using UsersManagementService.DAL.Options;
using BlobHttpHeaders = Azure.Storage.Blobs.Models.BlobHttpHeaders;

namespace UsersManagementService.DAL.Services;

public class AzureBlobService(IOptions<BlobStorageOptions> options) : IAzureBlobService
{
    private readonly BlobContainerClient _containerClient = new(options.Value.ConnectionString, options.Value.ContainerName);

    public async Task<string> UploadImageAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken = default)
    {
        _containerClient.CreateIfNotExists(cancellationToken: cancellationToken);
        var blobClient = _containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(
            fileStream,
            new BlobHttpHeaders { ContentType = contentType },
            cancellationToken: cancellationToken);

        var sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = _containerClient.Name,
            BlobName = fileName,
            Resource = "b",
            ExpiresOn = DateTimeOffset.UtcNow.AddYears(100)
        };
        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        var sasUri = blobClient.GenerateSasUri(sasBuilder);
        return sasUri.AbsoluteUri;
    }
}