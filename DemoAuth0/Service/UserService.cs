using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoAuth0.Service;

public class UserService : IUserService
{
    private readonly DemoContext _context;
    private readonly IConfiguration _configuration;

    public UserService(DemoContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    private string GenerateToken(User user)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString())

            };
        var tokeOptions = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(20),
            signingCredentials: signinCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

        return tokenString;
    }

    public async Task RegisterUser(string name, string email, string pass)
    {
        var user = new User();
        user.Name = name;
        user.Email = email;
        user.Password = pass;

        await _context.NewUsers.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task FindUseById(long id)
    {
        var user = await _context.NewUsers.FindAsync(id) ?? throw new Exception("User Not Found");
    }

    public async Task<string> UserLogin(string email, string pass)
    {
        var user = await _context.NewUsers.FirstOrDefaultAsync(x => x.Email == email) ?? throw new Exception("User is Not Found");

        if (user.Password != pass)
            throw new Exception("Password Does Not Match");

        var token = GenerateToken(user);
        return token;
    }

    public async Task<List<User>> GetUser()
    {
        var user = _context.NewUsers.ToList();

        return user;
    }
}
