using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Transaction
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public Guid DonatedUserId { get; set; }

    [ForeignKey(nameof(DonatedUserId))]
    public User DonatedBy { get; set; }

    public int AmountDonated { get; set; }
    public DateTime DonatedOn { get; set; } = DateTime.UtcNow;
    public Guid CreatedUserId { get; set; }

    [ForeignKey(nameof(CreatedUserId))]
    public User CreatedBy { get; set; }
}