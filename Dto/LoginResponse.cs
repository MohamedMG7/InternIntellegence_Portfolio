using Microsoft.AspNetCore.Identity;

namespace InternIntellegence_Portfolio.Dto{
    public class LoginResponse{
        public SignInResult Result { get; set; } = null!;
        public string? Token { get; set; }
    }
}