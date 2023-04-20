using PhotoSharinggApplication.Models;
using PhotoSharinggApplication.Repositories.AkunRepository;

namespace PhotoSharinggApplication.Services.AkunService
{
    public class AkunService : IAkunService
    {
        private readonly IAkunRepository _ar;
        public AkunService(IAkunRepository ar)
        {
            _ar = ar;
        }

        public bool DaftarUser(User data)
        {
            data.Roles = _ar.AmbilRolesByIdAsync("2").Result;
            return _ar.DaftarUserAsync(data).Result;
        }

        public bool HapusAkun(string username)
        {
            var ak = _ar.CariUserAsync(username).Result;

            return _ar.HapusAkunAsync(ak).Result;
        }

        // User
        public List<User> AmbilSemuaUser()
        {
            return _ar.AmbilSemuaUserAsync().Result;
        }

        public User AmbilUserByUsername(string username)
        {
            return _ar.AmbilUserByUsernameAsync(username).Result;
        }

        public bool DaftarAdmin(User data)
        {
            data.Roles = _ar.AmbilRolesByIdAsync("1").Result;
            return _ar.DaftarAdminAsync(data).Result;
        }

        // Roles
        public List<Roles> AmbilSemuaRoles()
        {
            return _ar.AmbilSemuaRolesAsync().Result;
        }

        public Roles AmbilRolesById(string id)
        {
            return _ar.AmbilRolesByIdAsync(id).Result;
        }
    }
}
