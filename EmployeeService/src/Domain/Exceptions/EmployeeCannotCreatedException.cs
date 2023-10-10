using System.Runtime.Serialization;

namespace EmployeeService.Domain.Entities;


public sealed class EmployeeCannotCreatedException : Exception
{
    public EmployeeCannotCreatedException()
    {
    }

    public EmployeeCannotCreatedException(string? message) : base(message)
    {
    }

    public EmployeeCannotCreatedException(string? message, Exception? innerException) : base(message, innerException)
    {

    }
}