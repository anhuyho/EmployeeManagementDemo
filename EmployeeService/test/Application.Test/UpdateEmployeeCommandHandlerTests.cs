using EmployeeService.Application.Employees.Commands;
using EmployeeService.Domain.Abstractions;
using EmployeeService.Domain.Entities;
using Moq;
using Shouldly;

namespace Application.Test;

public class UpdateEmployeeCommandHandlerTests
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;

    public UpdateEmployeeCommandHandlerTests()
    {
        _employeeRepositoryMock = new();
    }

    [Fact]
    public async Task Handler_Should_ReturnTrueResult_WhenUpdateEmployee()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var updateEmployeeCommand = new UpdateEmployeeCommand(employeeId, Employee.Create(employeeId, "Tim Cook", "CEO", DateTime.UtcNow, 60000));
        _employeeRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Employee>())).ReturnsAsync(true);
        var handler = new UpdateEmployeeCommandHandler(_employeeRepositoryMock.Object);

        // Act
        var result = await handler.Handle(updateEmployeeCommand, CancellationToken.None);

        // Assert
        _employeeRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Employee>()), Times.Once);
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value.Name.ShouldBe("Tim Cook");
    }

    [Fact]
    public async Task Handler_Should_ReturnFail_WhenUpdateANotExistedEmployee()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var updateEmployeeCommand = new UpdateEmployeeCommand(employeeId, Employee.Create(employeeId, "Steve Jobs", "Co-Founder", DateTime.UtcNow, 60000));

        _employeeRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Employee>())).ReturnsAsync(false);

        var handler = new UpdateEmployeeCommandHandler(_employeeRepositoryMock.Object);

        // Act
        var result = await handler.Handle(updateEmployeeCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeFalse(); // Check that the result indicates failure
        result.Error.ShouldNotBeNull();
        result.Error.Message.ShouldNotBeEmpty();
    }
}
