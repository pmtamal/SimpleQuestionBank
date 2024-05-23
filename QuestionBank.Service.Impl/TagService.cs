using AutoMapper;
using Microsoft.Extensions.Logging;
using QuestionBank.Model.Domain;
using QuestionBank.Repository.Interface;
using QuestionBank.Service.Interface;

namespace QuestionBank.Service.Impl
{
    public class TagService :BaseService, ITagService
    {
        private readonly IMapper _mapper;
        private readonly IMiscRepository _miscRepository;

        public TagService(IMapper mapper,IMiscRepository miscRepository, ILogger<QuestionService> logger, IUnitOfWork unitOfWork) : base(logger, unitOfWork)
        {
            _mapper = mapper;
            _miscRepository = miscRepository;
        }

        public async Task<IEnumerable<SkillsTag>> GetSkillsTagsAsync()
        {
            var skillsTag = await _miscRepository.GetAllSkillsTag();

            return  skillsTag.Select(_mapper.Map<SkillsTag>);
        }
    }
}
