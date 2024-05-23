using AutoMapper;
using Microsoft.Extensions.Logging;
using QuestionBank.Common.Enumeration;
using QuestionBank.Model.Domain;
using QuestionBank.Repository.Interface;
using QuestionBank.Service.Interface;
using QuestionBank.Utility;

namespace QuestionBank.Service.Impl
{
    public class UserAccountService :BaseService, IUserAccountService
    {
        private readonly IMapper _mapper;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserAccountService> _logger;
        private readonly IMiscRepository _miscRepository;

        public UserAccountService(IMapper mapper, IUserAccountRepository userAccountRepository, IUnitOfWork unitOfWork, ILogger<UserAccountService> logger, IMiscRepository miscRepository):base(logger,unitOfWork)
        {
            _mapper = mapper;
            _userAccountRepository = userAccountRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _miscRepository = miscRepository;
        }

        public async Task RegisterNewUserAsync(UserAccount userAccount)
        {
            _logger.LogDebug("Executing service in service method");
            var dbEntity = _mapper.Map<Persistence.Entity.UserAccount>(userAccount);

            dbEntity.Password = CryptoHelper.GetPasswordHash(userAccount.Password);
            dbEntity.RegisteredOn = DateTime.UtcNow;
            dbEntity.AccountStatus = Common.Enumeration.UserAccountStatus.Active;

            await _userAccountRepository.AddAsync(dbEntity);

            if (userAccount.TagIds.Any())
            {
                var userTags = userAccount.TagIds.Select(_ => new Persistence.Entity.UserTag()
                {
                    TagId = _,
                    User = dbEntity
                });
                await _userAccountRepository.AddUserTagsAsync(userTags);
            }

            await _unitOfWork.CompleteAsync();

            _logger.LogDebug("Service Executed");
        }

        public async Task<UserAccount> GetUserInfoByIdAsync(long id)
        {
            _logger.LogDebug("Executing UserAccountService in service method");
            return _mapper.Map<UserAccount>((await _userAccountRepository.FindAsync(_ => _.Id == id, _ => _.Person)).First());
        }

        public async Task<IEnumerable<UserAccount>> GetAllReviewers()
        {
            var dbResult = await _userAccountRepository.FindAsync(_ => _.RoleId == (int)UserRole.Reviewer, _ => _.Person);

            return dbResult.Select(_mapper.Map<UserAccount>);
        }

        public async Task<IEnumerable<UserAccount>> GetAllApprovers()
        {
            var dbResult = await _userAccountRepository.FindAsync(_ => _.RoleId == (int)UserRole.Approver, _ => _.Person);

            return dbResult.Select(_mapper.Map<UserAccount>);
        }

        public async Task<IEnumerable<UserAccount>> GetAllUsersAsync()
        {
            var dbResult = await _userAccountRepository.GetAllAsync(_ => _.Person, _ => _.SkillsTags);
            return dbResult.Select(_mapper.Map<UserAccount>);
        }

        public async Task<UserAccount> GetUserByIdAsync(long userId)
        {
            var dbResult = (await _userAccountRepository.FindAsync(_ => _.Id == userId, _ => _.Person, _ => _.SkillsTags)).Select(_mapper.Map<UserAccount>).First();
            dbResult.TagIds = dbResult.SkillsTags.Select(x => x.Id).ToList();
            return dbResult;
        }

        public async Task UpdateUserAsync(UserAccount userAccount)
        {
            _logger.LogDebug("Executing service in service method");

            var dbEntity = (await _userAccountRepository.FindAsync(_ => _.Id == userAccount.Id, _ => _.Person)).First();
            dbEntity.CellNo = userAccount.CellNo;
            dbEntity.Email = userAccount.Email;
            dbEntity.RoleId = userAccount.RoleId;
            dbEntity.Person.FirstName = userAccount.Person.FirstName;
            dbEntity.Person.MiddleName = userAccount.Person.MiddleName;
            dbEntity.Person.LastName = userAccount.Person.LastName;
            dbEntity.Person.Address = userAccount.Person.Address;
            dbEntity.Person.DOB = userAccount.Person.DOB;

            await _userAccountRepository.DeleteAllTagsFromUserAsync(userAccount.Id);

            if (userAccount.TagIds.Any())
            {
                var userTags = userAccount.TagIds.Select(_ => new Persistence.Entity.UserTag()
                {
                    TagId = _,
                    User = dbEntity
                });
                await _userAccountRepository.AddUserTagsAsync(userTags);
            }

            await _unitOfWork.CompleteAsync();

            _logger.LogDebug("Service Executed");
        }

        public async Task<bool> ResetUserPasswordAsync(long userId, string currentPassword, string newPassword)
        {
            var dbEntity = (await _userAccountRepository.FindAsync(_ => _.Id == userId, _ => _.Person)).First();
            if (CryptoHelper.VerifyPassword(currentPassword, dbEntity.Password))
            {
                dbEntity.Password = CryptoHelper.GetPasswordHash(newPassword);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            else return false;
        }

        public async Task UploadUserImage(long userId, string image)
        {
            var dbEntity = (await _userAccountRepository.FindAsync(_ => _.Id == userId, _ => _.Person)).First();
            dbEntity.Person.Photo = image;
            await _unitOfWork.CompleteAsync();
        }

        public async Task<string?> GetUserImage(long userId)
        {
            var dbEntity = (await _userAccountRepository.FindAsync(_ => _.Id == userId, _ => _.Person)).First();
            return dbEntity.Person?.Photo;
        }
    }
}
