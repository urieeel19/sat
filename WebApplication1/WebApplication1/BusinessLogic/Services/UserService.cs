using AutoMapper;
using WebApplication1.Application.User.Create;
using WebApplication1.BusinessLogic.Factory;
using WebApplication1.BusinessLogic.Repositories;
using WebApplication1.BusinessLogic.Utils;
using WebApplication1.DAL.Domain;
using WebApplication1.Wrappers;

namespace WebApplication1.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IUserTypeFactory _userFactory;

        public UserService(IUserRepository repository, IUserTypeFactory userFactory, IMapper mapper, ILogger<UserService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _userFactory = userFactory;
        }

        public async Task<Result<ResponseCreate>> CreateAsync(RequestCreate request)
        {
            request.Email = Utils.Utils.NormalizeEmail(request.Email);

            var user = _mapper.Map<User>(request);
            var type = _userFactory.Get(request.UserType);
            type.SetMoney(user);

            var isDuplicated = _repository.IsDuplicated(user);
            if (isDuplicated)
            {
                //return new FailedResult<ResponseCreate>(ResultType.Invalid, Errors.USER_DUPLICATED);
                return new FailedResult<ResponseCreate>(ResultType.Invalid, "Usuario duplicado");
            }

            var result =  _repository.CreateAsync(user);
            var response = _mapper.Map<ResponseCreate>(result);
            return new SuccessResult<ResponseCreate>(response);
        }

    }
}
