using AutoMapper;
using Microsoft.Extensions.Logging;
using QuestionBank.Model.Domain;
using QuestionBank.Repository.Interface;
using QuestionBank.Service.Interface;

namespace QuestionBank.Service.Impl
{
    public class SettingService : BaseService, ISettingService
    {
        private readonly IMapper _mapper;
        private readonly IMiscRepository _miscRepository;

        public SettingService(IMapper mapper, IMiscRepository miscRepository, ILogger<QuestionService> logger, IUnitOfWork unitOfWork) : base(logger, unitOfWork)
        {
            _mapper = mapper;
            _miscRepository = miscRepository;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            var dbRoles = await _miscRepository.GetRoles();
            var roles = dbRoles.Select(_mapper.Map<Role>);
            return roles;

        }
    }
}
