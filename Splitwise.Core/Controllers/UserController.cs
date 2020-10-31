using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("User")]
public class UserController:Controller{
    private readonly IExpense expenseRepository;
    private readonly IGroup groupRepository;
    private readonly IActivity activityRepository;
    private readonly ITransaction paymentRepository;

    public UserController(
        IExpense expenseRepository,IGroup groupRepository,IActivity activityRepository ,ITransaction paymentRepository
    )
    {
        this.expenseRepository = expenseRepository;
        this.groupRepository = groupRepository;
        this.activityRepository = activityRepository;
        this.paymentRepository = paymentRepository;
    }


    [HttpGet("{id}/Expenses")]
    public ActionResult GetUserExpenses(string id){

            return Ok(expenseRepository.GetUserExpense(id));
    }
    [HttpGet("{id}/Groups")]

    public ActionResult GetUserGroups(string id){

            return Ok(groupRepository.GetUserGroups(id));
    }
    [HttpGet("{id}/Activities")]
    public ActionResult GetUserActivties(string id){

        return Ok(activityRepository.GetUserActivities(id));
    }

    [HttpGet("{id}/Transactions")]
    public ActionResult GetuserTransaction(string id){

        return Ok(paymentRepository.GetUsertransactions(id));
    }


}