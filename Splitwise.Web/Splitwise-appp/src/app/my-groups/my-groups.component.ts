import { Component, OnInit } from '@angular/core';
import { IGroup } from 'src/Models/IGroup';
import { Splitwise } from '../Services/SplitWiseApi';

@Component({
  selector: 'app-my-groups',
  templateUrl: './my-groups.component.html',
  styleUrls: ['./my-groups.component.css']
})
export class MyGroupsComponent implements OnInit {
  groups:Splitwise.Group[];
  constructor(private service:Splitwise.UserClient) { }

  ngOnInit(): void {

      this.service.getUserGroups("15c37175-215a-47bc-b077-9dfc3c81441d").subscribe(
        x=>this.groups=x
      );
    
  }

}
