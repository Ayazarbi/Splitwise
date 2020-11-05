import { Component, OnInit } from '@angular/core';

import { IGroup } from 'src/Models/IGroup';
import { IGroupModel } from 'src/Models/IGroupModel';

@Component({
  selector: 'app-add-group',
  templateUrl: './add-group.component.html',
  styleUrls: ['./add-group.component.css']
})
export class AddGroupComponent implements OnInit {
 
  Memberemail:string;
  Group:IGroupModel={Expense:[],Members:[],MembersId:[],group:{title:"",amount:0,createdby:{balance:0,email:"",mobilenumber:"",password:"",userId:"",username:""},groupId:0,creatorIdId:"",date:""}}
  constructor() { }

  ngOnInit(): void {
  }

  Addmember(email:string){

    this.Group.MembersId.push(email);
    console.log(this.Group.MembersId);
  }

  Removeitem(item:string){
       var index= this.Group.MembersId.indexOf(item);
       this.Group.MembersId.splice(index,1)
  }

}
