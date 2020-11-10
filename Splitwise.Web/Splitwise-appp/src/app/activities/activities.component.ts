import { Component, OnInit } from '@angular/core';
import { SplitWise } from '../Services/SplitWiseApi';

@Component({
  selector: 'app-activities',
  templateUrl: './activities.component.html',
  styleUrls: ['./activities.component.css']
})
export class ActivitiesComponent implements OnInit {
 
  Activities:SplitWise.Activity[];
  constructor(
    private activityservice:SplitWise.UserClient
  ) { }

  ngOnInit(): void {
    this.activityservice.getUserActivties(localStorage.getItem("id")).subscribe(x=>this.Activities=x)
 
  }

}
