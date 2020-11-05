import { IUser } from './IUser';

export interface IPayerModel{

    payerId:string;
    payer:IUser;
    amount:number;
}