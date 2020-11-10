import { Component, OnInit } from '@angular/core';
import { IGroup } from 'src/Models/IGroup';
import { SplitWise } from '../Services/SplitWiseApi';

@Component({
  selector: 'app-my-groups',
  templateUrl: './my-groups.component.html',
  styleUrls: ['./my-groups.component.css']
})
export class MyGroupsComponent implements OnInit {
  groups:SplitWise.Group[];
  constructor(private service:SplitWise.UserClient) { }

  ngOnInit(): void {

      this.service.getUserGroups(localStorage.getItem("id")).subscribe(
        x=>this.groups=x
      );
    
  }

}
