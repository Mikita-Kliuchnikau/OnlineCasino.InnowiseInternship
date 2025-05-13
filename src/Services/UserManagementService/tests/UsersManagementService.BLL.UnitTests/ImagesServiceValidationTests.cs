using FluentValidation.TestHelper;
using Moq;
using NSubstitute;
using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.DeleteImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;
using static UsersManagementService.BLL.UnitTests.TestEntities.ImageValidationTestEntities;

namespace UsersManagementService.BLL.UnitTests;

public class ImagesServiceValidationTests
{

    [Fact]
    public async Task Should_Not_Have_Error_When_All_Fields_Are_ValidAsync()
    {
        // Arrange
        var validator = new CreateImageCommandValidator();

        // Act
        var result = await validator.TestValidateAsync(CreateCommand);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_Have_Error_When_Id_Is_Empty()
    {
        // Arrange
        var invalidCommand = CreateCommand with { Id = Guid.Empty };
        var validator = new CreateImageCommandValidator();

        // Act
        var result = await validator.TestValidateAsync(invalidCommand);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public async Task Should_Have_Error_When_UserId_Is_Empty()
    {
        // Arrange
        var invalidCommand = CreateCommand with { UserId = Guid.Empty };
        var validator = new CreateImageCommandValidator();

        // Act
        var result = await validator.TestValidateAsync(invalidCommand);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Should_Have_Error_When_ImageUrl_Is_Invalid(string imageUrl)
    {
        // Arrange
        var invalidCommand = CreateCommand with { ImageUrl = imageUrl };
        var validator = new CreateImageCommandValidator();

        // Act
        var result = await validator.TestValidateAsync(invalidCommand);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ImageUrl);
    }

    [Fact]
    public async Task Should_Not_Have_Error_When_DeletedId_Is_Valid_And_User_Is_Exists()
    {
        // Arrange
        _imagesRepositoryMock.DoesImageExistAsync(DeleteCommand.Id, It.IsAny<CancellationToken>())
            .Returns(true);

        var validator = new DeleteImageCommandValidator(_imagesRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(DeleteCommand);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_Have_Error_When_DeletedId_Is_Empty()
    {
        // Arrange
        var invalidCommand = DeleteCommand with { Id = Guid.Empty };

        var validator = new DeleteImageCommandValidator(_imagesRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(invalidCommand);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id);
    }

    [Fact]
    public async Task Should_Have_Error_When_DeletedUser_Does_Not_Exist()
    {
        // Arrange
        _imagesRepositoryMock.DoesImageExistAsync(DeleteCommand.Id, It.IsAny<CancellationToken>())
            .Returns(false);

        var validator = new DeleteImageCommandValidator(_imagesRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(DeleteCommand);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id);
    }

    [Fact]
    public async Task Should_Not_Have_Error_When_All_Fields_Are_Valid()
    {
        // Arrange
        _imagesRepositoryMock.DoesImageExistAsync(UpdateCommand.Id, It.IsAny<CancellationToken>())
            .Returns(true);

        var validator = new UpdateImageCommandValidator(_imagesRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(UpdateCommand);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_Have_Error_When_UpdatedId_Is_Empty()
    {
        // Arrange
        var invalidCommand = UpdateCommand with { Id = Guid.Empty };

        var validator = new UpdateImageCommandValidator(_imagesRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(invalidCommand);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id);
    }

    [Fact]
    public async Task Should_Have_Error_When_UpdatedUserId_Is_Empty()
    {
        // Arrange
        var invalidCommand = UpdateCommand with { UserId = Guid.Empty };

        var validator = new UpdateImageCommandValidator(_imagesRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(invalidCommand);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.UserId);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Should_Have_Error_When_YpdatedImageUrl_Is_Invalid(string imageUrl)
    {
        // Arrange
        var invalidCommand = UpdateCommand with { ImageUrl = imageUrl };

        var validator = new UpdateImageCommandValidator(_imagesRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(invalidCommand);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.ImageUrl);
    }

}
