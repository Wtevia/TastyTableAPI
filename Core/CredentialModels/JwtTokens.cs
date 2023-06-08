namespace Core.CredentialModels;

public class JwtTokens
{
    public string AccessToken { get; set; }
    public DateTime AccessTokenExpiresAt { get; set; }
}