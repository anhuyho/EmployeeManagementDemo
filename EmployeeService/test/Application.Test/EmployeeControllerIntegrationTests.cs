using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using API.Controllers;
namespace API.Tests;
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class TestPriorityAttribute : Attribute
{
    public TestPriorityAttribute(int priority)
    {
        Priority = priority;
    }

    public int Priority { get; private set; }
}
public class EmployeeControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public EmployeeControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }
    [Fact, TestPriority(1)]
    public async Task CreateEmployee_WithValidEmployee_ReturnsCreatedAtActionAndId()
    {
        // Arrange
        var newEmployee = new EmployeeModel { Name = "David", Position = "Designer", HiringDate = DateTime.Now, Salary = 6000 };

        // Act
        var response = await _client.PostAsJsonAsync("api/employees", newEmployee);

        // Assert
        response.EnsureSuccessStatusCode();
        var id = await response.Content.ReadFromJsonAsync<Guid>();
        Assert.NotEqual(Guid.Empty, id);
    }
    [Fact, TestPriority(2)]
    public async Task GetEmployees_ReturnsOkAndListOfEmployees()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("api/employees");

        // Assert
        response.EnsureSuccessStatusCode();
        var actualEmployees = await response.Content.ReadFromJsonAsync<IEnumerable<EmployeeModel>>();

    }


    [Fact, TestPriority(3)]
    public async Task UpdateEmployee_WithValidIdAndEmployee_ReturnsNoContent()
    {
        // Arrange
        var existingEmployeeId = Guid.NewGuid();
        var updatedEmployee = new EmployeeModel { Id = existingEmployeeId, Name = "Eve", Position = "Analyst", HiringDate = DateTime.Now, Salary = 4500 };

        // Act
        var response = await _client.PutAsJsonAsync($"api/employees/{existingEmployeeId}", updatedEmployee);

        // Assert
        response.EnsureSuccessStatusCode();
    }


    [Fact, TestPriority(4)]
    public async Task DeleteEmployee_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var existingEmployeeId = Guid.NewGuid();

        // Act
        var response = await _client.DeleteAsync($"api/employees/{existingEmployeeId}");

        // Assert
        response.EnsureSuccessStatusCode();
    }
}