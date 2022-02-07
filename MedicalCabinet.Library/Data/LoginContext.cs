
using MedicalCabinet.Library.Model;

namespace MedicalCabinet.Library.Data
{
    public class LoginContext
    {
        private readonly UserContext _userContext;

        public LoginContext(User newUser)
        {
            newUser.Password = HashUtility.GetHash(newUser.Password);
            _userContext = new UserContext(newUser);
        }

        public User SignIn()
        {
            using var db = new ApplicationContext();
            if (_userContext.ContainsLogin(db) && _userContext.ComparePasswords(db))
                return _userContext.GetUserByLogin(db);

            return null;
        }

        public bool SignUp()
        {
            using var db = new ApplicationContext();
            if (_userContext.ContainsUser(db))
                return false;

            db.Users.Add(_userContext.UserInfo);
            db.SaveChanges();
            return true;
        }
    }
}
