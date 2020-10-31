using System.ComponentModel.DataAnnotations.Schema;

public class TransactionModel{
     public string PayerId { get; set; }


    public string PayeeId { get; set; }


    public double PaidAmount { get; set; }

    public int SettelementId { get; set; }  


}
