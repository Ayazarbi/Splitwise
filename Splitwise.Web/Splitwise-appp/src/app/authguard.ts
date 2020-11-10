import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { SplitWise } from './Services/SplitWiseApi';

@Injectable({
    providedIn:"root"
  
  })
export class Authguard implements CanActivate {

    constructor(private service:SplitWise.AccountClient,
                private router:Router){

    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        
            if(this.service.checklogin()){

                return true;
            
            }
            this.router.navigate(["/login"]);
            return false;
    }
}
