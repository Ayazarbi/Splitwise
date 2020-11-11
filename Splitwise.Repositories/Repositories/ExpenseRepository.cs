using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

public class ExpenseRepository:IExpense{
    private readonly AppdbContext context;

    public UserManager<Applicationuser> UserManager { get; }

    public ExpenseRepository(AppdbContext context,UserManager<Applicationuser> userManager)
    {
        this.context = context;
        UserManager = userManager;
    }

    public Expense AddExpense(ExpenseModel expenseModel){

        this.context.Expenses.Add(expenseModel.Expense);
        context.SaveChanges();
        
        if(expenseModel.Expense
        .GroupId!=null){
        GroupExpense groupExpense=new GroupExpense(){
            ExpenseId=expenseModel.Expense.ExpenseId,
            GroupId= expenseModel.Expense.GroupId,
        };
            context.GroupsofExpenses.Add(groupExpense);
            context.SaveChanges();
        }
        
    
        foreach (var item in expenseModel.Shares)
        {
            item.ExpenseId=expenseModel.Expense.ExpenseId;
            context.Shareinfo.Add(item);
            context.SaveChanges();
        }
        foreach (var item in expenseModel.Payers)
        {
            var share=GetShare(item.PayerId,expenseModel.Expense.ExpenseId);
            
            Expenseinfo expenseinfo= new Expenseinfo();
            expenseinfo.ExpenseId=expenseModel.Expense.ExpenseId;
            expenseinfo.PaidAmouunt=item.Amount;
            expenseinfo.UserId=item.PayerId;
            expenseinfo.ShareId=share.ShareId;
            expenseinfo.shareamount=share.ShareAmount;
            if((share.ShareAmount-item.Amount)>=0){
            expenseinfo.BorrowedAmout=share.ShareAmount-item.Amount;
            expenseinfo.LentedAmout=0;
            }
            else{
                expenseinfo.LentedAmout=item.Amount-share.ShareAmount;
            }

            context.Expensesinfo.Add(expenseinfo);
            context.SaveChanges();


        }

        var expenseinfolist=context.Expensesinfo.ToList().Where(x=>x.ExpenseId==expenseModel.Expense.ExpenseId);
        foreach (var item in expenseinfolist)
        {
             var LentedAmout=item.LentedAmout;
             if(LentedAmout>0){
             
                foreach (var subitem in expenseinfolist)
                {
                    var BorrowedAmout=subitem.BorrowedAmout;
                    
                    if(LentedAmout<0){
                        break;
                    }
                    if(item.UserId==subitem.UserId){
                        continue;
                    }
                    
                    if(BorrowedAmout>0){

                        Settelement settelement=new Settelement();
                        settelement.ExpenseId=expenseModel.Expense.ExpenseId;
                        settelement.BorrowerId=subitem.UserId;
                        settelement.LenterId=item.UserId;
                        settelement.GroupId=expenseModel.Expense.GroupId;
                        if(LentedAmout>=BorrowedAmout){
                            settelement.SettelementAmount=BorrowedAmout;
                            LentedAmout=LentedAmout-BorrowedAmout;
                            BorrowedAmout=0;
                        }
                        if(LentedAmout<BorrowedAmout){
                            settelement.SettelementAmount=LentedAmout;
                            LentedAmout=0;
                            BorrowedAmout=BorrowedAmout-LentedAmout;
                            
                        }

                        context.Settelements.Add(settelement);
                        context.SaveChanges();
                        
                    }

                }
             }
        }

        var activity=new Activity(){
            Activitydata="You"+expenseModel.Expense.Title+"Added",
            Date=DateTime.Now.ToString(),
            UserId=expenseModel.Expense.UserId,
            };

            context.Activities.Add(activity);
            context.SaveChanges();



        return expenseModel.Expense;
        
    }

    

    
    

    private Share GetShare(string id, int expenseId)
    {
        var shareinfo=context.Shareinfo.FirstOrDefault(x=>x.ExpenseId==expenseId && x.UserId==id);
        return shareinfo;
    }

    public async Task<ExpenseModel> GetExpense(int id){
        ExpenseModel model=new ExpenseModel();
        List<PayerModel> payers=new List<PayerModel>();
        var expense=context.Expenses.ToList().FirstOrDefault(x=>x.ExpenseId==id);
        var expenseinfolist=context.Expensesinfo.ToList().Where(x=>x.ExpenseId==id);
        var sharelist=context.Shareinfo.ToList().Where(x=>x.ExpenseId==id);
            foreach (var item in expenseinfolist)
            {
                PayerModel payerModel=new PayerModel();
                var user=await UserManager.FindByIdAsync(item.UserId);
                payerModel.Payer=user;
                payerModel.Amount=item.PaidAmouunt;
                payers.Add(payerModel);
                
            }
        

        foreach (var item in sharelist)
        {
                var user=await UserManager.FindByIdAsync(item.UserId);
                item.User=user; 
        }

        model.Shares=sharelist.ToList();
        model.Expense=expense;
        model.Payers=payers;


        return model;

    }

