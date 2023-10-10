namespace EmployeeService.Infastructure;

public class RepositoryOptions
{
    public string JsonFilePath { get; set; } = string.Empty;
}
public class CacheOptions
{
    public int ExpireMinute { get; set; }
}
