import { Component, OnInit } from '@angular/core';
import { IRegisterModel } from 'src/Models/RegisterModel';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  User:IRegisterModel={email:"",username:"",balance:0,mobilenumber:"",password:"",confirmpassword:""}
  error:string;
  constructor() { }

  ngOnInit(): void {
  }

  Signup(){

  }

}
