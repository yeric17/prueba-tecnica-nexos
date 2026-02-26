namespace Application.Users
{
    public record LoginCommandResponse
    {
        public string AccessToken { get; init; } = string.Empty;
    }
}
