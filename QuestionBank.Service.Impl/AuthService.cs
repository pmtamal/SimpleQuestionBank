using AutoMapper;
using Microsoft.Extensions.Logging;
using QuestionBank.Model.Domain;
using QuestionBank.Repository.Interface;
using QuestionBank.Service.Interface;
using QuestionBank.Utility;

namespace QuestionBank.Service.Impl
{
    public class AuthService :BaseService, IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserAccountService> _logger;
        private readonly IMiscRepository _miscRepository;

        public AuthService(IMapper mapper, IUserAccountRepository userAccountRepository, 
            IUnitOfWork unitOfWork, ILogger<UserAccountService> logger,IMiscRepository miscRepository):base(logger,unitOfWork)
        {
            _mapper = mapper;
            _userAccountRepository = userAccountRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _miscRepository = miscRepository;
        }

        
        public async Task<UserAccount?> AuthenticateUserAsync(string userName, string password)
        {
            var dbUser = await _userAccountRepository.GetByEmailAsync(userName);

            if (dbUser != null && CryptoHelper.VerifyPassword(password, dbUser.Password)) {

                return _mapper.Map<UserAccount>(dbUser);
            
            }

            return null;

        }

        public async Task AddRefreshToken(UserRefreshToken userRefreshToken)
        {
            await _miscRepository.AddRefreshTokenAsync(_mapper.Map<Persistence.Entity.UserRefreshToken>(userRefreshToken));

        }

        public async Task<UserRefreshToken> GetRefreshToken(string refreshToken) { 
            
            var dbToken= await _miscRepository.GetRefreshTokenAsync(refreshToken);

            return _mapper.Map<UserRefreshToken>(dbToken);
        
        }
        
        public async Task DeleteRfreshToken(string refreshToken) {

            await _miscRepository.DeleteRefreshTokenAsync(refreshToken);
        
        }

    }
}
