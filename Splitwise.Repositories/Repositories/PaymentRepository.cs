using System;
using System.Collections.Generic;
using System.Linq;

public class PaymentRepository:ITransaction{
    private readonly AppdbContext context;
   
    public  PaymentRepository(AppdbContext context)
    {
        this.context = context;
    }


    public Transaction Add(TransactionModel transactionn){

            var transaction=new Transaction(){
                PaidAmount=transactionn.PaidAmount,
                PayeeId=transactionn.PayeeId,
                PayerId=transactionn.PayerId,
                SettelementId=transactionn.SettelementId,
            };
        
        
        context.Payments.Add(transaction);
        context.SaveChanges();

       return UpdateSettelement(transaction);

     }

    private Transaction UpdateSettelement(Transaction transaction)
    {
        var Settelement=context.Settelements.ToList().FirstOrDefault(x=>x.SettelementId==transaction.SettelementId);
        Settelement.SettelementAmount=Settelement.SettelementAmount-transaction.PaidAmount;
        

        var changed= context.Settelements.Attach(Settelement);
        changed.State=Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();

         var activity=new Activity();
       activity.Activitydata="You Paid"+transaction.PaidAmount+" to "+transaction.PayeeId;
       activity.Date=DateTime.Now.ToString();
       activity.UserId=transaction.PayerId;

       context.Activities.Add(activity);
       context.SaveChanges();
        return transaction;
   
    }

    public IEnumerable<Transaction> GetUsertransactions(string Useid){

        var transactions=context.Payments.ToList().FindAll(x=>x.PayeeId==Useid || x.PayerId==Useid);
        return  transactions;
    }
}