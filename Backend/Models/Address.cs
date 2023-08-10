namespace Backend.Models;

public class Address
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
}