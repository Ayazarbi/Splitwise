import { Component, OnInit } from '@angular/core';
import { IGroup } from 'src/Models/IGroup';

@Component({
  selector: 'app-my-groups',
  templateUrl: './my-groups.component.html',
  styleUrls: ['./my-groups.component.css']
})
export class MyGroupsComponent implements OnInit {
  groups:IGroup[]=[{
    amount:0,createdby:null,creatorIdId:"1",date:"",groupId:1,title:"Manalitrip"
  }]
  constructor() { }

  ngOnInit(): void {
  }

}
