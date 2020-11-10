using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

public class PaymentRepository:ITransaction{
    private readonly AppdbContext context;
    private readonly UserManager<Applicationuser> usermanager1;
    private bool usermanager;

    public  PaymentRepository(AppdbContext context,UserManager<Applicationuser> usermanager)
    {
        this.context = context;
        usermanager1 = usermanager;
    }


    public Transaction Add(TransactionModel transactionn){

            var transaction=new Transaction(){
                PaidAmount=transactionn.PaidAmount,
                PayeeId=transactionn.PayeeId,
                PayerId=transactionn.PayerId,
               
            };
        
        var trans=UpdateSettelement(transaction);
        context.Payments.Add(transaction);
        context.SaveChanges();

       return trans;

     }

    private Transaction UpdateSettelement(Transaction transaction)
    {
    
        var Settelements=context.Settelements.Where(x=>x.BorrowerId==transaction.PayerId&&x.LenterId==transaction.PayeeId);

            foreach (var item in Settelements)
            {
                
                
                
                if(transaction.PaidAmount<=0){

                    break;
                }
                else{

                    var result=item.SettelementAmount-transaction.PaidAmount;
                    transaction.PaidAmount-=item.SettelementAmount;
                    if(result>0){
                        item.SettelementAmount=result;

                    }
                    else{
                        item.SettelementAmount=0;
                    }
                    
                    var state=context.Settelements.Attach(item);
                    state.State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                   
                }

                }

                if(transaction.PaidAmount>0){

                    Settelement settelement=new Settelement(){
                        BorrowerId=transaction.PayeeId,
                        LenterId=transaction.PayerId,
                        ExpenseId=null,
                        SettelementAmount=transaction.PaidAmount,
                        GroupId=null,
                    };
                    context.Settelements.Add(settelement);

                    
                }

                 return transaction;

      }   
    //     var Settelement=context.Settelements.ToList().FirstOrDefault(x=>x.SettelementId==transaction.SettelementId);
    //     Settelement.SettelementAmount=Settelement.SettelementAmount-transaction.PaidAmount;
        

    //     var changed= context.Settelements.Attach(Settelement);
    //     changed.State=Microsoft.EntityFrameworkCore.EntityState.Modified;
    //     context.SaveChanges();

    //      var activity=new Activity();
    //    activity.Activitydata="You Paid"+transaction.PaidAmount+" to "+transaction.PayeeId;
    //    activity.Date=DateTime.Now.ToString();
    //    activity.UserId=transaction.PayerId;

    //    context.Activities.Add(activity);
    //    context.SaveChanges();
   
   
    
    

    public async System.Threading.Tasks.Task<List<Transaction>> GetUsertransactionsAsync(string Useid){

        var transactions=context.Payments.ToList().FindAll(x=>x.PayeeId==Useid || x.PayerId==Useid);
        foreach (var item in transactions)
        {
            var user= await usermanager1.FindByIdAsync(item.PayerId);
            item.Payer=user;
            var user2=await usermanager1.FindByIdAsync(item.PayeeId);
            item.Payee=user2;

        }
        
        return  transactions.ToList();
    }
}