using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Address> Address => Set<Address>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
}