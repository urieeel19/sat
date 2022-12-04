using WebApplication1.DAL.Domain;

namespace WebApplication1.BusinessLogic.Models.UserType
{
    public class SuperUser : DAL.Domain.UserType
    {
        public override void SetMoney(User user)
        {
            if (user.Money > 100)
            {
                var gif = user.Money * 0.2;
                user.Money = user.Money + gif;
            }
        }
    }
}
