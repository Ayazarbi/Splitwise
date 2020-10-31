using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccount accountRepository;
    private readonly UserManager<Applicationuser> userManager;

    public AccountController(IAccount accountRepository,UserManager<Applicationuser> userManager){
        this.accountRepository = accountRepository;
        this.userManager = userManager;
    }

    [Route("Register")]
    [HttpPost]
    public async Task<ActionResult> Register(SignupModel model){

        var user=new Applicationuser(){
            Email=model.Email,
            Balacnce=model.Balance,
            UserName=model.Username,
        };

        var result=await userManager.CreateAsync(user,model.password);
        if(result.Succeeded){

            return Ok(new {result=result});
        }

        return BadRequest("Something went wrong");
    }

    [Route("Login")]
    [HttpPost]
    public async Task<ActionResult> Login(LoginModel model){

    var key = Encoding.UTF8.GetBytes("1234567890123456");

      if(await accountRepository.Login(model)){

          var tokendescriptor=new SecurityTokenDescriptor{

                        Subject= new ClaimsIdentity(new Claim[]{
                            new Claim("Email", model.Email),
                            
                            

                        }),
                        Expires=DateTime.UtcNow.AddDays(1),
                            SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenhandler=new JwtSecurityTokenHandler();
                    var Securitytoken=tokenhandler.CreateToken(tokendescriptor);
                    var token=tokenhandler.WriteToken(Securitytoken);
        return Ok(new{token=token});

      }
      return BadRequest("Invalid Credetials");
    }

    [HttpPut("{id}")]

    public ActionResult Chanagepassword(string id,ResetpasswordModel model){

        return Ok(accountRepository.ResetPassword(id,model));
    }

    [HttpPost]
    public ActionResult Logout(){

        return Ok();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult> getuser(string id){

        return Ok(await accountRepository.GetUserinfo(id));
    }

}