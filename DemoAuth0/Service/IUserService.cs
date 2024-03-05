namespace DemoAuth0.Service;

public interface IUserService
{
    Task RegisterUser(string name, string email, string pass);
    Task<string> UserLogin(string email, string pass);
    Task FindUseById(long id);
    Task<List<User>> GetUser();

}