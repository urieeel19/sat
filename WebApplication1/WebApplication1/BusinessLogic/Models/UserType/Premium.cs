
using WebApplication1.DAL.Domain;

namespace WebApplication1.BusinessLogic.Models.UserType
{
    public class Premium : DAL.Domain.UserType
    {
        public override void SetMoney(User user)
        {
            if (user.Money > 100)
            {
                user.Money += user.Money * 2;
            }
        }
    }
}
