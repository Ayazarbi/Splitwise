using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]")]
public class ExpenseController:Controller{
    private readonly IExpense expenseRepository;

    public ExpenseController(IExpense expenseRepository){
        this.expenseRepository = expenseRepository;
    }


[HttpPost]
public ActionResult<Expense> Add(ExpenseModel expenseModel){

return Ok(expenseRepository.AddExpense(expenseModel));

}

[HttpGet("{id}")]
public async Task<ActionResult<ExpenseModel>> Getexpense( int id){

return Ok(await expenseRepository.GetExpense(id));

}

[HttpPut("{id}")]
public ActionResult<ExpenseModel> Edit(int id,ExpenseModel expenseModel){

return Ok(expenseRepository.EditExpense(id,expenseModel));

}

[HttpDelete("{id}")]
public ActionResult<Expense> delete(int id){

return Ok(expenseRepository.DeleteExpense(id));

}

[HttpGet("Calculation/{id}")]
public async Task<ActionResult<List<BorrowLentModel>>> Getcalculation(int id){

    return Ok(await expenseRepository.GetExpeneseCalculations(id));
}

[Route("Test")]
    [HttpGet]
    public ActionResult Test(){

        return Ok(new {Ok="ok"});
    }



}