import { IExpense } from './IExpense';
import { IGroup } from './IGroup';
import { IUser } from './IUser';

export interface IGroupModel{

    group:IGroup;
    Members:IUser[];
    MembersId:string[];
    Expense:IExpense[];
}