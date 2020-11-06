import { Component, OnInit } from '@angular/core';
import { Splitwise } from '../Services/SplitWiseApi';

@Component({
  selector: 'app-container',
  templateUrl: './container.component.html',
  styleUrls: ['./container.component.css']
})
export class ContainerComponent implements OnInit {

  Isloggedin:boolean;
  
  constructor(private service:Splitwise.AccountClient) { }

  ngOnInit(): void {
    
    this.Isloggedin= this.service.checklogin();
    console.log(this.Isloggedin);
  }

  logout(){

    this.service.logout();
  }
}
