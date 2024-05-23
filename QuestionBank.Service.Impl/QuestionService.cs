using AutoMapper;
using Microsoft.Extensions.Logging;
using QuestionBank.Common.Enumeration;
using QuestionBank.Model.Domain;
using QuestionBank.Repository.Interface;
using QuestionBank.Service.Interface;

namespace QuestionBank.Service.Impl
{
    public class QuestionService :BaseService, IQuestionService
    {
        private readonly IMapper _mapper;
        private readonly IQuestionRepository _questionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQuestionFeedBackRepository _questionFeedBackRepository;

        public QuestionService(IMapper mapper, IQuestionRepository questionRepository, ILogger<QuestionService> logger, IUnitOfWork unitOfWork,IQuestionFeedBackRepository questionFeedBackRepository) : base(logger, unitOfWork)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
            _unitOfWork = unitOfWork;
            _questionFeedBackRepository = questionFeedBackRepository;
        }

        public async Task AddNewQuestion(Question questionBank)

        {
            
            questionBank.CreatedOn = DateTime.UtcNow;            
            questionBank.LastUpdatedOn = DateTime.UtcNow;            

            var questionEntity = _mapper.Map<Persistence.Entity.Question>(questionBank);

            await _questionRepository.AddAsync(questionEntity);

            if (questionBank.SkillsTagIds.Any())
            {
                var questionTags = questionBank.SkillsTagIds.Select(skillId => new Persistence.Entity.QuestionTag()
                {

                    TagId = skillId,
                    Question = questionEntity,                  


                });

                await _questionRepository.AddQuestionTags(questionTags);

            }


        }

        public async Task<IEnumerable<Question>> GetAllMergedQuestion()
        {
            var dbData = await _questionRepository.GetAllQuestionByStatusWithUserInfo(Common.Enumeration.QuestionStatus.Finalized);

            return dbData.Select(_mapper.Map<Question>);

        }
        public async Task<IEnumerable<Question>> GetAllQuestionByStatusAndUserId(QuestionStatus questionStatus,long userId)
        {
            var dbData = await _questionRepository.GetAllQuestionByStatusAndUserId(questionStatus,userId);

            return dbData.Select(_mapper.Map<Question>);

        }

        //public async Task<IEnumerable<Question>> GetAllReviewQuestionByStatusAndUserId(ReviewStatus questionStatus, long userId)
        //{
        //    var dbData = await _questionRepository.GetAllQuestionByStatusAndUserId(questionStatus, userId);

        //    return dbData.Select(_mapper.Map<Question>);

        //}

        public async Task<Question> GetQuestion(long questionId)
        {
            var dbEntity = await _questionRepository.GetQuestionWithTags(questionId);


            return _mapper.Map<Question>(dbEntity);

        }

        public async Task UpdateQuestion(Question questionBank)
        {

            var stillUnResolved = await _questionFeedBackRepository.AreCommentsStillUnResolved(questionBank.Id);

            if(stillUnResolved)
            {
                SetServiceError(ServiceError.ResolvePendingComment);
                return;
            }

            questionBank.LastUpdatedOn=DateTime.UtcNow;

            var questionEntity = _mapper.Map<Persistence.Entity.Question>(questionBank);

            _questionRepository.Update(questionEntity,_=>_.CreatedOn,_=>_.FinalizedOn);
            await _questionRepository.DeleteAllTagsFromQuestion(questionBank.Id);

            if (questionBank.SkillsTagIds.Any())
            {
                var questionTags = questionBank.SkillsTagIds.Select(skillId => new Persistence.Entity.QuestionTag()
                {

                    TagId = skillId,
                    Question = questionEntity

                });

                await _questionRepository.AddQuestionTags(questionTags);

                await ApplyDbChanges();
                

            }

        }
    }
}
