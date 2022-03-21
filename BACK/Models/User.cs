namespace LetsCode.Models;

public class User : BaseEntity
{
    public string Username { get; private set; }

    public string Password { get; private set; }
    public string Name { get; private set; }

    public User(string username, string password, string name) : base()
    {
        Username = username;
        Password = password;
        Name = name;
    }
}

public class AuthenticationRequest
{
    public string? Login { get; set; }

    public string? Senha { get; set; }
}
