using PhotoSharinggApplication.Data;
using PhotoSharinggApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace PhotoSharinggApplication.Repositories.AkunRepository
{
    public class AkunRepository : IAkunRepository
    {
        private readonly AppDbContext _context;

        public AkunRepository(AppDbContext db)
        {
            _context = db;
        }

        public async Task<bool> DaftarUserAsync(User data)
        {
            _context.Users.Add(data);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<User> CariUserAsync(string Username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == Username);
        }

        public async Task<bool> HapusAkunAsync(User data)
        {
            _context.Remove(data);
            await _context.SaveChangesAsync();

            return true;
        }


        // USER
        public async Task<List<User>> AmbilSemuaUserAsync()
        {
            return await _context.Users.Include(x => x.Roles).ToListAsync();
        }

        public async Task<User> AmbilUserByUsernameAsync(string username)
        {
            return await _context.Users.FindAsync(username);
        }

        public async Task<bool> DaftarAdminAsync(User data)
        {
            _context.Add(data);
            await _context.SaveChangesAsync();

            return true;
        }

        //ROLES
        public async Task<List<Roles>> AmbilSemuaRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Roles> AmbilRolesByIdAsync(string id)
        {
            return await _context.Roles.FindAsync(id);
        }
    }
}
