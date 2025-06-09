using FluentValidation.TestHelper;
using NSubstitute;
using UsersManagementService.BLL.Validators.UsersValidators;
using static UsersManagementService.BLL.UnitTests.TestEntities.TestUserEntities;


namespace UsersManagementService.BLL.UnitTests;

public class UsersServiceValidationTests
{
    [Fact]
    public async Task Should_Not_Have_Error_When_All_Fields_Are_Valid()
    {
        // Arrange
        _usersRepositoryMock.IsUserUniqueAsync(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(true);
        var validator = new CreateUserModelValidator(_usersRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(CreateModel);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_Have_Error_When_User_IsNot_Unique()
    {
        // Arrange
        _usersRepositoryMock.IsUserUniqueAsync(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(false);
        var validator = new CreateUserModelValidator(_usersRepositoryMock);
        // Act
        var result = await validator.TestValidateAsync(CreateModel);
        // Assert
        result.ShouldHaveValidationErrorFor(c => c);
    }

    [Fact]
    public async Task Should_Have_Error_When_AuthId_Is_Empty()
    {
        // Arrange
        _usersRepositoryMock.IsUserUniqueAsync(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(true);
        var invalidModel = CreateModel with { AuthId = Guid.Empty };
        var validator = new CreateUserModelValidator(_usersRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(invalidModel);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.AuthId);
    }

    [Theory]
    [InlineData("")]
    [InlineData("not-an-email")]
    [InlineData("user@")]
    public async Task Should_Have_Error_When_DeletedEmail_Is_Invalid(string email)
    {
        // Arrange
        var invalidModel = CreateModel with { Email = email };
        _usersRepositoryMock.IsUserUniqueAsync(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(true);
        var validator = new CreateUserModelValidator(_usersRepositoryMock);

        // Act
        var result = await validator.TestValidateAsync(invalidModel);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Email);
    }

    [Fact]
    public async Task Should_Not_Have_Error_When_DeletedId_Is_Valid()
    {
        // Arrange

        var validator = new UserIdValidator();

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

        var validator = new UserIdValidator();

        // Act
        var result = await validator.TestValidateAsync(invalidModel);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c);
    }

    [Fact]
    public async Task Should_Have_Error_When_Username_Is_Invalid()
    {
        // Arrange
        var invalidModel = UpdateModel with { Username = string.Empty };

        var validator = new UpdateUserModelValidator();

        // Act
        var result = await validator.TestValidateAsync(invalidModel);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Username);
    }

    [Theory]
    [InlineData("")]
    [InlineData("invalidemail")]
    [InlineData("invalid@")]
    public async Task Should_Have_Error_When_UpdatedEmail_Is_Invalid(string email)
    {
        // Arrange
        var invalidModel = UpdateModel with { Email = email };

        var validator = new UpdateUserModelValidator();

        // Act
        var result = await validator.TestValidateAsync(invalidModel);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Email);
    }

    [Fact]
    public async Task Should_Not_Have_Error_When_Optional_Fields_Are_Null()
    {
        // Arrange
        var validModel = UpdateModel with
        {
            FirstName = null,
            SecondName = null,
            LastName = null,
            PassportNumber = null,
            IdentificationNumber = null
        };

        var validator = new UpdateUserModelValidator();

        // Act
        var result = await validator.TestValidateAsync(validModel);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.FirstName);
        result.ShouldNotHaveValidationErrorFor(c => c.SecondName);
        result.ShouldNotHaveValidationErrorFor(c => c.LastName);
        result.ShouldNotHaveValidationErrorFor(c => c.PassportNumber);
        result.ShouldNotHaveValidationErrorFor(c => c.IdentificationNumber);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Should_Have_Error_When_FirstName_Is_Invalid(string name)
    {
        // Arrange
        var invalidModel = UpdateModel with { FirstName = name };

        var validator = new UpdateUserModelValidator();

        // Act
        var result = await validator.TestValidateAsync(invalidModel);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.FirstName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Should_Have_Error_When_SecondName_Is_Invalid(string name)
    {
        // Arrange
        var invalidModel = UpdateModel with { SecondName = name };

        var validator = new UpdateUserModelValidator();

        // Act
        var result = await validator.TestValidateAsync(invalidModel);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.SecondName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Should_Have_Error_When_LastName_Is_Invalid(string name)
    {
        // Arrange
        var invalidModel = UpdateModel with { LastName = name };

        var validator = new UpdateUserModelValidator();

        // Act
        var result = await validator.TestValidateAsync(invalidModel);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.LastName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Should_Have_Error_When_PassportNumber_Is_Invalid(string passport)
    {
        // Arrange
        var invalidModel = UpdateModel with { PassportNumber = passport };

        var validator = new UpdateUserModelValidator();

        // Act
        var result = await validator.TestValidateAsync(invalidModel);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.PassportNumber);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Should_Have_Error_When_IdentificationNumber_Is_Invalid(string identidication)
    {
        // Arrange
        var invalidModel = UpdateModel with { IdentificationNumber = identidication };

        var validator = new UpdateUserModelValidator();

        // Act
        var result = await validator.TestValidateAsync(invalidModel);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.IdentificationNumber);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    public async Task Should_Pass_When_PageNumber_And_PageSize_Are_Valid(int validValue)
    {
        // Arrange
        var query = GetPagedQuery with { PageNumber = validValue, PageSize = validValue };
        var validator = new GetPagedUsersQueryValidator();

        // Act
        var result = await validator.TestValidateAsync(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public async Task Should_Have_Error_When_PageNumber_Is_Invalid(int invalidValue)
    {
        // Arrange
        var query = GetPagedQuery with { PageNumber = invalidValue };
        var validator = new GetPagedUsersQueryValidator();

        // Act
        var result = await validator.TestValidateAsync(query);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.PageNumber);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public async Task Should_Have_Error_When_PageSize_Is_Invalid(int invalidValue)
    {
        // Arrange
        var query = GetPagedQuery with { PageSize = invalidValue };
        var validator = new GetPagedUsersQueryValidator();

        // Act
        var result = await validator.TestValidateAsync(query);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.PageSize);
    }
}