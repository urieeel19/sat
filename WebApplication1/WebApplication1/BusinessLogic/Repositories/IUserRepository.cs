using WebApplication1.DAL.Domain;

namespace WebApplication1.BusinessLogic.Repositories
{
    public interface IUserRepository
    {
        bool IsDuplicated(User entity);
        User CreateAsync(User entity);

        IEnumerable<User> Get();  
    }
}
