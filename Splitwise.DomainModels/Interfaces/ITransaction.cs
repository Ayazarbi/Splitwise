using System.Collections.Generic;

public interface ITransaction{

    public Transaction Add(TransactionModel transaction);

    public IEnumerable<Transaction> GetUsertransactions(string Useid);


}