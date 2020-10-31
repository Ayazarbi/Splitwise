using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

public class ActivityRepository:IActivity{
    private readonly AppdbContext context;

    public ActivityRepository(AppdbContext context){
        this.context = context;
    }





    
    public IEnumerable<Activity> GetUserActivities(string id){

       var activities=context.Activities.ToList().Where(x=>x.UserId==id);

        return activities;
    }
}