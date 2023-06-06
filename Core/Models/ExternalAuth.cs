using Core.Enums;

namespace Core.Models;

public class ExternalAuth : BaseEntity
{
    public string Key { get; set; }
    public ExternalProviderType ProviderType { get; set; }  
    public virtual int UserId { get; set; }
    public virtual User User { get; set; }
}