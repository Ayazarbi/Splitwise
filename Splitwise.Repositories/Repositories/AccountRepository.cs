using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

public class AccountRepository:IAccount{
    private readonly AppdbContext context;
    private readonly UserManager<Applicationuser> userManager;
    private readonly IExpense expenserepo;
    private readonly IGroup grouprepo;
    private readonly IActivity activityrepo;
    private readonly ITransaction transactionrepo;

    public AccountRepository(AppdbContext context,UserManager<Applicationuser> userManager,IExpense expenserepo,IGroup grouprepo,IActivity activityrepo,ITransaction transactionrepo){
        this.context = context;
        this.userManager = userManager;
        this.expenserepo = expenserepo;
        this.grouprepo = grouprepo;
        this.activityrepo = activityrepo;
        this.transactionrepo = transactionrepo;
    }


    public async Task<bool> Login(LoginModel model){

        var user=await userManager.FindByEmailAsync(model.Email);

        if(user!=null){
        var result=await userManager.CheckPasswordAsync(user,model.password);
            return result;
        }
        return false;

    }

        public SignupModel Signup(SignupModel model){

        throw new NotImplementedException();

    }

    public Applicationuser ResetPassword(string userid ,ResetpasswordModel model){

        throw new NotImplementedException();

    }

    public async Task<UserModel> GetUserinfo(string id)
    {

        List<PayerModel> Owesfrom=new List<PayerModel>();
        List<PayerModel> Owsto=new List<PayerModel>();
        UserModel  userModel=new UserModel();
        userModel.User=await userManager.FindByIdAsync(id);
        userModel.Expenses=expenserepo.GetUserExpense(id);
        userModel.Groups=grouprepo.GetUserGroups(id).ToList();
        userModel.Activities=activityrepo.GetUserActivities(id).ToList();
        userModel.Transactions=transactionrepo.GetUsertransactions(id).ToList();
        

        var settelements=context.Settelements.ToList().Where(x=>x.BorrowerId==id || x.LenterId==id);

        foreach (var item in settelements)
        {
            if(item.BorrowerId==id){
            var lenter=await userManager.FindByIdAsync(item.LenterId);
             PayerModel payerModel=new PayerModel(){

                 Amount=item.SettelementAmount,
                 Payer=lenter,
                    
             };

             Owsto.Add(payerModel);

            }
             else{
                var borroer=await userManager.FindByIdAsync(item.BorrowerId);
                var payerModell=new PayerModel();
                    payerModell.Payer=borroer;
                    payerModell.Amount=item.SettelementAmount;
                    payerModell.PayerId =borroer.Id;


                Owesfrom.Add(payerModell);
             }

    
        }

    userModel.Owesfrom=Owesfrom;
    userModel.Owsto=Owsto;
    return userModel;
    
    }




}

