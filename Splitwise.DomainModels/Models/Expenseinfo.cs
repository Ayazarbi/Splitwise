using System.ComponentModel.DataAnnotations.Schema;

public class Expenseinfo{

    public int ExpenseinfoId { get; set; }
    [ForeignKey("Expense")]
    public int ExpenseId { get; set; }

    public Expense Expense {get; set;}

    public double PaidAmouunt{get; set;}

    public double BorrowedAmout { get; set; }

    public double LentedAmout { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; }

    public Applicationuser User{get; set;}

    [ForeignKey("Share")]
    public int ShareId{get;set;}
    public Share Share { get; set; }

    public double shareamount{get; set;}
}