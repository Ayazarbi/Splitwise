import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';

import { IGroup } from 'src/Models/IGroup';
import { IGroupModel } from 'src/Models/IGroupModel';
import { SplitWise } from '../Services/SplitWiseApi';

@Component({
  selector: 'app-add-group',
  templateUrl: './add-group.component.html',
  styleUrls: ['./add-group.component.css']
})
export class AddGroupComponent implements OnInit {
 
  User?:SplitWise.Applicationuser;
  Friends:SplitWise.Applicationuser[];
  Title:string;
  Date:Date;
  Members:SplitWise.UserModel[];
  Memberemail:string;
  Amount:number=0.0;
Group:SplitWise.GroupModel={expenses:[],group:{amount:0,createdby:null,creatorIdId:"aa54591c-4da7-424f-8a34-ded81b2063fc",date:"",groupId:0,title:"",init:null,toJSON:null},members:[],membersId:[],init:null,toJSON:null}
  constructor(private friendsservice:SplitWise.FriendsClient,
              private groupservice:SplitWise.GroupClient,
              private Userservice:SplitWise.AccountClient) { }

  ngOnInit(): void {

   this.Userservice.getuser(localStorage.getItem("id")).subscribe( x=>{
    this.User=x.user
    this.Group.members.push(this.User);
   
   }
     );
   
    this.friendsservice.getfriend(localStorage.getItem("id")).subscribe(x=>this.Friends=x);


  }

  async Addmember(email:string){


    var member=await this.Friends.find(x=>x.email==email);
    this.Group.members.push(member);
    this.Group.membersId.push(member?.id)
    console.log(this.Group.membersId);
    
  }

  Removeitem(item:string){
      
   var index1= this.Group.members.findIndex(x=>x.id==item);
   this.Group.members.splice(index1,1);    
   var index= this.Group.membersId.findIndex(x=>x==item);
       this.Group.membersId.splice(index,1)
  }

  Addgroup(){

  
this.Group.group.amount=this.Amount;
this.Group.group.title=this.Title;
this.Group.group.date=this.Date.toString();
this.Group.group.creatorIdId=localStorage.getItem("id");

this.groupservice.add(this.Group).subscribe(x=>console.log(x),
err=>console.log(err));

console.log(this.Group);

  }

}
