import { IGroup } from './IGroup';
import { IUser } from './IUser';

export interface IExpense{

    groupId:number;
    group:IGroup;
    expenseId:number;
    title:string;
    splitType:string;
    amount:number;
    date:string;
    userId:string;
    user:IUser;
}