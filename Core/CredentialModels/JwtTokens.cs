namespace Core.CredentialModels;

public class JwtTokens
{
    public string AccessToken { get; set; }
    public int? ExpiresAtTimestamp { get; set; }
    
    public int UserId { get; set; }
    
    public string UserEmail { get; set; }
    
    public IList<string> UserRoles { get; set; }
}