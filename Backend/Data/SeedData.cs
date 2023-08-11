using System.Security.Cryptography;
using System.Text;
using Backend.Enums;
using Backend.Models;
using Bogus;

namespace Backend.Data;

public static class Seeder
{
    private static List<User> GenerateUsers(int count)
    {
        var userFaker = new Faker<User>()
            .RuleFor(u => u.Name, f => f.Name.FullName())
            .RuleFor(u => u.NormalizedName, (f, u) => u.Name.ToLower())
            .RuleFor(u => u.Password, f => f.Internet.Password(6))
            .RuleFor(u => u.PasswordHash, (f, u) => HashPassword(u.Password))
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
            .RuleFor(u => u.Role, f => f.PickRandom<UserRole>())
            .RuleFor(u => u.BloodGroup, f => f.PickRandom<BloodGroup>())
            .RuleFor(u => u.Mobile, f => f.Phone.PhoneNumber())
            .RuleFor(u => u.Address, f => GenerateAddress());

        return userFaker.Generate(count);
    }

    private static Address GenerateAddress()
    {
        var addressFaker = new Faker<Address>()
            .RuleFor(a => a.StreetAddress, f => f.Address.StreetAddress())
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.State, f => f.Address.State())
            .RuleFor(a => a.Country, f => f.Address.Country());

        return addressFaker.Generate();
    }

    private static List<Transaction> GenerateTransactions(int count, List<User> users)
    {
        var transactionFaker = new Faker<Transaction>()
            .RuleFor(t => t.DonatedUserId, f => f.PickRandom(users).ID)
            .RuleFor(t => t.Amount, f => (f.Random.Number(100, 600) / 10) * 10)
            .RuleFor(t => t.CreatedUserId, f => f.PickRandom(users).ID)
            .RuleFor(t => t.Type, f => f.PickRandom<TransactionType>());

        var transactions = transactionFaker.Generate(count);

        var depositSum = transactions.Where(x => x.Type == TransactionType.Deposit).Sum(x => x.Amount);
        var WithdrawalSum = transactions.Where(x => x.Type == TransactionType.Withdrawal).Sum(x => x.Amount);

        if (depositSum < WithdrawalSum)
        {
            int diff = WithdrawalSum - depositSum;
            var withdrawals = transactions.Where(x => x.Type == TransactionType.Withdrawal);

            int unit = diff / withdrawals.Count();
            foreach (var withdrawal in withdrawals)
            {
                withdrawal.Amount -= unit;
            }
        }

        return transactions;
    }

    public static void FakeData(this IApplicationBuilder applicationBuilder)
    {
        var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<Context>() ?? throw new Exception("Cannot fetch 'Context' class.");

        Console.WriteLine("--> Check if data exists...");
        if (context.Users.Any()) return;

        Console.WriteLine("--> Faking data...");

        var users = Seeder.GenerateUsers(500);
        context.Users.AddRange(users);

        var transactions = Seeder.GenerateTransactions(10000, users);
        context.Transactions.AddRange(transactions);

        context.SaveChanges();
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }
}