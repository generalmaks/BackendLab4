namespace BackendLab3.DataAccess.Models;

public class Healthcheck
{
    public string Status => "Healthy";
    public DateTime Time => DateTime.Now;
}