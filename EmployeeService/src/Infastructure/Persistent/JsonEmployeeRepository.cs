using EmployeeService.Domain.Abstractions;
using EmployeeService.Domain.Entities;
using System.Text.Json;

namespace EmployeeService.Infastructure.Persistent;

public class JsonEmployee
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public DateTime HiringDate { get; set; }
    public decimal Salary { get; set; }
}

public class JsonEmployeeRepository : IEmployeeRepository
{
    private List<Employee> _employeeData;
    private ICacheService _cacheService;

    private readonly string _jsonFilePath;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public JsonEmployeeRepository(RepositoryOptions options, ICacheService cacheService, CacheOptions caching)
    {
        _cacheService = cacheService;
        _jsonFilePath = options.JsonFilePath;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

        if (!File.Exists(_jsonFilePath))
        {
            File.WriteAllText(_jsonFilePath, "[]");
        }
        ReadEmployeesFromFile();
    }


    public async Task<IEnumerable<Employee>> GetAsync() => _employeeData;

    public async Task<Employee?> GetByIdAsync(Guid id) => _employeeData.Where(e => e.Id == id).SingleOrDefault();


    public async Task<Guid> AddAsync(Employee employee)
    {
        _employeeData.Add(employee);

        await WriteEmployeesToFile();
        return employee.Id;
    }

    public async Task<bool> UpdateAsync(Employee employee)
    {
        var existingEmployeeIndex = _employeeData.FindIndex(e => e.Id == employee.Id);

        if (existingEmployeeIndex != -1)
        {
            _employeeData[existingEmployeeIndex] = employee;
            await WriteEmployeesToFile();
            return true;
        }
        return false;
    }

    public async Task RemoveAsync(Guid id)
    {
        var employeeToRemove = _employeeData.FirstOrDefault(e => e.Id == id);

        if (employeeToRemove != null)
        {
            _employeeData.Remove(employeeToRemove);
            await WriteEmployeesToFile();
        }
    }



    private void ReadEmployeesFromFile()
    {
        var cacheKey = $"jsonEmployees";

        var readDataFromFileFunc = async () =>
        {
            var employees = new List<Employee>();

            var json = await File.ReadAllTextAsync(_jsonFilePath);

            if (!string.IsNullOrWhiteSpace(json))
            {
                employees = JsonSerializer.Deserialize<IEnumerable<JsonEmployee>>(json, _jsonSerializerOptions)?.Select(e => Employee.Create(e.Id, e.Name, e.Position, e.HiringDate, e.Salary)).ToList();
            }

            return employees;
        };

        _employeeData = _cacheService.GetOrCreateAsync(cacheKey, readDataFromFileFunc).Result;

    }
    private async Task WriteEmployeesToFile()
    {
        using var fileStream = File.Create(_jsonFilePath);
        await JsonSerializer.SerializeAsync(fileStream, _employeeData, _jsonSerializerOptions);
    }
}