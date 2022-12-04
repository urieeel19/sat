using WebApplication1.BusinessLogic.Models.UserType;
using WebApplication1.DAL.Domain;

namespace WebApplication1.BusinessLogic.Factory
{
    public interface IUserTypeFactory
    {
        UserType Get(string userType);
    }

    public class UserTypeFactory : IUserTypeFactory
    {
        public const string PREMIUN = "Premium";
        public const string NORMAL = "Normal";
        public const string SUPERUSER = "SuperUser";

        public Dictionary<string, UserType> _dictionary = new()
        {

            { PREMIUN, new Premium() },
            { NORMAL, new Normal () },
            { SUPERUSER, new SuperUser () }
        };

        public UserType Get(string userType)
        {
            if (!_dictionary.ContainsKey(userType))
            {
                throw new Exception(Application.ErrorMessages.UserTypeErrorMessages.INVALID_USERTYPE);
            }
            return _dictionary[userType];
        }
    }
}