    public ExpenseModel EditExpense(int id,ExpenseModel expenseModel){
        
        var oldexpense=context.Expenses.FirstOrDefault(x=>x.ExpenseId==id);
        oldexpense.SplitType=expenseModel.Expense.SplitType;
        oldexpense.Amount=expenseModel.Expense.Amount;
        oldexpense.Date=expenseModel.Expense.Date;
        oldexpense.GroupId=expenseModel.Expense.GroupId;
        oldexpense.Title=expenseModel.Expense.Title;
        oldexpense.UserId=expenseModel.Expense.UserId;
       
        var expense=context.Expenses.Attach(oldexpense);
        expense.State=Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        
        
        
        var expensesinfo=context.Expensesinfo.Where(x=>x.ExpenseId==id);
        context.Expensesinfo.RemoveRange(expensesinfo);
        var Settelements=context.Settelements.Where(x=>x.ExpenseId==id);
        context.Settelements.RemoveRange(Settelements);
        var shareinfo=context.Shareinfo.Where(x=>x.ExpenseId==id);
        context.Shareinfo.RemoveRange(shareinfo);

        foreach (var item in expenseModel.Shares)
        {
            item.ExpenseId=oldexpense.ExpenseId;
            context.Shareinfo.Add(item);
            context.SaveChanges();
        }
        foreach (var item in expenseModel.Payers)
        {
            var share=GetShare(item.Payer.Id,oldexpense.ExpenseId);
            
            Expenseinfo expenseinfo= new Expenseinfo();
            expenseinfo.ExpenseId=oldexpense.ExpenseId;
            expenseinfo.PaidAmouunt=item.Amount;
            expenseinfo.UserId=item.Payer.Id;
            expenseinfo.ShareId=share.ShareId;
            expenseinfo.shareamount=share.ShareAmount;
            if((share.ShareAmount-item.Amount)>=0){
            expenseinfo.BorrowedAmout=share.ShareAmount-item.Amount;
            expenseinfo.LentedAmout=0;
            }
            else{
                expenseinfo.LentedAmout=item.Amount-share.ShareAmount;
            }

            context.Expensesinfo.Add(expenseinfo);
            context.SaveChanges();


        }

        var expenseinfolist=context.Expensesinfo.ToList().Where(x=>x.ExpenseId==oldexpense.ExpenseId);
        foreach (var item in expenseinfolist)
        {
             if(item.LentedAmout>0){
             
                foreach (var subitem in expenseinfolist)
                {
                    if(item.LentedAmout<0){
                        break;
                    }
                    if(item.UserId==subitem.UserId){
                        continue;
                    }
                    
                    if(subitem.BorrowedAmout>0){

                        Settelement settelement=new Settelement();
                        settelement.ExpenseId=oldexpense.ExpenseId;
                        settelement.BorrowerId=subitem.UserId;
                        settelement.LenterId=item.UserId;
                        settelement.GroupId=expenseModel.Expense.GroupId;
                        if(item.LentedAmout>=subitem.BorrowedAmout){
                            settelement.SettelementAmount=subitem.BorrowedAmout;
                            item.LentedAmout=item.LentedAmout-subitem.BorrowedAmout;
                            subitem.BorrowedAmout=0;
                        }
                        if(item.LentedAmout<subitem.BorrowedAmout){
                            settelement.SettelementAmount=item.LentedAmout;
                            item.LentedAmout=0;
                            subitem.BorrowedAmout=subitem.BorrowedAmout-item.LentedAmout;
                            
                        }

                        context.Settelements.Add(settelement);
                        context.SaveChanges();
                        
                    }

                }
             }
        }

        var activity=new Activity(){
            Activitydata="You"+expenseModel.Expense.Title+"Edited",
            Date=DateTime.Now.ToString(),
            UserId=expenseModel.Expense.UserId,
            };

            context.Activities.Add(activity);
            context.SaveChanges();


        return expenseModel;
        
    }





    

    public List<Expense> GetUserExpense(string Userd){

       var Userexpense=from user in context.Expensesinfo
                        join expense in context.Expenses on user.ExpenseId equals expense.ExpenseId
                        where user.UserId==Userd
                        select expense;

        return Userexpense.ToList();

    }

    public Expense DeleteExpense(int id){

        var expense=context.Expenses.FirstOrDefault(x=>x.ExpenseId==id);
        context.Expenses.Remove(expense);
        var expenses=context.Expenses.Where(x=>x.ExpenseId==id);
        context.Expenses.RemoveRange(expenses);
        var expenseinfos=context.Expensesinfo.Where(x=>x.ExpenseId==id);
        context.Expensesinfo.RemoveRange(expenseinfos);
        var groupofexpenses=context.GroupsofExpenses.Where(x=>x.ExpenseId==id);
        context.GroupsofExpenses.RemoveRange(groupofexpenses);
        var settelements=context.Settelements.Where(x=>x.ExpenseId==id);
        context.Settelements.RemoveRange(settelements);

        context.SaveChanges();
        

        var activity=new Activity(){
            Activitydata="You Deleted"+expense.Title,
            Date=DateTime.Now.ToString(),
            UserId=expense.UserId,
            };

            context.Activities.Add(activity);
            context.SaveChanges();

        
        return expense;
    }

    public async Task<List<BorrowLentModel>> GetExpeneseCalculations(int id){

        List<BorrowLentModel> borrowLentModels=new List<BorrowLentModel>();
        var settlements=context.Settelements.ToList().Where(x=>x.ExpenseId==id);
        foreach (var item in settlements)
        {
            BorrowLentModel borrowLentModel=new BorrowLentModel();
            var borrower= await UserManager.FindByIdAsync(item.BorrowerId);
            var lenter=await  UserManager.FindByIdAsync(item.LenterId);

            borrowLentModel.Borrower=borrower;
            borrowLentModel.LenterId=item.LenterId;
            borrowLentModel.Lenter=lenter;
            borrowLentModel.BorrowerId=item.BorrowerId;
            borrowLentModel.Amount=item.SettelementAmount;
            
            borrowLentModels.Add(borrowLentModel);
        }

        return borrowLentModels;
    }


}