using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppdbContext:IdentityDbContext<Applicationuser>
{

    public AppdbContext(DbContextOptions<AppdbContext> options):base(options){
    
    }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Group> Groups { get; set; }

    public DbSet<Activity> Activities { get; set; }

    public DbSet<Expenseinfo> Expensesinfo { get; set; }

   public DbSet<Friend> Friends { get; set; }

   public DbSet<GroupExpense> GroupsofExpenses { get; set; }

   public DbSet<GroupMembers> GroupMembers { get; set; }

   public DbSet<Share> Shareinfo { get; set; }

   public DbSet<Settelement> Settelements { get; set; }

   public DbSet<Transaction> Payments { get; set; }







}