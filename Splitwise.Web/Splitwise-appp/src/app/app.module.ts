import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ResetpasswordComponent } from './resetpassword/resetpassword.component';
import { ContainerComponent } from './container/container.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MyExpensesComponent } from './my-expenses/my-expenses.component';
import { MyGroupsComponent } from './my-groups/my-groups.component';
import { AddExpensesComponent } from './add-expenses/add-expenses.component';
import { AddGroupComponent } from './add-group/add-group.component';
import { ActivitiesComponent } from './activities/activities.component';
import { CreatepaymentsComponent } from './createpayments/createpayments.component';
import { EditgroupComponent } from './editgroup/editgroup.component';
import { FormsModule } from '@angular/forms';
import { SplitWise} from './Services/SplitWiseApi';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Authguard } from './authguard';
import { EditexpenseComponent } from './editexpense/editexpense.component';



@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    ResetpasswordComponent,
    ContainerComponent,
    DashboardComponent,
    MyExpensesComponent,
    MyGroupsComponent,
    AddExpensesComponent,
    AddGroupComponent,
    ActivitiesComponent,
    CreatepaymentsComponent,
    EditgroupComponent,
    EditexpenseComponent,

    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    
    
    

  ],
  providers: [SplitWise.AccountClient ,SplitWise.UserClient,SplitWise.ExpenseClient,SplitWise.GroupClient,SplitWise.PaymentClient,SplitWise.FriendsClient],
  bootstrap: [AppComponent]
})
export class AppModule { }
