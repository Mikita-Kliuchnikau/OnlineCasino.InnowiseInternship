using Microsoft.AspNetCore.Http;
using System.Text;
using UsersManagementService.Common.Enums;
using UsersManagementService.Presentation.Models;
using static UsersManagementService.IntegrationTests.TestEntities.UserTestEntities;

namespace UsersManagementService.IntegrationTests.TestEntities;

public static class ImageTestEntities
{
    public static readonly ImageDto BaseImageDto = new()
    {
        UserId = BaseTestGuid,
        Type = ImageType.Default,
        File = TestFile
    };

    private static IFormFile? _file;

    private static IFormFile TestFile
    {
        get
        {
            if (_file == null)
            {
                var content = "Hello World";
                var fileName = "test.pdf";
                var stream = new MemoryStream();
                var writer = new StreamWriter(stream);
                writer.Write(content);
                writer.Flush();
                stream.Position = 0;
                _file = new FormFile(stream, 0, stream.Length, "file", fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "multipart/form-data"
                };
            }
            return _file;
        }
    }
}
