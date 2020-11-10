using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFriend{

public Task<Friend> Addfriend(Friend friend);
public Task<IEnumerable<Applicationuser>> GetFriend(string id);

public Task<Applicationuser> Deletefriend(string userid,string friendid); 



}