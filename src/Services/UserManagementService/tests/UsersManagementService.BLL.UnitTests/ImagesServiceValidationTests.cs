using FluentValidation.TestHelper;
using Moq;
using NSubstitute;
using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.DeleteImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;
using static UsersManagementService.BLL.UnitTests.TestEntities.TestImageEntities;

namespace UsersManagementService.BLL.UnitTests;

public class ImagesServiceValidationTests
{

    [Fact]
    public async Task Should_Not_Have_Error_When_All_Fields_Are_ValidAsync()
    {
        // Arrange
        var validator = new CreateImageModelValidator();

        // Act
        var result = await validator.TestValidateAsync(CreateModel);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_Have_Error_When_Id_Is_Empty()
    {
        // Arrange
        var invalidModel = CreateModel with { Id = Guid.Empty };
        var validator = new CreateImageModelValidator();

        // Act
        var result = await validator.TestValidateAsync(invalidModel);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public async Task Should_Have_Error_When_UserId_Is_Empty()
    {
        // Arrange
        var invalidModel = CreateModel with { UserId = Guid.Empty };
        var validator = new CreateImageModelValidator();

        // Act
        var result = await validator.TestValidateAsync(invalidModel);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Should_Have_Error_When_ImageUrl_Is_Invalid(string imageUrl)
    {
        // Arrange
        var invalidModel = CreateModel with { ImageUrl = imageUrl };
        var validator = new CreateImageModelValidator();

        // Act
        var result = await validator.TestValidateAsync(invalidModel);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ImageUrl);
    }

    [Fact]
    public async Task Should_Not_Have_Error_When_DeletedId_Is_Valid_And_User_Is_Exists()
    {
        // Arrange
        _imagesRepositoryMock.DoesImageExistAsync(DeleteModel, It.IsAny<CancellationToken>())
            .Returns(true);

        var validator = new DeleteImageValidator(_imagesRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(DeleteModel);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_Have_Error_When_DeletedId_Is_Empty()
    {
        // Arrange
        var invalidModel = Guid.Empty;

        var validator = new DeleteImageValidator(_imagesRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(invalidModel);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c);
    }

    [Fact]
    public async Task Should_Have_Error_When_DeletedUser_Does_Not_Exist()
    {
        // Arrange
        _imagesRepositoryMock.DoesImageExistAsync(DeleteModel, It.IsAny<CancellationToken>())
            .Returns(false);

        var validator = new DeleteImageValidator(_imagesRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(DeleteModel);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c);
    }

    [Fact]
    public async Task Should_Not_Have_Error_When_All_Fields_Are_Valid()
    {
        // Arrange
        _imagesRepositoryMock.DoesImageExistAsync(UpdateModel.Id, It.IsAny<CancellationToken>())
            .Returns(true);

        var validator = new UpdateImageModelValidator(_imagesRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(UpdateModel);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_Have_Error_When_UpdatedId_Is_Empty()
    {
        // Arrange
        var invalidModel = UpdateModel with { Id = Guid.Empty };

        var validator = new UpdateImageModelValidator(_imagesRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(invalidModel);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id);
    }

    [Fact]
    public async Task Should_Have_Error_When_UpdatedUserId_Is_Empty()
    {
        // Arrange
        var invalidModel = UpdateModel with { UserId = Guid.Empty };

        var validator = new UpdateImageModelValidator(_imagesRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(invalidModel);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.UserId);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Should_Have_Error_When_YpdatedImageUrl_Is_Invalid(string imageUrl)
    {
        // Arrange
        var invalidModel = UpdateModel with { ImageUrl = imageUrl };

        var validator = new UpdateImageModelValidator(_imagesRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(invalidModel);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.ImageUrl);
    }

}
