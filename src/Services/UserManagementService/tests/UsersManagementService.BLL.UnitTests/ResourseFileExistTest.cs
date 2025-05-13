using FluentAssertions;
using System.Resources;
using UsersManagementService.BLL.Resources;
using UsersManagementService.Common.Enums;
using UsersManagementService.Common.Helpers;

namespace UsersManagementService.BLL.UnitTests;

public class ResourseFileExistTest
{
    [Fact]
    public void Should_Contain_Resource_File_WithResourceHelper()
    {
        var resourceHelper = new ResourceHelper<UserMessages>(CulturePreference.English);
        var resourceValue = resourceHelper.GetValue(UserKeys.ValidationUserDoesntExist);

        resourceValue.Should().Be("User doesn't exist");
    }

    [Fact]
    public void Should_Contain_Resource_File_WithResourceManager()
    {
        var resourceManager = new ResourceManager(typeof(UserMessages));
        var resourceValue = resourceManager.GetString(UserKeys.ValidationUserDoesntExist);

        resourceValue.Should().Be("User doesn't exist");
    }
}