using InternIntellegence_Portfolio.DbHelper.DatabaseContext;

namespace InternIntellegence_Portfolio.DbHelper.Repos{
    public class ValidationRepo{

        private readonly ApplicationContext _context;

        public ValidationRepo(ApplicationContext context)
        {
            _context = context;
        }
        public bool UserExists(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }
            return _context.Users.Any(u => u.Id == userId);
        }
    }
}

