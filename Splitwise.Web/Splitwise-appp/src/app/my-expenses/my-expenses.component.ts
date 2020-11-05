import { Component, OnInit } from '@angular/core';
import { IExpense } from 'src/Models/IExpense';

@Component({
  selector: 'app-my-expenses',
  templateUrl: './my-expenses.component.html',
  styleUrls: ['./my-expenses.component.css']
})
export class MyExpensesComponent implements OnInit {
  expenses:IExpense[]=[
    {amount:0,date:"",expenseId:
    1,group:null,groupId:1,splitType:"equally",title:"title",user:null,userId:"11"},
    {amount:0,date:"",expenseId:
    1,group:null,groupId:1,splitType:"equally",title:"title",user:null,userId:"11"},
 
  ];
  constructor() { }

  ngOnInit(): void {
  }

}
