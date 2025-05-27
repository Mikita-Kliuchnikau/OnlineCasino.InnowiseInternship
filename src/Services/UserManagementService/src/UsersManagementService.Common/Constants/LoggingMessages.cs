namespace UsersManagementService.Common.Constants;

public static class LoggingMessages
{
    public static readonly string ValidatingStartingMessage = "{0} Validating {1}, at {2}";
    public static readonly string ValidatingSucceededMessage = "{0} Validation succeeded for {1}, at {2}";
    public static readonly string ValidatingFailedMessage = "{0} Validation failed for {1} with error {2}, at {3}";
    public static readonly string RequestStartingMessage = "{0} Starting request processing, at {1}";
    public static readonly string RequestSucceededMessage = "{0} Request processed successfully, at {1}";
    public static readonly string RequestFailedMessage = "{0} Request processing failed with error {1}, at {2}";
}
