using System.ComponentModel.DataAnnotations.Schema;
using Backend.Enums;
using Backend.Extensions;

namespace Backend.Models;

public class Transaction
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public Guid DonatedUserId { get; set; }

    [ForeignKey(nameof(DonatedUserId))]
    public User DonatedBy { get; set; }

    public int Amount { get; set; }
    public DateTime DonatedOn { get; set; } = DateTime.UtcNow;
    public Guid CreatedUserId { get; set; }

    [ForeignKey(nameof(CreatedUserId))]
    public User CreatedBy { get; set; }

    [NotMapped]
    public TransactionType Type { get; set; }

    [Column(nameof(Type))]
    public string TypeStr
    {
        get => Type.ToString();
        set => Type = value.ParseEnum<TransactionType>();
    }
}