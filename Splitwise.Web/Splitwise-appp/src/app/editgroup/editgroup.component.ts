import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MemberModel } from 'src/Models/Membermodel';
import { Splitwise } from '../Services/SplitWiseApi';

@Component({
  selector: 'app-editgroup',
  templateUrl: './editgroup.component.html',
  styleUrls: ['./editgroup.component.css']
})
export class EditgroupComponent implements OnInit {

  Members:MemberModel[]=[];
  MemberId:string="";
  MemebersId:string[]=[];

  Friends:Splitwise.Applicationuser[];
  Group:Splitwise.GroupModel;
  constructor(private groupservice:Splitwise.GroupClient,
              private Route:ActivatedRoute,
              private friendservice:Splitwise.FriendsClient) { }
  Id:number;
  ngOnInit(): void {
   this.Id=+this.Route.snapshot.paramMap.get("id");
   console.log(this.Id);
   this.friendservice.getfriend("aa54591c-4da7-424f-8a34-ded81b2063fc").subscribe(x=>this.Friends=x)
    this.groupservice.getGroup(this.Id).subscribe(x=>{
      this.Group=x;
      for (let key in this.Group?.members) {  
            var member:MemberModel={email:this.Group?.members[key]?.email,memberId:this.Group.members[key]?.id};
            this.Members.push(member);  
      }
      
    });
  }

  Addmember(email){
    var member=this.Friends.find(x=>x.id==email);
    this.Group?.members.push(member);
    this.MemebersId.push(email);
    console.log(email)
  }
  Removeitem(item){

    let index=this.Group?.members.indexOf(item);
    this.Group?.members.splice(index,1);

  }
  Editgroup(){

    this.Group.membersId=this.MemebersId;

    console.log(this.Group); 
    
       this.groupservice.edit(this.Id,this.Group).subscribe(x=>console.log(x))
  }

  Removeexpense(item){

    var index=this.Group?.expenses.findIndex(x=>x.expenseId==item);
    this.Group?.expenses.splice(index,1);

  }

}
