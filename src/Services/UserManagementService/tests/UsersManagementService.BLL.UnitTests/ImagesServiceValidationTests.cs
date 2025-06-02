using FluentValidation.TestHelper;
using Moq;
using NSubstitute;
using UsersManagementService.BLL.Validators.ImagesValidators;
using static UsersManagementService.BLL.UnitTests.TestEntities.TestImageEntities;

namespace UsersManagementService.BLL.UnitTests;

public class ImagesServiceValidationTests
{
    [Fact]
    public async Task Should_Not_Have_Error_When_All_Fields_Are_Valid()
    {
        // Arrange
        var idValidator = new ImageIdValidator();
        var validator = new ImageModelValidator(idValidator);

        // Act
        var result = await validator.TestValidateAsync(ImageModel, options => options.IncludeAllRuleSets());

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_Have_Error_When_Id_Is_Empty()
    {
        // Arrange
        var invalidModel = ImageModel with { Id = Guid.Empty };
        var idValidator = new ImageIdValidator();
        var validator = new ImageModelValidator(idValidator);

        // Act
        var result = await validator.TestValidateAsync(invalidModel, options => options.IncludeAllRuleSets());

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public async Task Should_Have_Error_When_UserId_Is_Empty()
    {
        // Arrange
        var invalidModel = ImageModel with { UserId = Guid.Empty };
        var idValidator = new ImageIdValidator();
        var validator = new ImageModelValidator(idValidator);

        // Act
        var result = await validator.TestValidateAsync(invalidModel, options => options.IncludeAllRuleSets());

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Should_Have_Error_When_ImageUrl_Is_Invalid(string imageUrl)
    {
        // Arrange
        var invalidModel = ImageModel with { ImageUrl = imageUrl };
        var idValidator = new ImageIdValidator();
        var validator = new ImageModelValidator(idValidator);

        // Act
        var result = await validator.TestValidateAsync(invalidModel, options => options.IncludeAllRuleSets());

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ImageUrl);
    }

    [Fact]
    public async Task Should_Not_Have_Error_When_DeletedId_Is_Valid()
    {
        // Arrange
        var validator = new ImageIdValidator();

        // Act
        var result = await validator.TestValidateAsync(DeleteModel, options => options.IncludeAllRuleSets());

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
        var result = await validator.TestValidateAsync(invalidModel, options => options.IncludeAllRuleSets());

        // Assert
        result.ShouldHaveValidationErrorFor(c => c);
    }

    [Fact]
    public async Task Should_Have_Error_When_UpdatedId_Is_Empty()
    {
        // Arrange
        var invalidModel = ImageModel with { Id = Guid.Empty };
        var idValidator = new ImageIdValidator();
        var validator = new ImageModelValidator(idValidator);

        // Act
        var result = await validator.TestValidateAsync(invalidModel, options => options.IncludeAllRuleSets());

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public async Task Should_Have_Error_When_UpdatedUserId_Is_Empty()
    {
        // Arrange
        var invalidModel = ImageModel with { UserId = Guid.Empty };
        var idValidator = new ImageIdValidator();
        var validator = new ImageModelValidator(idValidator);

        // Act
        var result = await validator.TestValidateAsync(invalidModel, options => options.IncludeAllRuleSets());

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.UserId);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Should_Have_Error_When_YpdatedImageUrl_Is_Invalid(string imageUrl)
    {
        // Arrange
        var invalidModel = ImageModel with { ImageUrl = imageUrl };
        var idValidator = new ImageIdValidator();
        var validator = new ImageModelValidator(idValidator);

        // Act
        var result = await validator.TestValidateAsync(invalidModel, options => options.IncludeAllRuleSets());

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.ImageUrl);
    }

}
