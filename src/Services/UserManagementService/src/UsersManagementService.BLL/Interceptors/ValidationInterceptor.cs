using Castle.DynamicProxy;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Reflection;
using UsersManagementService.BLL.Attributes;
using IValidatorFactory = UsersManagementService.BLL.Interfaces.Validators.IValidatorFactory;
using ValidationResult = FluentValidation.Results.ValidationResult;
using ValidationException = FluentValidation.ValidationException;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace UsersManagementService.BLL.Interceptors;

public class ValidationInterceptor( 
    ILogger<ValidationInterceptor> logger,
    IValidatorFactory factory) : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        var method = invocation.MethodInvocationTarget ?? invocation.Method;
        var attribute = method.GetCustomAttributes<ValidateAttribute>();
        var validatorType = attribute?.FirstOrDefault()?.ValidatorType;
        if (validatorType is null)
        {
            logger.LogError("Invalid validation attribute");
            invocation.Proceed();
            return;
        }
        var genericMethod = factory.GetType()
            .GetMethod(nameof(IValidatorFactory.GetValidatorOrThrow))
            ?.MakeGenericMethod(validatorType);
        var validator = genericMethod?.Invoke(factory, null);
        var model = invocation.Arguments.FirstOrDefault();
        var validateMethod = validator?.GetType()
            .GetMethods()
            .FirstOrDefault(m =>
                m.Name == "ValidateAsync" &&
                m.GetParameters()[0].ParameterType != typeof(IValidationContext));
        if (validateMethod is null)
        {
            logger.LogError("Validation method not found");
            invocation.Proceed();
            return;
        }
        if (model is null)
        {
            logger.LogError("Validation model is empty");
            invocation.Proceed();
            return;
        }
        logger.LogDebug("Processing validation {ValidationName}, {@Model}", method.Name, model);
        var task = validateMethod.Invoke(validator, [model, CancellationToken.None]) as Task<ValidationResult>;
        var validationResult = task?.GetAwaiter().GetResult();
        if (validationResult is null || !validationResult.IsValid)
        {
            throw new ValidationException($"Validation failed for {method.Name}", validationResult?.Errors);
        }
        logger.LogDebug("Completed validation {ValidationName}, {@Model}", method.Name, model);

        invocation.Proceed();
    }
}
