using System.Collections.Generic;
using System.Threading.Tasks;

public interface IGroup{

    public Group AddGroup(GroupModel group);

    public  Task<GroupModel> GetGroup(int id);

    public IEnumerable<Group> GetUserGroups(string userid);

    public GroupModel Editgroup(int id,GroupModel groupModel);

    public Group DeleteGroup(int id);

    public IEnumerable<Expense> GetGroupExpenses(int id);

    public Task<List<Settelement>> GetGroupCalculation(int id);

    public IEnumerable<Group> getallgroups();

}