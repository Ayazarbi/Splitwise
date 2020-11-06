import { Component, OnInit } from '@angular/core';
import { IRegisterModel } from 'src/Models/RegisterModel';
import { Splitwise } from '../Services/SplitWiseApi';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  username:string;
  email:string;
  password:string;
  confirmpassword:string;
  balance:number=0.0;
  mobilenumber:string;
  User?:Splitwise.SignupModel={balance:0,mobilenumber:"",email:"",password:"",username:"",init:null,toJSON:null};
  error:string;
  constructor(private service:Splitwise.AccountClient) { }

  ngOnInit(): void {
  }

  Signup(){
    this.User.balance=this.balance;
    this.User.username=this.username;
    this.User.password=this.password;
    this.User.mobilenumber=this.mobilenumber;
    this.User.email=this.email;
    console.log(this.User);
    this.service.register(this.User).subscribe(x=>{console.log;
    this.error="User Added Successfully"},err=>{

      if(err.status=="400"){
        this.error="something went wrong";
      }
    })
    
  }

}
