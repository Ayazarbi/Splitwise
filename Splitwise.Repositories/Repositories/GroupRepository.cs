using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

public class GroupRepository:IGroup{
    private readonly AppdbContext context;
    private readonly IExpense expenserepo;

    public UserManager<Applicationuser> UserManager { get; }

    public GroupRepository(AppdbContext context,UserManager<Applicationuser> userManager,IExpense  expenserepo)
    {
        this.context = context;
        UserManager = userManager;
        this.expenserepo = expenserepo;
    }


    public Group AddGroup(GroupModel model){
         
         var group=context.Groups.Add(model.Group);
         context.SaveChanges();
        foreach (var item in model.MembersId)
        {
            GroupMembers groupMembers=new GroupMembers(){
                GroupId=model.Group.GroupId,
                UserId=item,

            };

            context.GroupMembers.Add(groupMembers);
            context.SaveChanges();
            
        }
         var activity=new Activity();
       activity.Activitydata="You Added"+model.Group.Title+"Group";
       activity.Date=DateTime.Now.ToString();
       activity.UserId=model.Group.CreatorIdId;

       context.Activities.Add(activity);
       context.SaveChanges();

         return model.Group;
          
        }

    public async Task<GroupModel> GetGroup(int id){

        var group=context.Groups.ToList().FirstOrDefault(x=>x.GroupId==id);
        var expensesid=context.GroupsofExpenses.ToList().Where(x=>x.GroupId==id);
        
        var creator=await UserManager.FindByIdAsync(group.CreatorIdId);
        group.Createdby=creator;
        
        List<Expense> expenses=new List<Expense>();
         foreach (var item in expensesid)
        {
            var exp=context.Expenses.ToList().FirstOrDefault(x=>x.ExpenseId==item.ExpenseId);
            if(exp!=null){
                expenses.Add(exp);
            }    
        }
         var membersid=context.GroupMembers.ToList().Where(x=>x.GroupId==id);
        List<Applicationuser> membbers=new List<Applicationuser>();
        List<string> listofmembersid=new List<string>();
         foreach (var item in membersid)
        {
            var user=await UserManager.FindByIdAsync(item.UserId);
            if(user!=null){

                membbers.Add(user);
                listofmembersid.Add(user.Id);
            }
        }

        GroupModel model=new GroupModel(){
            Group=group,
            Expenses=expenses,
            Members=membbers,
            MembersId=listofmembersid,

        };

        return model;

    }

    public IEnumerable<Group> GetUserGroups(string userid){

        var groups=from Groupmember in context.GroupMembers.ToList()
                    join Group in context.Groups.ToList() on Groupmember.GroupId equals Group.GroupId
                    where Groupmember.UserId == userid
                    select Group;

            
            return groups;
                    
   
    }

    public GroupModel Editgroup(int id,GroupModel groupModel){
        
            var oldgroup=context.Groups.ToList().FirstOrDefault(x=>x.GroupId==id);
            oldgroup.Amount=groupModel.Group.Amount;
            oldgroup.CreatorIdId=groupModel.Group.CreatorIdId;
            oldgroup.Date=groupModel.Group.Date;
            oldgroup.Title=groupModel.Group.Title;
            var group=context.Groups.Attach(oldgroup);
            group.State=Microsoft.EntityFrameworkCore.EntityState.Modified;


            var groupMember=context.GroupMembers.Where(x=>x.GroupId==id);
            context.GroupMembers.RemoveRange(groupMember);

            var groupExpenses=context.GroupsofExpenses.ToList().Where(x=>x.GroupId==id);
            foreach (var item in groupExpenses)
            {
                expenserepo.DeleteExpense(item.ExpenseId);
            }
            
            foreach (var item in groupModel.MembersId.ToList()){

                  GroupMembers groupMembers=new GroupMembers();
                groupMembers.GroupId=oldgroup.GroupId;
                groupMembers.UserId=item;
            
                    context.GroupMembers.Add(groupMembers);
                    context.SaveChanges();
            
            }
            foreach (var item in groupModel.Expenses.ToList())
            {
                GroupExpense groupExpense=new GroupExpense(){
                    GroupId=oldgroup.GroupId,
                    ExpenseId=item.ExpenseId,
                };

                context.GroupsofExpenses.Add(groupExpense);
                context.SaveChanges();
            }

        var activity=new Activity();
       activity.Activitydata="You Edited"+groupModel.Group.Title+"group";
       activity.Date=DateTime.Now.ToString();
       activity.UserId=groupModel.Group.CreatorIdId;

       context.Activities.Add(activity);
       context.SaveChanges();
           
        return groupModel;
    }

    public Group DeleteGroup(int id){
        
        var group=context.Groups.ToList().FirstOrDefault(x=>x.GroupId==id);
        context.Groups.Remove(group);
        // var groupMembers=context.GroupMembers.Where(x=>x.GroupId==id);
        // context.GroupMembers.RemoveRange(groupMembers);
        // var groupExpense=context.GroupsofExpenses.Where(x=>x.GroupId==id);
        // context.GroupsofExpenses.RemoveRange(groupExpense);
        context.SaveChanges();

       
        var activity=new Activity();
       activity.Activitydata="You deleted"+group.Title+"group";
       activity.Date=DateTime.Now.ToString();
       activity.UserId=group.CreatorIdId;

       context.Activities.Add(activity);
       context.SaveChanges();
        return group;
    
    }

    public IEnumerable<Expense> GetGroupExpenses(int id){

        var groupExpense=from g in context.GroupsofExpenses.ToList()
                        join e in context.Expenses.ToList() on g.ExpenseId equals e.ExpenseId
                        where g.GroupId==id
                        select e;


        return groupExpense;
    }

    public async Task<List<BorrowLentModel>> GetGroupCalculation(int id){

        List<BorrowLentModel> calculations=new List<BorrowLentModel>(); 
        var settlemetns=context.Settelements.ToList().Where(x=>x.GroupId==id);
        foreach (var item in settlemetns)
        {
            BorrowLentModel borrowLentModel=new BorrowLentModel(){
                Amount=item.SettelementAmount,
                BorrowerId=item.BorrowerId,
                 LenterId=item.LenterId,
            };

            var borrower=await UserManager.FindByIdAsync(item.BorrowerId);
            var lenter=await UserManager.FindByIdAsync(item.LenterId);

            borrowLentModel.Borrower=borrower;
            borrowLentModel.Lenter=lenter;



        }

        return calculations;
        
    }

    

    public IEnumerable<Group> getallgroups()
    {
        return context.Groups.ToList();
    }
}