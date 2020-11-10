using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class FriendsController : Controller{
    private readonly IFriend friendRepository;

    public FriendsController(IFriend friendRepository){
        this.friendRepository = friendRepository;
    }

    [HttpPost]
    public async Task<ActionResult<Friend>> Add(Friend friend){

        return Ok( await friendRepository.Addfriend(friend));

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Applicationuser>>> getfriend(string id){

        var friends=await friendRepository.GetFriend(id);
        return Ok(friends);
    }

    [HttpDelete("{userid}/{friendid}")]
    public async Task<ActionResult<Applicationuser>> delete(string userid,string friendid){
        return Ok( await friendRepository.Deletefriend(userid,friendid));
    }
}