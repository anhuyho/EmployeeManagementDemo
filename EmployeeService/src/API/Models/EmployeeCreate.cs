namespace API.Controllers;

public class EmployeeModel
{
    public required string Name { get; set; }
    public string? Position { get; set; }
    public DateTime HiringDate { get; set; } = DateTime.UtcNow;
    public decimal Salary { get; set; }
    public Guid Id { get; set; }
}
