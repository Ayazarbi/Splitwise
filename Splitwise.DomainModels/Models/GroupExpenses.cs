using System.ComponentModel.DataAnnotations.Schema;

public class GroupExpense{

    
    public int GroupExpenseId { get; set; }
    
    [ForeignKey("Group")]
    public int GroupId { get; set; }
    public Group Group { get; set; }
    
     [ForeignKey("Expense")]
     public int ExpenseId { get; set; }

     public Expense Expense{get;set;}
      
}