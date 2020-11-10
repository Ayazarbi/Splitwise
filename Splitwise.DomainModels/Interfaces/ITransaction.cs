using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITransaction{

    public Transaction Add(TransactionModel transaction);

    public  Task<List<Transaction>> GetUsertransactionsAsync(string Useid);


}