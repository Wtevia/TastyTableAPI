namespace Core.CredentialModels;

public class SocialTokenValidationResult
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Provider { get; set; }
    public string ProviderKey { get; set; }
}