using Microsoft.AspNetCore.Authentication;

namespace AzureLearningDocker.Models
{
    public class LoginViewModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
        public List<AuthenticationScheme> ExternalLogins { get; set; } = new List<AuthenticationScheme>();
    }
}
