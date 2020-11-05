import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ActivitiesComponent } from './activities/activities.component';
import { AddExpensesComponent } from './add-expenses/add-expenses.component';
import { AddGroupComponent } from './add-group/add-group.component';
import { ContainerComponent } from './container/container.component';
import { CreatepaymentsComponent } from './createpayments/createpayments.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EditgroupComponent } from './editgroup/editgroup.component';
import { EditComponent } from './Expenseinfo/edit/edit.component';
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
  {path:"splitwise",component:ContainerComponent,children:[

    {path:"dashboard",component:DashboardComponent},
    {path:"myexpenses",component:MyExpensesComponent},
    {path:"mygroups",component:MyGroupsComponent},
    {path:"expenseinfo/:id",component:EditComponent},
    {path:"groupinfo/:id",component:EditgroupComponent},
    {path:"addgroup",component:AddGroupComponent},
    {path:"addexpense",component:AddExpensesComponent},
    {path:"activities/:id",component:ActivitiesComponent},
    {path:"pay",component:CreatepaymentsComponent,}
  ]}

  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
