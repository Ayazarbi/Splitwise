using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAccount{

    public Task<bool> Login(LoginModel model);

    public SignupModel Signup(SignupModel model);

    public Applicationuser ResetPassword(string userid ,ResetpasswordModel model);

    public Task<UserModel> GetUserinfo(string id);

    public IEnumerable<Applicationuser> Getalluser();
}