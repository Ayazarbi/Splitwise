import { Component, OnInit } from '@angular/core';
import { SplitWise } from '../Services/SplitWiseApi';

@Component({
  selector: 'app-createpayments',
  templateUrl: './createpayments.component.html',
  styleUrls: ['./createpayments.component.css']
})
export class CreatepaymentsComponent implements OnInit {

  PayeeId:string="";
  Amount:number=0;
  error:string;
  
  friends:SplitWise.Applicationuser[];
  constructor(private friendservice :SplitWise.FriendsClient,
              private paymentservice:SplitWise.PaymentClient) { }

  ngOnInit(): void {

    this.friendservice.getfriend(localStorage.getItem("id")).subscribe(x=>{this.friends=x;
    console.log(x)});
  }

  Payment(){

   var Transaction:SplitWise.TransactionModel={paidAmount:this.Amount,payeeId:this.PayeeId,payerId:localStorage.getItem("id"),init:null,toJSON:null};
    
   console.log(Transaction);
   this.paymentservice.add(Transaction).subscribe(x=>console.log(x));

}





  }

