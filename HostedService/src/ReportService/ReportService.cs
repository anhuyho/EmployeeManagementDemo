using System.Text.Json;

public class Employee
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public DateTime HiringDate { get; set; }
    public decimal Salary { get; set; }
}
public class ReportService : IHostedService, IDisposable
{
    private readonly ILogger<ReportService> _logger;
    private Timer _timer;
    private readonly int Schedule = 10;
    private static string DELIMITER = ";";
    private readonly IHttpClientFactory _httpClientFactory;
    public ReportService(ILogger<ReportService> logger,
                            IHttpClientFactory httpClientFactory,
                            IConfiguration config)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;

        if (int.TryParse(config["Schedule"], out var schedule))
            Schedule = schedule;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("ReportService is starting.");

        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(Schedule));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("ReportService is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        _logger.LogInformation("ReportService is doing some work.");
        try
        {
            using var client = _httpClientFactory.CreateClient("EmployeeService");

            HttpResponseMessage response = client.GetAsync("/api/employees").Result;

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Could not get data from service .... ");
                return;
            }
            ReportGeneration(response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
        }
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    private void ReportGeneration(HttpResponseMessage response)
    {
        var jsonEmployees = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

        var employees = JsonSerializer.Deserialize<IEnumerable<Employee>>(jsonEmployees, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        });

        if (employees is null || employees.Count() <= 0) return;

        if (!Directory.Exists("Reports"))
        {
            Directory.CreateDirectory("Reports");
        }

        var fileName = Path.Combine("Reports", "Employees.csv");
        File.WriteAllLines(
            fileName
            , employees.Select(employee => $"{employee.Id}{DELIMITER} {employee.Name}{DELIMITER} {employee.Position}{DELIMITER} {employee.HiringDate}{DELIMITER} {employee.Salary}")
            );

        _logger.LogInformation($"Get data successfully !!!");
    }
}
