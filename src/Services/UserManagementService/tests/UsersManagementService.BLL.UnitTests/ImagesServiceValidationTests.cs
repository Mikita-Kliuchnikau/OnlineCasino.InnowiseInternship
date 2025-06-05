using FluentValidation.TestHelper;
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
        _imagesRepositoryMock
            .IsImageUniqeAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(true);
        var idValidator = new ImageIdValidator();
        var validator = new ImageModelValidator(idValidator, _imagesRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(ImageModel, options => options.IncludeAllRuleSets());

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_Have_Error_When_Id_Is_Empty()
    {
        // Arrange
        _imagesRepositoryMock
            .IsImageUniqeAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(true);
        var invalidModel = ImageModel with { Id = Guid.Empty };
        var idValidator = new ImageIdValidator();
        var validator = new ImageModelValidator(idValidator, _imagesRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(invalidModel, options => options.IncludeAllRuleSets());

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public async Task Should_Have_Error_When_UserId_Is_Empty()
    {
        // Arrange
        _imagesRepositoryMock
           .IsImageUniqeAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
          .Returns(true);
        var invalidModel = ImageModel with { UserId = Guid.Empty };
        var idValidator = new ImageIdValidator();
        var validator = new ImageModelValidator(idValidator, _imagesRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(invalidModel, options => options.IncludeAllRuleSets());

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public async Task Should_Have_Error_When_Id_Is_Not_Unique()
    {
        // Arrange
        _imagesRepositoryMock
            .IsImageUniqeAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(false);
        var invalidModel = ImageModel;
        var idValidator = new ImageIdValidator();
        var validator = new ImageModelValidator(idValidator, _imagesRepositoryMock);
        // Act
        var result = await validator.TestValidateAsync(invalidModel, options => options.IncludeAllRuleSets());
        // Assert
        result.ShouldHaveValidationErrorFor(x => x);
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
        _imagesRepositoryMock
          .IsImageUniqeAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
          .Returns(true);
        var invalidModel = ImageModel with { Id = Guid.Empty };
        var idValidator = new ImageIdValidator();
        var validator = new ImageModelValidator(idValidator, _imagesRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(invalidModel, options => options.IncludeAllRuleSets());

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public async Task Should_Have_Error_When_UpdatedUserId_Is_Empty()
    {
        // Arrange
        _imagesRepositoryMock
            .IsImageUniqeAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(true);
        var invalidModel = ImageModel with { UserId = Guid.Empty };
        var idValidator = new ImageIdValidator();
        var validator = new ImageModelValidator(idValidator, _imagesRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(invalidModel, options => options.IncludeAllRuleSets());

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.UserId);
    }
}
