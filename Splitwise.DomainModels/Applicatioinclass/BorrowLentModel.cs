public class BorrowLentModel{


    public string BorrowerId { get; set; }

    public string LenterId { get; set; }
    public Applicationuser Borrower { get; set; }
    public Applicationuser Lenter{get;set;}

    public double Amount{get; set;}
}