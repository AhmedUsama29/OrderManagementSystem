using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstraction;
using Shared.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class AuthenticationService(UserManager<IdentityUser> _userManager,
                                        IOptions<JWTOptions> _jwtOptions) : IAuthenticationService
    {
     
        public async Task<UserResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username)
                ?? throw new UserNotFoundException(request.Username);

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (isPasswordValid)
                return new UserResponse
                {
                    Username = user.UserName!,
                    Token = await GenerateToken(user)
                };

            throw new UnAuthorizedException();
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new IdentityUser()
            {
                UserName = request.Username,

            };

            var result = await _userManager.CreateAsync(user, request.Password);


            if (result.Succeeded) return new UserResponse()
            {
                Username = user.UserName,
                Token = await GenerateToken(user)
            };

            var Errors = result.Errors.Select(e => e.Description).ToList();

            throw new BadRequestException(Errors);
        }

        private async Task<string> GenerateToken(IdentityUser user)
        {
            var jwtOptions = _jwtOptions.Value;

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName!),
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            string secretKey = jwtOptions.SecretKey;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(jwtOptions.DurationInDays),
                signingCredentials: credentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

    }
}
