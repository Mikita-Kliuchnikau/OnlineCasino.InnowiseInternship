namespace UsersManagementService.BLL.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class ValidateAttribute(Type validatorType) : Attribute
{
    public Type ValidatorType { get; } = validatorType;
}
