import { IUser } from './IUser';

export interface IGroup{

    groupId:number;
    title:string;
    creatorIdId:string;
    createdby:IUser;
    date:string;
    amount:number;
}