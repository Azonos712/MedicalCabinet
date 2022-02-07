
using MedicalCabinet.Library.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MedicalCabinet.Library.Data
{
    class UserContext
    {
        public User UserInfo { get; private set; }

        public UserContext(User user)
        {
            UserInfo = user;
        }

        public bool ContainsUser(ApplicationContext db) => ContainsEmail(db) || ContainsLogin(db);

        public bool ContainsEmail(ApplicationContext db) => db.Users.Any(u => u.Email == UserInfo.Email);

        public bool ContainsLogin(ApplicationContext db) => db.Users.Any(u => u.Login == UserInfo.Login);

        public bool ComparePasswords(ApplicationContext db) => UserInfo.Password == GetUserByLogin(db).Password;

        public User GetUserByLogin(ApplicationContext db)
        {
            return db.Users
                .Include(u => u.Doctor)
                    .ThenInclude(d => d.Notes)
                .Include(u => u.Doctor)
                    .ThenInclude(d => d.CasesOfIllness)
                    .ThenInclude(c => c.Patient)
                .Single(u => u.Login == UserInfo.Login);
        }
    }
}
