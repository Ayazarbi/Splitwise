using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("Group")]
public class GroupController:Controller{
    private readonly IGroup groupRepository;

    public GroupController(IGroup groupRepository){
        this.groupRepository = groupRepository;
    }


    [HttpPost]
    public ActionResult<Group> Add(GroupModel group){

        return Ok(groupRepository.AddGroup(group));
    }

    [HttpGet("{id}")]

    public async Task<ActionResult<GroupModel>> GetGroup(int id){

        
        var result=await groupRepository.GetGroup(id);
        return Ok(result);
    }

    [HttpDelete("{id}")]

    public ActionResult<Group> Delete(int id){

        return Ok(groupRepository.DeleteGroup(id));
    }

    [HttpGet("{id}/Expenses")]
    public ActionResult<IEnumerable<Expense>> GetGroupExpenses(int id){

        return Ok(groupRepository.GetGroupExpenses(id));
    }

    [HttpGet("{id}/calculations")]
    public async Task<ActionResult<BorrowLentModel>> Getcalculations(int id){

        return Ok(await groupRepository.GetGroupCalculation(id));
    }

    [HttpPut("{id}")]
    public ActionResult<GroupModel> Edit(int id,GroupModel groupModel){

        return Ok(groupRepository.Editgroup(id,groupModel));
    }

    [HttpGet]
    public ActionResult<IEnumerable<Group>> Getallgroups(){

        return Ok(groupRepository.getallgroups());
    }

    
}