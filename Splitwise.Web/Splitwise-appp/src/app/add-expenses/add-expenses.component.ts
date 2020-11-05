import { Component, OnInit } from '@angular/core';
import { IExpenseModel } from 'src/Models/IExpenseModel';
import { IPayerModel } from 'src/Models/IPayerModel';
import { IShare } from 'src/Models/IShare';

@Component({
  selector: 'app-add-expenses',
  templateUrl: './add-expenses.component.html',
  styleUrls: ['./add-expenses.component.css']
})
export class AddExpensesComponent implements OnInit {

  Membersemail:string[]=[];
  Equally:string;
  percentage:string;
  Memberemail:string;
  Payeremail:string;
  Paidamount:number;
  Smemberemail:string;
  Membershare:number;
  model:IExpenseModel={
    expense:{amount:0,date:"",expenseId:0,groupId:0,group:{title:"",amount:0,createdby:{balance:0,email:"",userId:"",mobilenumber:"",password:"",username:""},date:"",creatorIdId:"",groupId:0},title:"",user:{balance:0,email:"",userId:"",mobilenumber:"",password:"",username:""},userId:"",splitType:""},
    payers:[],
    shares:[],
    payersId:[],
  }
  constructor() { }

  ngOnInit(): void {
  }

    Addmember(email:string){

      this.Membersemail.push(email);
      
    }
    Removeitem(item){
         let index=this.Memberemail.indexOf(item);
         this.Membersemail.splice(index,1);
    }

    Addpayer(email:string,amount){

      
     let payer:IPayerModel={amount:amount,payer:{balance:0,email:"",mobilenumber:"",password:"",userId:"",username:""},payerId:email};
    
      this.model.payers.push(payer)



    }

    Removepayer(item){

      let index=this.model.payers.indexOf(item);
      this.model.payers.splice(index,1);
    }
    Addmembershare(email,shareamount){
      let share:IShare={expenseId:0,expesne:{amount:0,date:"",expenseId:0,groupId:0,group:{title:"",amount:0,createdby:{balance:0,email:"",userId:email,mobilenumber:"",password:"",username:""},date:"",creatorIdId:"",groupId:0},title:"",user:{balance:0,email:"",userId:"",mobilenumber:"",password:"",username:""},userId:"",splitType:""},shareAmount:(this.model.expense.amount*shareamount)/100,shareId:null,sharePercentage:shareamount,userId:email,user:{balance:0,email:"",mobilenumber:"",password:"",userId:"",username:""}};
      this.model.shares.push(share);

    }

    Removeshare(item){

      let index=this.model.shares.indexOf(item);
      this.model.shares.splice(index,1);
    }
}
