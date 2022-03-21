using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LetsCode.Models;
using Microsoft.IdentityModel.Tokens;

namespace LetsCode.Customs;

public static class JwtTokenService
{
    public static string GenerateToken(User user, string jwtToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = GetKeyBytes(jwtToken);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.UserData, user.Username),
                new Claim(ClaimTypes.Role, MyJwtConstants.DEFAULT_ROLE)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static byte[] GetKeyBytes(String key)
    {
        return Encoding.ASCII.GetBytes(key);
    }

    public static void AddRoleClaims(ClaimsPrincipal principal)
    {
        var claimsIdentity = principal.Identity as ClaimsIdentity;
        if (claimsIdentity != null)
        {
            if (!claimsIdentity.HasClaim(ClaimTypes.Role, MyJwtConstants.DEFAULT_ROLE))
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, MyJwtConstants.DEFAULT_ROLE));
            }
        }
    }
}

#nullable disable
public static class MyJwtConstants
{
    public const string CONFIG_SECTION_NAME = "Secrets";
    public const string CONFIG_KEY_NAME = "PrivateJwt";

    public const string CONFIG = $"{CONFIG_SECTION_NAME}:{CONFIG_KEY_NAME}";

    public const string DEFAULT_KEY = "d861056ebef14a9d923c49544bfab760";

    public const string BEARER_FORMAT = "JWT";

    public const string DEFAULT_ROLE = "User";

    public const string DEFAULT_POLICY = "UserPolicy";
}
