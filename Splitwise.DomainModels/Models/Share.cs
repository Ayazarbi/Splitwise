using System.ComponentModel.DataAnnotations.Schema;

public class Share{

    public int ShareId { get; set; }  
    

    [ForeignKey("Expense")]
    public int ExpenseId { get; set; }

    public Expense Expense { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; }

    public Applicationuser User{get; set;}

    public double SharePercentage { get; set; }

    public double ShareAmount  { get; set; }

} 