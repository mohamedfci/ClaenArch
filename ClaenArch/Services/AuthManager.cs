using Infrastructure.Contexts;

namespace ClaenArch.Services
{
    public class AuthManager
    {
        private readonly ITokenService _tokenService;
        private readonly ApplicationDbContext _context; // Inject your DbContext here

        public AuthManager(ITokenService tokenService, ApplicationDbContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        public string Authenticate(string username, string password)
        {
            // Example logic: Check username and password against database
            var user = _context.TblUsers.SingleOrDefault(u => u.UserName == username && u.Pass == password);

            if (user == null)
            {
                return null; // User not found or invalid credentials
            }

            // Generate JWT token
            return _tokenService.GenerateJwtToken(user);
        }
    }
}
