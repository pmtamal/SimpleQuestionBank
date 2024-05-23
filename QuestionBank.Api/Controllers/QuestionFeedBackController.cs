using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using QuestionBank.Api.CustomAttribute;
using QuestionBank.Common.Enumeration;
using QuestionBank.Model.Api;
using QuestionBank.Model.Domain;
using QuestionBank.Repository.Interface;
using QuestionBank.Service.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuestionBank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionFeedBackController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IQuestionFeedBackService _questionFeedBackService;
        private readonly IUnitOfWork _unitOfWork;

        public QuestionFeedBackController(IMapper mapper, IQuestionFeedBackService questionFeedBackService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _questionFeedBackService = questionFeedBackService;
            _unitOfWork = unitOfWork;
        }

        // GET: api/<QuestionFeedBackController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("review/status/{reviewStatus}")]
        public async Task<IActionResult> Get(ReviewStatus reviewStatus)
        {
            var data = await _questionFeedBackService.GetAllQuestionForReviewByStatusAndUserId(reviewStatus, UserSessionModel.UserId);

            return Ok(data.Select(_mapper.Map<QuestionReviewTableApiModel>));
        }

        [HttpGet("approval/status/{reviewStatus}")]
        public async Task<IActionResult> GetAllApproverQuestion(ReviewStatus reviewStatus)
        {
            var data = await _questionFeedBackService.GetAllQuestionForApproverByStatusAndUserId(reviewStatus, UserSessionModel.UserId);

            return Ok(data.Select(_mapper.Map<QuestionReviewTableApiModel>));
        }

        // POST api/<QuestionFeedBackController>
        [HttpPost]
        public async Task<IActionResult> AddFeedBack([FromBody] QuestionFeedBackApiModel questionFeedBackApiModel)
        {
            var domainModel = _mapper.Map<QuestionFeedBack>(questionFeedBackApiModel);

            domainModel.UserId = 6;

            await _questionFeedBackService.AddQuestionFeedBack(domainModel);

            await _unitOfWork.CompleteAsync();

            return Ok(new ResponseBase());
        }

        [HttpPut("review")]
        public async Task<IActionResult> AddOrUpdateFeedBack([FromBody] QuestionFeedBackApiModel questionFeedBackApiModel)
        {
            var domainModel = _mapper.Map<QuestionFeedBack>(questionFeedBackApiModel);

            domainModel.UserId = UserSessionModel.UserId;

            await _questionFeedBackService.UpdateOrInsertQuestionFeedBack(domainModel);

            await _unitOfWork.CompleteAsync();

            return Ok(new ResponseBase());
        }

        [HttpPost("comment")]
        public async Task<IActionResult> AddFeedBackComment([FromBody] QuestionFeedBackCommentApiModel questionFeedBackApiModel)
        {
            var domainModel = _mapper.Map<QuestionFeedBackComment>(questionFeedBackApiModel);

            domainModel.UserId = UserSessionModel.UserId;

            var updatedComment = await _questionFeedBackService.AddFeedBackComment(domainModel);



            return Ok(_mapper.Map<QuestionFeedBackCommentApiModel>(updatedComment));
        }
        [HttpPut("comment")]
        public async Task<IActionResult> UpdateFeedBackComment([FromBody] QuestionFeedBackCommentApiModel questionFeedBackApiModel)
        {
            var domainModel = _mapper.Map<QuestionFeedBackComment>(questionFeedBackApiModel);

            domainModel.UserId = UserSessionModel.UserId;   

            await  _questionFeedBackService.UpdateFeedBackComment(domainModel);



            return Ok(new ResponseBase());
        }
        [HttpPut("comment/resolve/{id}")]
        public async Task<IActionResult> UpdateFeedBackComment(long id)
        {
            

            await _questionFeedBackService.ResolveComment(id);



            return Ok(new ResponseBase());
        }

        [HttpDelete("comment/{id}")]
        public async Task<IActionResult> DeleteFeedBackComment(long id)
        {

            await _questionFeedBackService.DeleteFeedBackComment(id, UserSessionModel.UserId);

            return Ok(new ResponseBase());
        }

        [HttpGet("comment/{questionId}")]
        public async Task<IActionResult> GetComments(long questionId)
        {

            var data=await _questionFeedBackService.GetFeedBackCommentsByUser(UserSessionModel.UserId,questionId);

            return Ok(data.Select(_mapper.Map<QuestionFeedBackCommentApiModel>));
        }

        // PUT api/<QuestionFeedBackController>/5
        
        // DELETE api/<QuestionFeedBackController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
