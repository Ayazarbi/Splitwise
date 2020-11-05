import { NumberSymbol } from '@angular/common';
import { IExpense } from './IExpense';
import { IUser } from './IUser';

export interface IShare{

    shareId:number,
    expenseId:number,
    expesne:IExpense,
    userId:string,
    user:IUser,
    sharePercentage:number,
    shareAmount:number

}