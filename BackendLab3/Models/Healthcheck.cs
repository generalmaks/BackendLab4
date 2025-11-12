namespace BackendLab3.Models;

public class Healthcheck
{
    public string Status => "Healthy";
    public DateTime Time => DateTime.Now;
}