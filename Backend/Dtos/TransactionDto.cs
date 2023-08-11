namespace Backend.Dtos;

public class TransactionDto
{
    public Guid DonatedUserId { get; set; }
    public int Amount { get; set; }
    public Guid CreatedUserId { get; set; }
    public string TransactionType { get; set; }
}