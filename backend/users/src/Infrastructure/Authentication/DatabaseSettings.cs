namespace Infrastructure.Authentication;

public sealed class DatabaseSettings
{
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string Name { get; set; } = null!;
    public string User { get; set; } = null!;
    public string Password { get; set; } = null!;
}
