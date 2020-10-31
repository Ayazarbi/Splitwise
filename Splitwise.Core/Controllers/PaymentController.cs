using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class PaymentController:Controller{
    private readonly ITransaction paymentRepository;

    public PaymentController(ITransaction paymentRepository)
    {
        this.paymentRepository = paymentRepository;
    }

    [HttpPost]
    public ActionResult Add(TransactionModel transaction){

    return Ok(paymentRepository.Add(transaction));
        
    }

    [Route("Test")]
    [HttpGet]
    public ActionResult Test(){

        return Ok(new {Ok="ok"});
    }

}