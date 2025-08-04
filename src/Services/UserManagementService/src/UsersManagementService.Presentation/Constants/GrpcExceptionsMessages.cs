namespace UsersManagementService.Presentation.Constants;

public static class GrpcExceptionsMessages
{
    public const string InvalidUserIdFormat = "Invalid UserId format";
    public const string UserNotFound = "User with Id {0} not found";
    public const string UserBalanceDeductionFailed = "Failed to deduct balance from user";
    public const string MessageIdIsRequired = "MessageId is required";
    public const string MessageAlreadyProcessed = "Message already processed";
}
