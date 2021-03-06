import { Component, OnInit } from '@angular/core';
import { IExpense } from 'src/Models/IExpense';
import { SplitWise } from '../Services/SplitWiseApi';

@Component({
  selector: 'app-my-expenses',
  templateUrl: './my-expenses.component.html',
  styleUrls: ['./my-expenses.component.css']
})
export class MyExpensesComponent implements OnInit {
  expenses:SplitWise.Expense[];
  //   {amount:0,date:"",expenseId:
  //   1,group:null,groupId:1,splitType:"equally",title:"title",user:null,userId:"11"},
  //   {amount:0,date:"",expenseId:
  //   1,group:null,groupId:1,splitType:"equally",title:"title",user:null,userId:"11"},
 
  // ];
  constructor(private userexpenseservice:SplitWise.UserClient,
              private expenseservice:SplitWise.ExpenseClient) {

   }

  ngOnInit(): void {
    this.userexpenseservice.getUserExpenses(localStorage.getItem("id")).subscribe(x=>{
      this.expenses=x;
    },
    err=>{
      console.log(err)
    })
    

  
  }


  Deleteexpense(id){

    this.expenseservice.delete(id).subscribe(x=>{
      alert("Expense deleted Successfully");
    })
  }

}
