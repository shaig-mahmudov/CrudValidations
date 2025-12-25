using WebAppPractiece.Data;

namespace WebAppPractiece.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;
        public LayoutService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, string>> GetAllSettings()
        {
            Dictionary<string, string> settings =  _context.Settings.Where(m => !m.IsDeleted).AsEnumerable().ToDictionary(m => m.Key, m => m.Value);

            return settings;
        }

    }
}
