import { Component, OnInit } from '@angular/core';

import { IExpenseModel } from 'src/Models/IExpenseModel';
import { IPayerModel } from 'src/Models/IPayerModel';
import { IShare } from 'src/Models/IShare';
import { SplitWise } from '../Services/SplitWiseApi';

@Component({
  selector: 'app-add-expenses',
  templateUrl: './add-expenses.component.html',
  styleUrls: ['./add-expenses.component.css']
})
export class AddExpensesComponent implements OnInit {

  
  Date:string;
  title:string;
  Groupid:string;
  Amount:string;"";
  Splittype:string="";
  Shares:SplitWise.Share[]=[];
  Payers:SplitWise.PayerModel[]=[];
  Groups:SplitWise.Group[];
  friends:SplitWise.Applicationuser[];
  Members:SplitWise.Applicationuser[]=[];
  Equally:string;
  percentage:string;
  Memberemail:string;
  Payeremail:string;
  Paidamount:number;
  Smemberemail:string;
  Membershare:number;
  User:SplitWise.Applicationuser;
  model:SplitWise.ExpenseModel={
    expense:{amount:0,date:"",expenseId:0,groupId:0,group:{title:"",amount:0,createdby:null,date:"",creatorIdId:"",groupId:0,init:null,toJSON:null},title:"",user:null,userId:"",splitType:"",init:null,toJSON:null},
    payers:[],
    shares:[],
    init:null,
    toJSON:null
  };
  constructor(private friendservice:SplitWise.FriendsClient,
              private groupservice:SplitWise.GroupClient,
              private expenseservice:SplitWise.ExpenseClient,
              private userservice:SplitWise.AccountClient) { }

  ngOnInit(): void {

    this.friendservice.getfriend(localStorage.getItem("id")).subscribe(async x=>{
      this.friends=x;
    });
    this.groupservice.getallgroups().subscribe(x=>{
      this.Groups=x;
    });
    this.userservice.getuser(localStorage.getItem("id")).subscribe(x=>{
      this.User=x.user;
      this.Members.push(this.User);
      this.friends?.push(this.User);
    })
  
  }
    

    Addmember(email:string){

      var member=this.friends.find(x=>x.id==email);
      this.Members.push(member);
      
    }
    Removeitem(item){
          let index=this.Members.findIndex(x=>x.id==item);
        this.Members.splice(index,1);
    }

    Addpayer(email:string,amount){

      
     var friend=this.friends.find(x=>x.email==email);
     var Payer:SplitWise.PayerModel={payer:friend,amount:amount,init:null,toJSON:null,payerId:friend.id}
     this.Payers.push(Payer)
     console.log(this.Payers);



    }

    Removepayer(item){

      let index=this.Payers.findIndex(x=>x.payer.email==item);
      this.Payers.splice(index,1);
    }
    Addmembershare(email,shareamount){
     
      var member=this.friends.find(x=>x.email==email);
      var share:SplitWise.Share={
        expense:null,
        init:null,
        toJSON:null,
        expenseId:0,
        sharePercentage:shareamount,
        shareAmount:(shareamount* parseInt(this.Amount)/100 ),
        shareId:0,
        user:member,
        userId:member.id
      }

      this.Shares.push(share);
      

    }

    Removeshare(item){

      let index=this.model.shares.findIndex(x=>x.userId==item);
      this.model.shares.splice(index,1);
    }

    Addexpense(){
      this.model.expense.splitType=this.Splittype;
      this.model.expense.userId=localStorage.getItem("id");
      this.model.expense.group=null;
      this.model.expense.groupId= parseInt(this.Groupid) ;
      this.model.expense.amount=parseInt(this.Amount);
      this.model.payers=this.Payers;
      if(this.Splittype=="Equally"){
        this.Members.forEach(element => {

          var share:SplitWise.Share={
            expense:null,
            init:null,
            toJSON:null,
            expenseId:0,
            sharePercentage:100/this.Members.length,
            shareAmount:parseInt(this.Amount)/this.Members.length,
            shareId:0,
            user:element,
            userId:element.id
          }
    
          this.Shares.push(share);

          
        });
      }
      this.model.shares=this.Shares;
      this.model.expense.date=this.Date.toString();
      this.model.expense.title=this.title;

      
      for (const key in this.model.shares) {
              this.model.shares[key].user=null;
      }

      this.expenseservice.add(this.model).subscribe(x=>console.log(x));

    }
}
