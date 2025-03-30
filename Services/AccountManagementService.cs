using InternIntellegence_Portfolio.Dto;
using InternIntellegence_Portfolio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InternIntellegence_Portfolio.Services
{
	public class AccountManagementService
	{
		
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly JWT _jwt;

        public AccountManagementService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _jwt = jwt.Value;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto RegistrationData){

            var user = new ApplicationUser{
                PhoneNumber = RegistrationData.PhoneNumber,
                Email = RegistrationData.Email,
                UserName = RegistrationData.Email
            };

            var AccountRegResult = await _userManager.CreateAsync(user,RegistrationData.Password);

            if(!AccountRegResult.Succeeded){
                return new AuthResponseDto 
                {
                    Success = false,
                    Errors = AccountRegResult.Errors.Select(e => e.Description).ToList()
                };
            }

            return new AuthResponseDto {
                Success = AccountRegResult.Succeeded,
                Errors = AccountRegResult.Succeeded ? null : AccountRegResult.Errors.Select(e => e.Description).ToList()
            };
        }

        public async Task<LoginResponse> LoginAsync(LoginDto LoginData)
        {
            var user = await _userManager.FindByEmailAsync(LoginData.Email);
            if (user == null)
            {
                return new LoginResponse{
                    Result = SignInResult.Failed,
                    Token = null
                };
            }

            var Loginresult = await _signInManager.PasswordSignInAsync(
                user,
                LoginData.Password,
                isPersistent: false,  
                lockoutOnFailure: true  
            );

            // Generate token only for successful logins
            
            JwtSecurityToken token = new JwtSecurityToken();
            if(Loginresult.Succeeded){
                token = await CreateJwtToken(user);
            }
            

            return new LoginResponse{
                Result = Loginresult,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user){
            var userClaims = await _userManager.GetClaimsAsync(user); 
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach(var role in roles){
                roleClaims.Add(new Claim("roles",role));
            }

            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Uid",user.Id)
            }.Union(userClaims).Union(roleClaims);

            var SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var SigningCredentials = new SigningCredentials(SymmetricSecurityKey,SecurityAlgorithms.HmacSha256);

            var JwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audiance,
                claims: claims,
                expires: DateTime.Now.AddHours(_jwt.DurationInHours),
                signingCredentials: SigningCredentials
            );

            return JwtSecurityToken;
        }
	}
}
