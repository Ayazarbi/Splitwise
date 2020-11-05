import { IActivity } from './IActivity';
import { IExpense } from './IExpense';
import { IGroup } from './IGroup';
import { IPayerModel } from './IPayerModel';
import { ITransaction } from './ITransaction';
import { IUser } from './IUser';

export interface IUserModel{

    user:IUser;
    epenses:IExpense[];
    groups:IGroup[];
    owesfrom:IPayerModel[];
    owesto:IPayerModel[];
    activities:IActivity[];
    transactions:ITransaction[];
    Friends:IUser[];
}