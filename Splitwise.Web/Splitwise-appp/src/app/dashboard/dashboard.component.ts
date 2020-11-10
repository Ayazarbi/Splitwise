import { Component, OnInit } from '@angular/core';
import { IUserModel } from 'src/Models/IUserModel';
import { SplitWise } from '../Services/SplitWiseApi';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  Users:any;
  Friendemail:string="";
  Friends:SplitWise.Applicationuser[];
  model:SplitWise.UserModel;

  //  model:IUserModel={
//    activities:[{activityId:1,activitydata:"Useradded",date:"date",user:null,userId:"ayaz"}],
//    epenses:[ {amount:0,date:"",expenseId:
//    1,group:null,groupId:1,splitType:"equally",title:"title",user:null,userId:"11"},
//    {amount:0,date:"",expenseId:
//    1,group:null,groupId:1,splitType:"equally",title:"title",user:null,userId:"11"},
// ],
//    groups:[ {amount:0,createdby:null,creatorIdId:"1",date:"",groupId:1,title:"Manalitrip"}],
//    owesfrom:[{amount:10,payerId:"moin",payer:null}],
//    owesto:[{amount:10,payerId:"moin",payer:null}],
//    transactions:[{paidAmount:100,payee:null,payeeId:"moin",payer:null,payerId:"Ayaz",settelementId:1,transactionId:1}],
//    user:{balance:0,email:"ayaz@gmail.com",mobilenumber:"1234567890",password:"1234567890",userId:"12345",username:"Ayaz"},
//    Friends:[{balance:0,email:"moin@gmail.com",mobilenumber:"",password:"",userId:"",username:"Moin"}]

//   }
  constructor(private service:SplitWise.AccountClient,
              private friendservice:SplitWise.FriendsClient,
              private Userservice:SplitWise.UserClient,) { }

  ngOnInit(): void {
    
    this.service.getuser(localStorage.getItem("id")).subscribe(x=>{
      this.model=x;
      console.log(x);

    },

    err=>{
      console.log(err);
    }
    );
    this.friendservice.getfriend(localStorage.getItem("id")).subscribe(x=>this.Friends=x);
    this.Userservice.getallUsers().subscribe(x=>this.Users=x)
 
  }

  async Addfriend(email){

    var friend:SplitWise.Friend={
      init:null,
      toJSON:null,
      frndId:email,
      userId:localStorage.getItem("id"),

    }

   await this.friendservice.add(friend).subscribe(x=>console.log(x))
    alert("friend added successfully kindly refresh window to reflect changes")

  }

  Deletefriend(friendid){
  this.friendservice.delete(localStorage.getItem("id"),friendid).subscribe(x=>{
    alert(x.email +"has been removed from your friend list");
  })
  }

}
