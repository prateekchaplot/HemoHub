namespace Backend.Models;

public class SecurityToken
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpireInDays { get; set; }
}