import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ActivitiesComponent } from './activities/activities.component';
import { AddExpensesComponent } from './add-expenses/add-expenses.component';
import { AddGroupComponent } from './add-group/add-group.component';
import { Authguard } from './authguard';
import { ContainerComponent } from './container/container.component';
import { CreatepaymentsComponent } from './createpayments/createpayments.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EditexpenseComponent } from './editexpense/editexpense.component';
import { EditgroupComponent } from './editgroup/editgroup.component';
import { LoginComponent } from './login/login.component';
import { MyExpensesComponent } from './my-expenses/my-expenses.component';
import { MyGroupsComponent } from './my-groups/my-groups.component';
import { RegisterComponent } from './register/register.component';
import { ResetpasswordComponent } from './resetpassword/resetpassword.component';


const routes: Routes = [
  {path:"",component:LoginComponent},
  {path:"login",component:LoginComponent},
  {path:"register",component:RegisterComponent},
  {path:"resetpass",component:ResetpasswordComponent},
  {path:"splitwise",component:ContainerComponent,canActivate:[Authguard],children:[

    {path:"dashboard",component:DashboardComponent,canActivateChild:[Authguard]},
    {path:"myexpenses",component:MyExpensesComponent,canActivateChild:[Authguard]},
    {path:"mygroups",component:MyGroupsComponent,canActivateChild:[Authguard]},
    {path:"groupinfo/:id",component:EditgroupComponent,canActivateChild:[Authguard]},
    {path:"expenseinfo/:id",component:EditexpenseComponent,canActivateChild:[Authguard]},
    {path:"addgroup",component:AddGroupComponent,canActivateChild:[Authguard]},
    {path:"addexpense",component:AddExpensesComponent,canActivateChild:[Authguard]},
    {path:"activities/:id",component:ActivitiesComponent,canActivateChild:[Authguard]},
    {path:"pay",component:CreatepaymentsComponent,canActivateChild:[Authguard]},
    {path:"activity",component:ActivitiesComponent,canActivateChild:[Authguard]}
  ]}

  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
