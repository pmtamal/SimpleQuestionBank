using AutoMapper;
using Microsoft.Extensions.Logging;
using QuestionBank.Common.Enumeration;
using QuestionBank.Model.Domain;
using QuestionBank.Repository.Interface;
using QuestionBank.Service.Interface;

namespace QuestionBank.Service.Impl
{
    public class QuestionFeedBackService :BaseService, IQuestionFeedBackService
    {
        private readonly IMapper _mapper;
        private readonly IQuestionFeedBackRepository _questionFeedBackRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public QuestionFeedBackService(IMapper mapper, IQuestionFeedBackRepository questionFeedBackRepository, 
            IQuestionRepository questionRepository,ILogger<QuestionFeedBackService> logger, IUnitOfWork unitOfWork) : base(logger, unitOfWork)
        {
            _mapper = mapper;
            _questionFeedBackRepository = questionFeedBackRepository;
            _questionRepository = questionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<QuestionFeedBackComment> AddFeedBackComment(QuestionFeedBackComment questionFeedBack)
        {
            var dbEntity = _mapper.Map<Persistence.Entity.QuestionFeedBackComment>(questionFeedBack);
            dbEntity.CommentSubmittedOn = DateTime.UtcNow;
            dbEntity.CommentStatus = Common.Enumeration.QuestionCommentStatus.WaitingForResolve;
            await _questionFeedBackRepository.AddQuestionFeedBackComment(dbEntity);
            await _unitOfWork.CompleteAsync();
            var savedComment = await _questionFeedBackRepository.GetQuestionFeedBackCommentAsync(dbEntity.Id);
            var qjDomain = _mapper.Map<QuestionFeedBackComment>(savedComment);
            qjDomain.IsEditableByUser = true;
            return qjDomain;
        }

        public async Task UpdateFeedBackComment(QuestionFeedBackComment questionFeedBackComment)
        {

            var dbComment = await _questionFeedBackRepository.GetQuestionFeedBackCommentAsync(questionFeedBackComment.Id);



            dbComment.CommentSubmittedOn = DateTime.UtcNow;
            dbComment.Comment = questionFeedBackComment.Comment;
            await _unitOfWork.CompleteAsync();

        }

        public async Task DeleteFeedBackComment(long id, long userId)
        {


            await _questionFeedBackRepository.DeleteQuestionFeedBackComment(id, userId);

            await _unitOfWork.CompleteAsync();

        }

        public async Task<IEnumerable<QuestionFeedBackComment>> GetFeedBackCommentsByUser(long userId, long questionId)
        {

            var allCommentByUser = await _questionFeedBackRepository.GetQuestionFeedBackCommentsByQuestionId(userId, questionId);
            
            var allCommentsOfQuestion = allCommentByUser.Select(_mapper.Map<QuestionFeedBackComment>).ToList();
            allCommentsOfQuestion.ForEach(_ => {
                _.IsEditableByUser = _.UserId == userId&&_.CommentStatus==QuestionCommentStatus.WaitingForResolve;
                
            });

            return allCommentsOfQuestion;



        }

        public async Task AddQuestionFeedBack(QuestionFeedBack questionFeedBack)
        {

            questionFeedBack.LastFeedBackSubmittedOn = DateTime.UtcNow;



            var dbEntityFeedBack = _mapper.Map<Persistence.Entity.QuestionFeedBack>(questionFeedBack);

            await _questionFeedBackRepository.AddAsync(dbEntityFeedBack);


            if (questionFeedBack.QuestionFeedBackComments.Any())
            {

                var dbEntityFeedBackComment = questionFeedBack.QuestionFeedBackComments.Select(fm =>
                {

                    fm.CommentSubmittedOn = DateTime.UtcNow;
                    fm.CommentStatus = Common.Enumeration.QuestionCommentStatus.WaitingForResolve;
                    fm.QuestionId = questionFeedBack.QuestionId;
                    fm.UserId = questionFeedBack.UserId;
                    return _mapper.Map<Persistence.Entity.QuestionFeedBackComment>(fm);


                });

                await _questionFeedBackRepository.AddQuestionFeedBackComments(dbEntityFeedBackComment);
            }




        }

        public async Task UpdateOrInsertQuestionFeedBack(QuestionFeedBack questionFeedBack)
        {
            var dbEntity = await _questionFeedBackRepository.SingleOrDefaultAysnc(_ => _.QuestionId == questionFeedBack.QuestionId && _.UserId == questionFeedBack.UserId&& !_.IsFeedBackCycleCompleted);

            var additional = 0;
            if (dbEntity == null)
            {
                questionFeedBack.LastFeedBackSubmittedOn = DateTime.UtcNow;

                await _questionFeedBackRepository.AddAsync(_mapper.Map<Persistence.Entity.QuestionFeedBack>(questionFeedBack));
                additional = 1;
            }
            else
            {

                dbEntity.LastFeedBackSubmittedOn = DateTime.UtcNow;
                dbEntity.FeedBackType = questionFeedBack.FeedBackType;
            }




            var question = (await _questionRepository.FindAsync(_ => _.Id == questionFeedBack.QuestionId, _ => _.QuestionCategory)).SingleOrDefault();

            if (questionFeedBack.FeedBackType == QuestionFeedBackType.MergeByApprover)
            {
                question.status = QuestionStatus.Finalized;
                _questionFeedBackRepository.CompleteFeedbackCycle(questionFeedBack.QuestionId);
            }
            else if(questionFeedBack.FeedBackType == QuestionFeedBackType.SendForRevision)
            {
                question.status = QuestionStatus.InRevision;
                await _questionFeedBackRepository.CompleteFeedbackCycle(questionFeedBack.QuestionId);
            }
            else if (questionFeedBack.FeedBackType == QuestionFeedBackType.AcceptedByReviewer|| questionFeedBack.FeedBackType == QuestionFeedBackType.RevisionRequiredByReviewer)
            {
                var reviewCount = await _questionFeedBackRepository.CountAsync(_ =>
                (_.FeedBackType == QuestionFeedBackType.RevisionRequiredByReviewer
                || _.FeedBackType == QuestionFeedBackType.AcceptedByReviewer) &&_.QuestionId==questionFeedBack.QuestionId&&!_.IsFeedBackCycleCompleted);

                if (question.QuestionCategory.MinNoOfReviewers <= ((reviewCount+additional)))
                {
                    question.status = QuestionStatus.InApproval;
                }
            }

            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Question>> GetAllQuestionForReviewByStatusAndUserId(ReviewStatus reviewStatus, long userId)
        {
            var dbEnttities = await _questionFeedBackRepository.GetAllQuestionForReviewByStatusAndUserId(reviewStatus, userId, QuestionStatus.InReview, CategoryUserAction.Review);

            return dbEnttities.Select(_mapper.Map<Question>);

        }

        public async Task<IEnumerable<Question>> GetAllQuestionForApproverByStatusAndUserId(ReviewStatus reviewStatus, long userId)
        {
            var dbEnttities = await _questionFeedBackRepository.GetAllQuestionForReviewByStatusAndUserId(reviewStatus, userId, QuestionStatus.InApproval, CategoryUserAction.Approve);

            return dbEnttities.Select(_mapper.Map<Question>);

        }

        public async Task ResolveComment(long id)
        {
            var comment =await _questionFeedBackRepository.GetQuestionFeedBackCommentAsync(id);
            comment.CommentStatus = QuestionCommentStatus.Resolved;
            await ApplyDbChanges();
        }
    }
}

