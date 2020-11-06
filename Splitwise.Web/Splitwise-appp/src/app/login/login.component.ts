import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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

  
   

  constructor(private service:Splitwise.AccountClient,
              private router:Router) { }

  ngOnInit(): void {
  }

  login(){
    
    this.service.login(this.User).subscribe(x=>{
          
      var token=x["token"];
      const payLoad = JSON.parse(window.atob(token.split('.')[1]));
          
      localStorage.setItem("token",token);
      localStorage.setItem("id",payLoad["id"]);
      this.router.navigate(["/splitwise/dashboard"]);
    },
    err=>{
    
    this.error="Invalid credentials"
    })
  }

}
