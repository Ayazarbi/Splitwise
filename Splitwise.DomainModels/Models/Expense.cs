using System.ComponentModel.DataAnnotations.Schema;

public class Expense{

   
    public  int GroupId{get; set;}

    public Group Group{get; set;}
    public int ExpenseId { get; set; }
    public string Title { get; set; }
    public string SplitType { get; set; }
    public double Amount { get; set; }
    public string Date { get; set; }
    
    [ForeignKey("User")]
    public string UserId { get; set; }

    public Applicationuser User{get; set;}
}