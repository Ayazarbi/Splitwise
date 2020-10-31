using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Transaction{

    [Key]
    public int TrasactionId { get; set; }
    
    [ForeignKey("Payer")]
    public string PayerId { get; set; }

    public Applicationuser Payer{get; set;}

    [ForeignKey("Payee")]
    public string PayeeId { get; set; }

    public Applicationuser Payee{get; set;}

    public double PaidAmount { get; set; }

    [ForeignKey("Settelement")]
    public int SettelementId { get; set; }  

    public Settelement Settelement { get; set; }
}