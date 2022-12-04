using WebApplication1.DAL.Domain;

namespace WebApplication1.BusinessLogic.Models.UserType
{
    public class Normal : DAL.Domain.UserType
    {
        public override void SetMoney(User user)
        {
            if (user.Money > 100)
            {
                var gif = user.Money * 0.12;
                user.Money = user.Money + gif;
            }
            if (user.Money < 100)
            {
                if (user.Money > 10)
                {
                    var gif = user.Money * 0.8;
                    user.Money = user.Money + gif;
                }
            }
        }
    }
}
