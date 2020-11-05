import { IExpense } from './IExpense';
import { IPayerModel } from './IPayerModel';
import { IShare } from './IShare';
import { IUser } from './IUser';

export interface IExpenseModel{

    expense:IExpense;
    payers:IPayerModel[];
    shares:IShare[];
    payersId:string[];


}