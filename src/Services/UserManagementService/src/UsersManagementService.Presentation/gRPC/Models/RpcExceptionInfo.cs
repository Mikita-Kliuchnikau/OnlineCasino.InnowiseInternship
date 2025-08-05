namespace UsersManagementService.Presentation.gRPC.Models;

public record RpcExceptionInfo(int StatusCode, string Details);
