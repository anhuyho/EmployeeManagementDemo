using EmployeeService.Domain.Entities;

namespace EmployeeService.Domain.Abstractions;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAsync();
    Task<Employee?> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(Employee employee);
    Task<bool> UpdateAsync(Employee employee);
    Task RemoveAsync(Guid id);
}