using EmployeeService.Domain.Exceptions.Base;

namespace EmployeeService.Domain.Exceptions;

public sealed class EmployeeNotFoundException : NotFoundException
{
    public EmployeeNotFoundException(Guid employeeId) : base($"Employee with id {employeeId} was not found") { }
}