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
    public ActionResult Add(GroupModel group){

        return Ok(groupRepository.AddGroup(group));
    }

    [HttpGet("{id}")]

    public async Task<ActionResult<GroupModel>> GetGroup(int id){

        
        var result=await groupRepository.GetGroup(id);
        return Ok(result);
    }

    [HttpDelete("{id}")]

    public ActionResult Delete(int id){

        return Ok(groupRepository.DeleteGroup(id));
    }

    [HttpGet("{id}/Expenses")]
    public ActionResult GetGroupExpenses(int id){

        return Ok(groupRepository.GetGroupExpenses(id));
    }

    [HttpGet("{id}/calculations")]
    public ActionResult Getcalculations(int id){

        return Ok(groupRepository.GetGroupCalculation(id));
    }

    [HttpPut("{id}")]
    public ActionResult Edit(int id,GroupModel groupModel){

        return Ok(groupRepository.Editgroup(id,groupModel));
    }



    
}