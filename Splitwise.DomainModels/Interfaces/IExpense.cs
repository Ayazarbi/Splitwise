using System.Collections.Generic;
using System.Threading.Tasks;

public interface IExpense{

public Expense AddExpense(ExpenseModel expenseModel);
public Task<ExpenseModel> GetExpense(int id);

public ExpenseModel EditExpense(int id,ExpenseModel expenseModel);

public List<Expense> GetUserExpense(string Userd);
public Expense DeleteExpense(int id);

public Task<List<Settelement>> GetExpeneseCalculations(int id);






}