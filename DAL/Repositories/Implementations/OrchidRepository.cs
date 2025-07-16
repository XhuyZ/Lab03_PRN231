using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementations
{
    public class OrchidRepository : IOrchidRepository
    {
        private readonly AppDBContext _context;

        public OrchidRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Orchid>> GetAllAsync()
        {
            return await _context.Orchids
                                 .Include(o => o.Category)
                                 .ToListAsync();
        }

        public async Task<Orchid?> GetByIdAsync(int id)
        {
            return await _context.Orchids
                                 .Include(o => o.Category)
                                 .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Orchid>> SearchAsync(string keyword)
        {
            return await _context.Orchids
                                 .Where(o => o.Name.Contains(keyword) ||
                                             o.Description.Contains(keyword))
                                 .Include(o => o.Category)
                                 .ToListAsync();
        }

        public async Task AddAsync(Orchid orchid)
        {
            await _context.Orchids.AddAsync(orchid);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Orchid orchid)
        {
            _context.Orchids.Update(orchid);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var orchid = await _context.Orchids.FindAsync(id);
            if (orchid != null)
            {
                _context.Orchids.Remove(orchid);
                await _context.SaveChangesAsync();
            } } public async Task<bool> ExistsAsync(int id) { return await _context.Orchids.AnyAsync(o => o.Id == id); } }
}

