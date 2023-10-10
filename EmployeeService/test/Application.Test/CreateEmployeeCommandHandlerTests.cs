using EmployeeService.Application.Employees.Commands;
using EmployeeService.Domain.Abstractions;
using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Exceptions;
using Moq;
using Shouldly;

namespace Application.Test;
using System;
using System.Threading.Tasks;
using EmployeeService.Application.Employees.Commands;
using EmployeeService.Domain.Entities;
using Xunit;

public class CreateEmployeeCommandHandlerTests
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;

    public CreateEmployeeCommandHandlerTests()
    {
        _employeeRepositoryMock = new();
    }

    [Fact]
    public async Task Handler_Should_ReturnTrueResult_WhenCreateANewEmployee()
    {
        //Arrange
        var createEmployeeCommand = new CreateEmployeeCommand("Jony Ive", "Desinger", DateTime.UtcNow, 3600);
        var handler = new CreateEmployeeCommandHandler(_employeeRepositoryMock.Object);

        //Act
        var result = await handler.Handle(createEmployeeCommand, CancellationToken.None);

        //Assert
        _employeeRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Employee>()), Times.Once);
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBe(Guid.Empty);
    }
    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateEmployee()
    {
        // Arrange
        var employeeRepositoryMock = new Mock<IEmployeeRepository>();
        var createEmployeeCommand = new CreateEmployeeCommand("John Doe", "Developer", DateTime.UtcNow, 50000);
        var handler = new CreateEmployeeCommandHandler(employeeRepositoryMock.Object);

        // Act
        var result = await handler.Handle(createEmployeeCommand, CancellationToken.None);

        // Assert
        employeeRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Employee>()), Times.Once);
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBe(Guid.Empty);
    }

    [Fact]
    public async Task Handle_InvalidName_ShouldReturnThrowExceptionResult()
    {
        // Arrange
        var employeeRepositoryMock = new Mock<IEmployeeRepository>();
        var createEmployeeCommand = new CreateEmployeeCommand("", "Developer", DateTime.UtcNow, 50000);
        var handler = new CreateEmployeeCommandHandler(employeeRepositoryMock.Object);

        // Act
        var result = handler.Handle(createEmployeeCommand, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<EmployeeCannotCreatedException>(() => result);
    }

    [Fact]
    public async Task Handle_InvalidSalary_ShouldReturnThrowExceptionResult()
    {
        // Arrange
        var employeeRepositoryMock = new Mock<IEmployeeRepository>();
        var createEmployeeCommand = new CreateEmployeeCommand("John Doe", "Developer", DateTime.UtcNow, -50000);
        var handler = new CreateEmployeeCommandHandler(employeeRepositoryMock.Object);

        // Act
        var result = handler.Handle(createEmployeeCommand, CancellationToken.None);

        // Assert

        await Assert.ThrowsAsync<EmployeeCannotCreatedException>(() => result);
    }

    [Fact]
    public async Task Handle_InvalidHiringDate_ShouldReturnThrowExceptionResult()
    {
        // Arrange
        var employeeRepositoryMock = new Mock<IEmployeeRepository>();
        var createEmployeeCommand = new CreateEmployeeCommand("John Doe", "Developer", null, 50000);
        var handler = new CreateEmployeeCommandHandler(employeeRepositoryMock.Object);

        // Act
        var result = handler.Handle(createEmployeeCommand, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<EmployeeCannotCreatedException>(() => result);
    }


}