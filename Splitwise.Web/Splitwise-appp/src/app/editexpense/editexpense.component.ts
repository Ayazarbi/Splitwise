import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SplitWise } from '../Services/SplitWiseApi';

@Component({
  selector: 'app-editexpense',
  templateUrl: './editexpense.component.html',
  styleUrls: ['./editexpense.component.css']
})
export class EditexpenseComponent implements OnInit {

  
  Userid:string=localStorage.getItem("id");
  Date:string="";
  title:string="";
  Groupid:number=0;
  Amount:string;"";
  Splittype:string="";
  Shares:SplitWise.Share[]=[];
  Payers:SplitWise.PayerModel[]=[];
  Groups:SplitWise.Group[]=[];
  friends:SplitWise.Applicationuser[]=[];
  Members:SplitWise.Applicationuser[]=[];
  Equally:string="";
  percentage:string="";
  Memberemail:string="";
  Payeremail:string="";
  Paidamount:number=0;
  Smemberemail:string="";
  Membershare:number=0;
  model:SplitWise.ExpenseModel={
    expense:{
      init:null,
      toJSON:null,


    },payers:[],shares:[],init:null,toJSON:null
  };
  creator:SplitWise.Applicationuser;
  Id:number;
  constructor(private expenseservice:SplitWise.ExpenseClient,
              private friendservice:SplitWise.FriendsClient,
              private groupservice:SplitWise.GroupClient,
              private userservice:SplitWise.AccountClient,
              private route:ActivatedRoute,
              private router:Router) { }

  ngOnInit(): void {
    this.Id=+this.route.snapshot.paramMap.get("id");
    
    this.friendservice.getfriend(localStorage.getItem("id")).subscribe(x=>this.friends=x);
   this.groupservice.getallgroups().subscribe(x=>this.Groups=x);
   
    this.expenseservice.getexpense(this.Id).subscribe(x=>{
      this.title=x.expense.title;
      this.Date=x.expense.date;
      this.Amount=x.expense.amount.toString();
      this.Splittype=x.expense.splitType;
      this.Shares=x.shares;
      this.Payers=x.payers;
      this.Groupid=x.expense.groupId;
      this.creator=x.expense.user;
      for (var key in x.payers) {
        
          this.Members.push(x.payers[key].payer);
      }

      

      

    });

    this.userservice.getuser(localStorage.getItem("id")).subscribe(x=>{
      this.friends.push(x.user);
    })

    this.groupservice.getGroupExpenses(this.Id).subscribe(x=>{
     
    })
  }

  Addmember(email:string){

    var member=this.friends.find(x=>x.id==email);
    this.Members.push(member);
    
  }
  Removeitem(item){
        let index=this.Members.findIndex(x=>x.id==item);
      this.Members.splice(index,1);
      let index1=this.Payers.findIndex(x=>x.payer.id==item);
    this.Payers.splice(index1,1);
    let index2=this.Shares.findIndex(x=>x.userId==item);
    this.Shares.splice(index2,1);

  }

  Addpayer(email:string,amount){

    console.log(email)
    
   var friend=this.friends?.find(x=>x.email==email);
   console.log(friend);
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

    let index=this.Shares.findIndex(x=>x.shareId==item);
    this.Shares.splice(index,1);
  }

  Addexpense(){

    if(this.Splittype=="Equally"){
      this.Shares=[];
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

    this.model.expense.splitType=this.Splittype;
    this.model.expense.userId=localStorage.getItem("id");
    this.model.expense.group=null;
    this.model.expense.groupId= this.Groupid ;
    this.model.expense.amount=parseInt(this.Amount);
    this.model.payers=this.Payers;

    this.model.shares=this.Shares;
    this.model.expense.date=this.Date.toString();
    this.model.expense.title=this.title;
    for (const key in this.model.shares) {
            this.model.shares[key].user=null;
    }

    console.log(this.model);

       this.expenseservice.edit(this.Id,this.model).subscribe(x=>{console.log(x)
        this.router.navigate(["/splitwise/myexpenses"])
      });


}
} 
