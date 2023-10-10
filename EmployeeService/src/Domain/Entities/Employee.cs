namespace EmployeeService.Domain.Entities;

public abstract class Entity
{
    protected Entity(Guid id) => Id = id;
    protected Entity() { }
    public Guid Id { get; protected set; }
}
public class Employee : Entity
{
    public string Name { get; private set; }
    public string Position { get; private set; }
    public DateTime HiringDate { get; private set; }
    public decimal Salary { get; private set; }

    protected Employee(Guid id, string name, string position, DateTime hiringDate, decimal salary) : base(id)
    {
        Name = name;
        Position = position;
        HiringDate = hiringDate;
        Salary = salary;
    }
    public static Employee Create(Guid id, string name, string position, DateTime? hiringDate, decimal salary)
    {
        if (string.IsNullOrEmpty(name) || hiringDate is null || salary < 0)
            throw new EmployeeCannotCreatedException();

        return new Employee(id, name, position, hiringDate.Value, salary);
    }
}
