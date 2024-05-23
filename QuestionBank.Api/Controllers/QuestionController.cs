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
    public class QuestionController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IQuestionService _questionService;
        private readonly IUnitOfWork _unitOfWork;

        public QuestionController(IMapper mapper, IQuestionService questionService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _questionService = questionService;
            _unitOfWork = unitOfWork;
        }

        // GET: api/<QuestionController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data=await _questionService.GetAllMergedQuestion();

            return Ok(data.Select(_mapper.Map<QustionTableApiModel>));
        }
        [HttpGet("user/status/{questionStatus}")]
        public async Task<IActionResult> Get(QuestionStatus questionStatus)
        {
            var data = await _questionService.GetAllQuestionByStatusAndUserId(questionStatus,UserSessionModel?.UserId??0);

            return Ok(data.Select(_mapper.Map<QustionTableApiModel>));
        }        

        // GET api/<QuestionController>/5
        [HttpGet("{questionId}")]
        public async Task<IActionResult> Get(long questionId)
        {
            var data = await _questionService.GetQuestion(questionId);

            return Ok(_mapper.Map<QuestionViewApiModel>(data));

        }
        [HttpGet("user/view/{questionId}")]
        public async Task<IActionResult> GetOwnQuestionForDisplay(long questionId)
        {
            var data = await _questionService.GetQuestion(questionId);

            return Ok(_mapper.Map<QuestionViewApiModel>(data));

        }

        [HttpGet("user/edit/{questionId}")]
        public async Task<IActionResult> GetOwnQuestionForEdit(long questionId)
        {
            var data = await _questionService.GetQuestion(questionId);

            return Ok(_mapper.Map<QuestionApiModel>(data));

        }
        [HttpPost("user/edit")]
        public async Task<IActionResult> uppdateOwnQuestion([FromBody] QuestionApiModel questionApiModel)
        {
            var questionDomain = _mapper.Map<Question>(questionApiModel);
            questionDomain.UserId = UserSessionModel.UserId;
            
            await _questionService.UpdateQuestion(questionDomain);            
            
            return Ok(_mapper.Map<ResponseBase>(_questionService.ServiceResult));

        }
        // POST api/<QuestionController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] QuestionApiModel questionApiModel)
        {
            var domainModel = _mapper.Map<Question>(questionApiModel);

            domainModel.UserId = UserSessionModel.UserId;

            await _questionService.AddNewQuestion(domainModel);

            await _unitOfWork.CompleteAsync();

            return Ok(new ResponseBase());

        }

        // PUT api/<QuestionController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] QuestionApiModel questionApiModel)
        {
            var domainModel = _mapper.Map<Question>(questionApiModel);

            domainModel.UserId = 5;

            await _questionService.UpdateQuestion(domainModel);

            await _unitOfWork.CompleteAsync();

            return Ok(new ResponseBase());
        }

        // DELETE api/<QuestionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
