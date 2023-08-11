namespace Backend.Dtos;

public class TransactionDto
{
    public Guid DonatedUserId { get; set; }
    public int AmountDonated { get; set; }
    public Guid CreatedUserId { get; set; }
}