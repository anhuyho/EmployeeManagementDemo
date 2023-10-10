var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("EmployeeService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["EmployeeServiceEndPoint"]);
});

builder.Services.AddHostedService<ReportService>();

var app = builder.Build();

app.Run();