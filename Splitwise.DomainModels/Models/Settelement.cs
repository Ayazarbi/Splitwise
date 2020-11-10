using System.ComponentModel.DataAnnotations.Schema;

public class Settelement{

    public int SettelementId { get; set; }
    [ForeignKey("Expense")]
    public int? ExpenseId { get; set; }  

    public Expense Expense{get;  set;}

    public double SettelementAmount { get; set; }

    [ForeignKey("Borrower")]
    public string BorrowerId { get; set; }

    public Applicationuser Borrower{get; set;}

    [ForeignKey("Lenter")]
    public string LenterId { get; set; }

    public Applicationuser Lenter{get; set;}

    [ForeignKey("Group")]
    public int? GroupId { get; set; }

    public Group Group{get;set;}
}