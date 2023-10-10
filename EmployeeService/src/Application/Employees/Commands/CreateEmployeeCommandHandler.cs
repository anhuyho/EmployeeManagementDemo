using EmployeeService.Application.Abstractions.Messaging;
using EmployeeService.Domain.Abstractions;
using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Shared;


namespace EmployeeService.Application.Employees.Commands;
public sealed record CreateEmployeeCommand(string Name, string Position, DateTime? HiringDate, decimal Salary) : ICommand<Guid>;
public sealed class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, Guid>
{
    private readonly IEmployeeRepository _employeeRepository;

    public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result<Guid>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = Employee.Create(Guid.NewGuid(), request.Name, request.Position, request.HiringDate, request.Salary);

        await _employeeRepository.AddAsync(employee);

        return Result<Guid>.Success(employee.Id);
    }
}
