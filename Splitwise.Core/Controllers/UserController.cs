using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("User")]
public class UserController:Controller{
    private readonly IExpense expenseRepository;
    private readonly IGroup groupRepository;
    private readonly IActivity activityRepository;
    private readonly ITransaction paymentRepository;
    private readonly IAccount accountrepositpry;

    public UserController(
        IExpense expenseRepository,IGroup groupRepository,IActivity activityRepository ,ITransaction paymentRepository,IAccount accountrepositpry
    )
    {
        this.expenseRepository = expenseRepository;
        this.groupRepository = groupRepository;
        this.activityRepository = activityRepository;
        this.paymentRepository = paymentRepository;
        this.accountrepositpry = accountrepositpry;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Applicationuser>> getallusers(){

            return Ok(accountrepositpry.Getalluser());
    
    }


    [HttpGet("{id}/Expenses")]
    public ActionResult<List<Expense>> GetUserExpenses(string id){

            return Ok(expenseRepository.GetUserExpense(id));
    }
    [HttpGet("{id}/Groups")]

    public ActionResult<IEnumerable<Group>> GetUserGroups(string id){

            return Ok(groupRepository.GetUserGroups(id));
    }
    [HttpGet("{id}/Activities")]
    public ActionResult<IEnumerable<Activity>> GetUserActivties(string id){

        return Ok(activityRepository.GetUserActivities(id));
    }

    [HttpGet("{id}/Transactions")]
    public ActionResult<IEnumerable<Transaction>> GetuserTransaction(string id){

        return Ok(paymentRepository.GetUsertransactionsAsync(id));
    }


}