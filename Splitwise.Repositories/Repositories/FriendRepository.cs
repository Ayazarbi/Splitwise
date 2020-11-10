using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

public class FriendRepository:IFriend{

    private readonly AppdbContext context;

    public UserManager<Applicationuser> UserManager { get; }

    public FriendRepository(AppdbContext context,UserManager<Applicationuser> userManager){
        
        this.context = context;
        UserManager = userManager;
    }

    
    

    public async Task<IEnumerable<Applicationuser>> GetFriend(string id){

     List<Applicationuser> friends=new List<Applicationuser>();
      var result=context.Friends.ToList().Where(x=>x.UserId==id);
      foreach (var item in result)
      {
            var friend=await UserManager.FindByIdAsync(item.FrndId);
            if(friend!=null){
                friends.Add(friend);
            }
            

      }

      return friends;

    }

    public  async Task<Applicationuser> Deletefriend(string userid,string friendid){

        var activity=new Activity(); 
         var friend=context.Friends.ToList().FirstOrDefault(x=>x.UserId==userid && x.FrndId==friendid);
         if(friend!=null){
             var user=await UserManager.FindByIdAsync(friend.FrndId);
             context.Friends.Remove(friend);
             context.SaveChanges();
             activity.Activitydata="You removed"+user.UserName+"From your friend list";
             activity.Date=DateTime.Now.ToString();
         activity.UserId=friend.UserId;

        
            
            context.Activities.Add(activity);
            context.SaveChanges();
             return user;
         }
         


         return null;

    }

    public async Task<Friend> Addfriend(Friend friend)
    {
       var result= await  context.Friends.AddAsync(friend);
       context.SaveChanges();
       var user=await UserManager.FindByIdAsync(friend.FrndId);
       var activity=new Activity();
       activity.Activitydata="You added"+user.UserName+"In your friendlist";
       activity.Date=DateTime.Now.ToString();
       activity.UserId=friend.UserId;

       context.Activities.Add(activity);
       context.SaveChanges();
       return friend;
    }
}