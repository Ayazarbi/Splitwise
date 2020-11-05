import { Component, OnInit } from '@angular/core';
import { ILoginModel } from 'src/Models/ILoginModel';
import { Splitwise } from '../Services/SplitWiseApi';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  
  error:string;
  User:Splitwise.LoginModel={email:"",password:"",init:null,toJSON:null};

  
   

  constructor(private service:Splitwise.AccountClient) { }

  ngOnInit(): void {
  }

  login(){
    
    this.service.login(this.User).subscribe(x=>console.log(x.data),
    err=>{
      console.log();
    })
  }

}
