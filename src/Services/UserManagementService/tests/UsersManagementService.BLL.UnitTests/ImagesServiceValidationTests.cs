using FluentValidation.TestHelper;
using UsersManagementService.BLL.Validators.ImagesValidators;
using static UsersManagementService.BLL.UnitTests.TestEntities.TestImageEntities;

namespace UsersManagementService.BLL.UnitTests;

public class ImagesServiceValidationTests
{
    [Fact]
    public async Task Should_Not_Have_Error_When_All_Fields_Are_Valid()
    {
        // Arrange
        var validator = new ImageModelValidator();

        // Act
        var result = await validator.TestValidateAsync(ImageModel);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_Have_Error_When_Id_Is_Empty()
    {
        // Arrange
        var invalidModel = ImageModel with { Id = Guid.Empty };
        var validator = new ImageModelValidator();

        // Act
        var result = await validator.TestValidateAsync(invalidModel);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public async Task Should_Have_Error_When_UserId_Is_Empty()
    {
        // Arrange
        var invalidModel = ImageModel with { UserId = Guid.Empty };
        var validator = new ImageModelValidator();

        // Act
        var result = await validator.TestValidateAsync(invalidModel);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public async Task Should_Not_Have_Error_When_DeletedId_Is_Valid()
    {
        // Arrange
        var validator = new ImageIdValidator();

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

        var validator = new ImageIdValidator();

        // Act
        var result = await validator.TestValidateAsync(invalidModel);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c);
    }
}
