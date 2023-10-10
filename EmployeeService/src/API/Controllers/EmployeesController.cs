using EmployeeService.Application.Employees.Commands;
using EmployeeService.Application.Employees.Queries;
using EmployeeService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/employees")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeModel>>> GetEmployees()
    {
        var query = new GetEmployeesQuery();

        var employeesResult = await _mediator.Send(query);

        var employees = employeesResult.Value;

        if (employees is null) return NotFound();

        return Ok(employeesResult.Value);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeModel>> GetEmployee(Guid id)
    {
        var query = new GetEmployeeByIdQuery(id);
        var employeeResult = await _mediator.Send(query);

        if (employeeResult is null ||
            (!employeeResult.IsSuccess
                && employeeResult.Error is not null
                && employeeResult.Error.Type.Equals("Employee.NotFound", StringComparison.CurrentCultureIgnoreCase)))
            return NotFound();

        return Ok(employeeResult.Value);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateEmployee(EmployeeModel employee)
    {
        var createEmployeeCommand = new CreateEmployeeCommand(employee.Name, employee.Position, employee.HiringDate, employee.Salary);
        var id = (await _mediator.Send(createEmployeeCommand)).Value;
        return CreatedAtAction("GetEmployee", new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(Guid id, EmployeeModel employee)
    {
        var command = new UpdateEmployeeCommand(id, Employee.Create(id, employee.Name, employee.Position, employee.HiringDate, employee.Salary));

        if (id != command.employeeId)
        {
            return BadRequest();
        }

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(Guid id)
    {
        await _mediator.Send(new DeleteEmployeeCommand(id));
        return NoContent();
    }
}
