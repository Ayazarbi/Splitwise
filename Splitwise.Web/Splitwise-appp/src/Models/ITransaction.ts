import { IUser } from './IUser';

export interface ITransaction{

    transactionId:number;
    payerId:string;
    payer:IUser;
    payeeId:string;
    payee:IUser;
    paidAmount:number;
    settelementId:number;


}