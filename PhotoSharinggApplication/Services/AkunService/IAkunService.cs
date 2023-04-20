using PhotoSharinggApplication.Models;

namespace PhotoSharinggApplication.Services.AkunService
{
    public interface IAkunService
    {
        bool DaftarUser(User data);
        bool HapusAkun(string username);

        //USER
        List<User> AmbilSemuaUser();
        User AmbilUserByUsername(string username);
        bool DaftarAdmin(User data);

        //ROLES
        List<Roles> AmbilSemuaRoles();
        Roles AmbilRolesById(string id);
    }
}
