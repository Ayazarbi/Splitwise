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

    async Task<UserModel> IAccount.GetUserinfo(string id)
    {

        List<PayerModel> OF=new List<PayerModel>();
        List<PayerModel> OT=new List<PayerModel>();
        UserModel  userModel=new UserModel();
        userModel.User=await userManager.FindByIdAsync(id);
        userModel.Expenses=expenserepo.GetUserExpense(id);
        userModel.Groups=grouprepo.GetUserGroups(id).ToList();
        userModel.Activities=activityrepo.GetUserActivities(id).ToList();
        userModel.Transactions=await transactionrepo.GetUsertransactionsAsync(id);
        

        var settelements=context.Settelements.ToList().Where(x=>(x.BorrowerId==id || x.LenterId==id) && x.SettelementAmount > 0);

        var OWESTO=(from item in settelements
                    where item.BorrowerId==id
                    group item by item.LenterId into Owesto
                    orderby Owesto.Key
                    select new {
                        
                        name=Owesto.Key,
                        amount=Owesto.Sum(x=>x.SettelementAmount)


                    }).ToList();

        var OWESFROM=(from item in settelements
                    where item.LenterId==id
                    group item by item.BorrowerId into Owesfrom
                    orderby Owesfrom.Key
                    select new {
                        
                        name=Owesfrom.Key,
                        amount=Owesfrom.Sum(x=>x.SettelementAmount)


                    }).ToList();


        

        foreach (var item in OWESTO.ToList())
        {
            var user=await userManager.FindByIdAsync(item.name);
            PayerModel pmm=new PayerModel(){
                Amount=0,
                Payer=user,
                PayerId=user.Id
            };
            foreach (var subitem in OWESFROM.ToList())
            {
                if(item.name==subitem.name){
                    var result=item.amount-subitem.amount;
                    if(result>0){
                        
                        pmm.Amount=result;
                        OT.Add(pmm);
                        OWESTO.Remove(item);
                        OWESFROM.Remove(subitem);
    
                    }
                    else if(result<0){

                        pmm.Amount=result*-1;
                        OF.Add(pmm);
                        OWESTO.Remove(item);
                        OWESFROM.Remove(subitem);
                    }
                    else if(result == 0)
                    {
                        OWESTO.Remove(item);
                        OWESFROM.Remove(subitem);
                    }
                    else{
                        continue;
                    }


                }
               
            }
        }

        if (OWESTO.Count == 0)
        {

            foreach (var item in OWESFROM.ToList())
            {
                var user = await userManager.FindByIdAsync(item.name);

                PayerModel pm = new PayerModel()
                {
                    Amount = item.amount,
                    Payer = user,
                    PayerId = user.Id,
                };

                OF.Add(pm);

            }

        }
        if (OWESFROM.Count == 0)
        {

            foreach (var item in OWESTO.ToList())
            {
                var user = await userManager.FindByIdAsync(item.name);

                PayerModel pm = new PayerModel()
                {
                    Amount = item.amount,
                    Payer = user,
                    PayerId = user.Id,
                };

                OT.Add(pm);

            }


        }

        if (OWESFROM.ToList().Count() > 0)
        {
            foreach (var item in OWESTO)
            {
                var user = await userManager.FindByIdAsync(item.name);
                PayerModel pmm = new PayerModel()
                {
                    Amount = item.amount,
                    Payer = user,
                    PayerId = user.Id
                };

                OT.Add(pmm);
            }
        }

        if (OWESTO.ToList().Count() > 0)
        {
            foreach (var item in OWESFROM)
            {
                var user = await userManager.FindByIdAsync(item.name);
                PayerModel pmm = new PayerModel()
                {
                    Amount = item.amount,
                    Payer = user,
                    PayerId = user.Id
                };

                OF.Add(pmm);
            }

        }



        userModel.Owesfrom=OF.ToList();
      userModel.Owsto=OT.ToList();
    return userModel;
     
    
    }

    public IEnumerable<Applicationuser> Getalluser(){
         
         return context.Users.ToList();
    }




}

