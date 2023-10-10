using EmployeeService.Application.Abstractions.Messaging;
using EmployeeService.Domain.Abstractions;
using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Shared;

namespace EmployeeService.Application.Employees.Queries;
public sealed record GetEmployeesQuery() : IQuery<IEnumerable<Employee>>;

public sealed class GetEmployeesQueryHandler : IQueryHandler<GetEmployeesQuery, IEnumerable<Employee>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICacheService _cacheService;
    public GetEmployeesQueryHandler(IEmployeeRepository employeeRepository, ICacheService cacheService)
    {
        _employeeRepository = employeeRepository;
        _cacheService = cacheService;
    }

    public async Task<Result<IEnumerable<Employee>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"GetEmployeesQuery";

        var employees = await _cacheService.GetOrCreateAsync(cacheKey, async () => await _employeeRepository.GetAsync());

        return new Result<IEnumerable<Employee>>(employees);
    }
}