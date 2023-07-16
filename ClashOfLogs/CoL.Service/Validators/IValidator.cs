namespace CoL.Service.Validators;

public interface IValidator<T>
{
    bool IsValid(T entity);
}