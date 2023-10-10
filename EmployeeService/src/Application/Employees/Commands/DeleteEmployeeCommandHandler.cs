using EmployeeService.Application.Abstractions.Messaging;
using EmployeeService.Domain.Abstractions;
using EmployeeService.Domain.Shared;
using MediatR;


namespace EmployeeService.Application.Employees.Commands;
public sealed record DeleteEmployeeCommand(Guid employeeId) : ICommand<Unit>;
public sealed class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand, Unit>
{
    private readonly IEmployeeRepository _employeeRepository;
    public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result<Unit>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        await _employeeRepository.RemoveAsync(request.employeeId);
        return Result<Unit>.Success(Unit.Value);
    }
}
