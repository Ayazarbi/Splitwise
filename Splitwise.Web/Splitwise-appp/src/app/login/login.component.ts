import { Component, OnInit } from '@angular/core';
import { ILoginModel } from 'src/Models/ILoginModel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  error:string;
  User:ILoginModel={email:"",password:""};
   

  constructor() { }

  ngOnInit(): void {
  }

  login(){
    
  }

}
