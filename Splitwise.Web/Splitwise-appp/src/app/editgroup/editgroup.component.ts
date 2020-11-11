import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MemberModel } from 'src/Models/Membermodel';
import { SplitWise } from '../Services/SplitWiseApi';

@Component({
  selector: 'app-editgroup',
  templateUrl: './editgroup.component.html',
  styleUrls: ['./editgroup.component.css']
})
export class EditgroupComponent implements OnInit {

  Settelements:SplitWise.Settelement[];
  Members:MemberModel[]=[];
  MemberId:string="";
  MemebersId:string[]=[];

  Friends:SplitWise.Applicationuser[];
  Group:SplitWise.GroupModel;
  constructor(private groupservice:SplitWise.GroupClient,
              private Route:ActivatedRoute,
              private friendservice:SplitWise.FriendsClient,
              private router:Router) { }
  Id:number;
  ngOnInit(): void {
   this.Id=+this.Route.snapshot.paramMap.get("id");
   console.log(this.Id);
   this.friendservice.getfriend(localStorage.getItem("id")).subscribe(x=>this.Friends=x)
    this.groupservice.getGroup(this.Id).subscribe(x=>{
      this.Group=x;
  });

  this.groupservice.getcalculations(this.Id).subscribe(x=>{
    this.Settelements=x;
    console.log(x);
    
  })
  
  }

  Addmember(email){
    var member=this.Friends.find(x=>x.id==email);
    this.Group?.members.push(member);
    this.Group?.membersId.push(email);
    console.log(email)
  }
  Removeitem(item){

    let index=this.Group?.members.indexOf(item);
    this.Group?.members.splice(index,1);
    let index2=this.Group?.membersId.indexOf(item.id);
    this.Group?.membersId.splice(index2,1);

  }
  Editgroup(){
    

   
    console.log(this.Group); 
    
       this.groupservice.edit(this.Id,this.Group).subscribe(x=>{
         this.router.navigate(["/splitwise/mygroups"])

       })
  }

  Removeexpense(item){

    var index=this.Group?.expenses.findIndex(x=>x.expenseId==item);
    this.Group?.expenses.splice(index,1);

  }

  

}
