using LetsCode.Models;

namespace LetsCode.Customs;

public static class UserRepository
{
    public static User Get(string username, string password)
    {
        var users = new List<User>();
        users.Add(new User("local", "host", "Eu mesmo"));
        users.Add(new User("letscode", "lets@123", "Avaliador"));
        return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == x.Password).FirstOrDefault();
    }
}
