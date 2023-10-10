using EmployeeService.Application.Abstractions.Messaging;
using EmployeeService.Domain.Abstractions;
using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Shared;


namespace EmployeeService.Application.Employees.Commands;
public sealed record UpdateEmployeeCommand(Guid employeeId, Employee Employee) : ICommand<Employee>;
public sealed class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand, Employee>
{
    private readonly IEmployeeRepository _employeeRepository;
    public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result<Employee>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var updatedEmployee = Employee.Create(request.employeeId, request.Employee.Name, request.Employee.Position, request.Employee.HiringDate, request.Employee.Salary);

        var success = await _employeeRepository.UpdateAsync(updatedEmployee);

        return success ? Result<Employee>.Success(updatedEmployee) : Result<Employee>.Failure(new Error("Employee.NotFound", $"Could not find employee {request.employeeId} to update"));
    }
}
