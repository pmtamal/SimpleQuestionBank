using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuestionBank.Model.Api;
using QuestionBank.Model.Domain;
using QuestionBank.Repository.Interface;
using QuestionBank.Service.Interface;

namespace QuestionBank.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionCategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IQuestionCategoryService _questionCategoryService;
        private readonly IUnitOfWork _unitOfWork;

        public QuestionCategoryController(IMapper mapper, IQuestionCategoryService questionCategoryService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _questionCategoryService = questionCategoryService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(QuestionCategoryApiModel questionCategoryApiModel)
        {

            var domainQuestonCatagory = _mapper.Map<QuestionCategory>(questionCategoryApiModel);

            await _questionCategoryService.AddCatagory(domainQuestonCatagory);

            await _unitOfWork.CompleteAsync();

            return Ok(new ResponseBase());


        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(QuestionCategoryApiModel questionCategoryApiModel)
        {

            var domainQuestonCatagory = _mapper.Map<QuestionCategory>(questionCategoryApiModel);

            await _questionCategoryService.UpdateCategory(domainQuestonCatagory);

            await _unitOfWork.CompleteAsync();

            return Ok(new ResponseBase());

        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {


            var allDomainCat = await _questionCategoryService.GetAllQuestionCategory();



            return Ok(allDomainCat.Select(_mapper.Map<QuestionCategoryTableApiModel>));

        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById(long categoryId)
        {


            var allDomainCat = await _questionCategoryService.GetQuestionCategoryById(categoryId);



            return Ok(_mapper.Map<QuestionCategoryApiModel>(allDomainCat));

        }



        [HttpGet("tags")]
        public async Task<IActionResult> GetSkillsTag()
        {


            return Ok(await _questionCategoryService.GetQuestionSkills());

        }





    }
}
