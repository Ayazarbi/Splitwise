import { IUser } from './IUser';

export interface IActivity{

    activityId:number;
    userId:string;
    user:IUser;
    activitydata:string;
    date:string;
}