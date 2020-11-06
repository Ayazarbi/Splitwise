import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';

import { IGroup } from 'src/Models/IGroup';
import { IGroupModel } from 'src/Models/IGroupModel';
import { Splitwise } from '../Services/SplitWiseApi';

@Component({
  selector: 'app-add-group',
  templateUrl: './add-group.component.html',
  styleUrls: ['./add-group.component.css']
})
export class AddGroupComponent implements OnInit {
 
  Friends:Splitwise.UserModel[];
  Title:string;
  Date:Date;
  Members:Splitwise.UserModel[];
  Memberemail:string;
  Amount:number=0.0;
Group:Splitwise.GroupModel={expenses:[],group:{amount:0,createdby:null,creatorIdId:"aa54591c-4da7-424f-8a34-ded81b2063fc",date:"",groupId:0,title:"",init:null,toJSON:null},members:[],membersId:[],init:null,toJSON:null}
  constructor(private friendsservice:Splitwise.FriendsClient,
              private groupservice:Splitwise.GroupClient) { }

  ngOnInit(): void {

    this.friendsservice.getfriend("aa54591c-4da7-424f-8a34-ded81b2063fc").subscribe(x=>this.Friends=x);


  }

  Addmember(email:string){

    this.Group.membersId.push(email)
    console.log(this.Group.membersId);
    
  }

  Removeitem(item:string){
       var index= this.Group.membersId.indexOf(item);
       this.Group.membersId.splice(index,1)
  }

  Addgroup(){
this.Group.group.amount=this.Amount;
this.Group.group.title=this.Title;
this.Group.group.date=this.Date.toString();
this.Group.group.creatorIdId="aa54591c-4da7-424f-8a34-ded81b2063fc";

this.groupservice.add(this.Group).subscribe(x=>console.log(x),
err=>console.log(err));

console.log(this.Group);

  }

}
