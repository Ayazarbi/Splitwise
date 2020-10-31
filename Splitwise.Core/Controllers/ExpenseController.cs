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
public ActionResult Add(ExpenseModel expenseModel){

return Ok(expenseRepository.AddExpense(expenseModel));

}

[HttpGet("{id}")]
public async Task<ActionResult> Getexpense( int id){

return Ok(await expenseRepository.GetExpense(id));

}

[HttpPut("{id}")]
public ActionResult Edit(int id,ExpenseModel expenseModel){

return Ok(expenseRepository.EditExpense(id,expenseModel));

}

[HttpDelete("{id}")]
public ActionResult delete(int id){

return Ok(expenseRepository.DeleteExpense(id));

}

[HttpGet("Calculation/{id}")]
public async Task<ActionResult> Getcalculation(int id){

    return Ok(await expenseRepository.GetExpeneseCalculations(id));
}

[Route("Test")]
    [HttpGet]
    public ActionResult Test(){

        return Ok(new {Ok="ok"});
    }



}