using WebApplication1.Application.User.Create;
using WebApplication1.Wrappers;

namespace WebApplication1.BusinessLogic.Services
{
    public interface IUserService
    {
        Task<Result<ResponseCreate>> CreateAsync(RequestCreate request);
    }

}
