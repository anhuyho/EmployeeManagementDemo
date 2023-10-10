using EmployeeService.Application.Abstractions.Messaging;
using EmployeeService.Domain.Abstractions;
using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Shared;

namespace EmployeeService.Application.Employees.Queries;
public sealed record GetEmployeeByIdQuery(Guid EmployeeId) : IQuery<Employee>;
public sealed class GetEmployeeByIdQueryHandler : IQueryHandler<GetEmployeeByIdQuery, Employee>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICacheService _cacheService;
    public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository, ICacheService cacheService)
    {
        _employeeRepository = employeeRepository;
        _cacheService = cacheService;
    }

    public async Task<Result<Employee>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"GetEmployeeById_{request.EmployeeId}";

        var employee = await _cacheService.GetOrCreateAsync(cacheKey, async () => await _employeeRepository.GetByIdAsync(request.EmployeeId));

        if (employee is null)
            return Result<Employee>.Failure(new Error("Employee.NotFound", $"The employee with Id {request.EmployeeId} was not found"));

        return new Result<Employee>(employee);
    }
}