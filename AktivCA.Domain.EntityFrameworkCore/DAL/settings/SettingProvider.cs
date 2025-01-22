using AktivCA.Domain.EntityFrameworkCore.EntityFrameworkCore;
using AktivCA.Domain.Settings;
using Microsoft.EntityFrameworkCore;

namespace AktivCA.Domain.EntityFrameworkCore.DAL.Settings
{
    public class SettingProvider: ISettingProvider
    {
        private AktivCADbContext _context { get; set; } 
        public SettingProvider(AktivCADbContext context)
        {
            _context = context;
        }
        public async Task<Setting?> GetSettingsAsync()
        {
            return await _context.Settings.FirstOrDefaultAsync();
        }

        public async Task CreateSettingsAsync(Setting entity)
        {
            await _context.Settings.AddAsync(entity);
        }

        public void UpdateSettings(Setting entity)
        {
            _context.Settings.Update(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
