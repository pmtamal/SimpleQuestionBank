using AutoMapper;
using Microsoft.Extensions.Logging;
using QuestionBank.Model.Domain;
using QuestionBank.Repository.Interface;
using QuestionBank.Service.Interface;

namespace QuestionBank.Service.Impl
{
    public class QuestionCategoryService :BaseService, IQuestionCategoryService
    {
        private readonly IMapper _mapper;
        private readonly IQuestionCategoryRepository _questionCategoryRepository;
        private readonly IMiscRepository _miscRepository;

        public QuestionCategoryService(IMapper mapper, IQuestionCategoryRepository questionCategoryRepository,
            IMiscRepository miscRepository,ILogger<QuestionCategoryService> logger,IUnitOfWork unitOfWork):base(logger,unitOfWork)
        {
            _mapper = mapper;
            _questionCategoryRepository = questionCategoryRepository;
            _miscRepository = miscRepository;
        }

        public async Task AddCatagory(QuestionCategory questionCategory)
        {
            var dbQuestionCategory = _mapper.Map<Persistence.Entity.QuestionCategory>(questionCategory);


            _questionCategoryRepository.Add(dbQuestionCategory);

            var categoryReviewer = questionCategory.ReviewerUesrIds == null || !questionCategory.ReviewerUesrIds.Any() ? Enumerable.Empty<Persistence.Entity.QuestionCategoryUserAction>() : questionCategory.ReviewerUesrIds.Select(reviewrUserIds => new Persistence.Entity.QuestionCategoryUserAction()
            {
                UserId = reviewrUserIds,
                QuestionCategory = dbQuestionCategory,
                CategoryUserAction = Common.Enumeration.CategoryUserAction.Review

            });

            var categoryApprover = questionCategory.ReviewerUesrIds == null || !questionCategory.ApproverUesrIds.Any() ? Enumerable.Empty<Persistence.Entity.QuestionCategoryUserAction>() : questionCategory.ApproverUesrIds.Select(approverUserId => new Persistence.Entity.QuestionCategoryUserAction()
            {
                UserId = approverUserId,
                QuestionCategory = dbQuestionCategory,
                CategoryUserAction = Common.Enumeration.CategoryUserAction.Approve

            });

            var categroyActionMaker = categoryApprover.Concat(categoryReviewer);

            await _questionCategoryRepository.AddQuestionCatagoryUserActions(categroyActionMaker);


        }

        public async Task<IEnumerable<QuestionCategory>> GetAllQuestionCategory()
        {
            var allCategories = await _questionCategoryRepository.GetAllCategoryWithUserInfo();

            return allCategories.Select(_mapper.Map<QuestionCategory>);
        }

        public async Task<QuestionCategory> GetQuestionCategoryById(long questionCategoryId)
        {
            var questionCategory = await _questionCategoryRepository.GetCategoryWithUserInfo(questionCategoryId);

            return _mapper.Map<QuestionCategory>(questionCategory);
        }

        public async Task<IEnumerable<SkillsTag>> GetQuestionSkills()
        {
            var skillsTag = await _miscRepository.GetAllSkillsTag();

            return skillsTag.Select(_mapper.Map<SkillsTag>);
        }

        public async Task UpdateCategory(QuestionCategory questionCategory)
        {
            var dbQuestionCategory = _mapper.Map<Persistence.Entity.QuestionCategory>(questionCategory);

            _questionCategoryRepository.Update(dbQuestionCategory);

            await _questionCategoryRepository.DeleteAllUserActionFromCategory(dbQuestionCategory.Id);

            var categoryReviewer = questionCategory.ReviewerUesrIds == null || !questionCategory.ReviewerUesrIds.Any() ? Enumerable.Empty<Persistence.Entity.QuestionCategoryUserAction>() : questionCategory.ReviewerUesrIds.Select(reviewrUserIds => new Persistence.Entity.QuestionCategoryUserAction()
            {
                UserId = reviewrUserIds,
                QuestionCategoryId = questionCategory.Id,
                CategoryUserAction = Common.Enumeration.CategoryUserAction.Review

            });

            var categoryApprover = questionCategory.ReviewerUesrIds == null || !questionCategory.ReviewerUesrIds.Any() ? Enumerable.Empty<Persistence.Entity.QuestionCategoryUserAction>() : questionCategory.ApproverUesrIds.Select(approverUserId => new Persistence.Entity.QuestionCategoryUserAction()
            {
                UserId = approverUserId,
                QuestionCategoryId = questionCategory.Id,
                CategoryUserAction = Common.Enumeration.CategoryUserAction.Approve

            });

            var categroyActionMaker = categoryApprover.Concat(categoryReviewer);

            if (categoryApprover.Any())

                await _questionCategoryRepository.AddQuestionCatagoryUserActions(categroyActionMaker);


        }


    }
}
