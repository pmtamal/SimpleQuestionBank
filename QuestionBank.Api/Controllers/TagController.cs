using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuestionBank.Model.Api;
using QuestionBank.Repository.Interface;
using QuestionBank.Service.Interface;

namespace QuestionBank.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITagService _tagService;
        private readonly IQuestionCategoryService _questionCategoryService;
        private readonly IUnitOfWork _unitOfWork;

        public TagController(IMapper mapper, ITagService tagService)
        {
            _mapper = mapper;
            _tagService = tagService;
        }

        



        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var data = await _tagService.GetSkillsTagsAsync();

            return Ok(data.Select(_mapper.Map<SkillsTagApiModel>));

        }





    }
}
