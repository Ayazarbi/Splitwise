using System.Collections.Generic;

public class UserModel{

    public Applicationuser User { get; set; }

    public List<Expense> Expenses { get; set; }

    public List<Group> Groups { get; set; }

    public List<PayerModel> Owesfrom{get;set;}

    public List<PayerModel> Owsto { get; set; }

    public List<Activity> Activities { get; set; }

    public List<Transaction> Transactions { get; set; }
}
