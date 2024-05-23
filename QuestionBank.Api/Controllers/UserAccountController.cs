
using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QuestionBank.Api.CustomAttribute;
using QuestionBank.Common;
using QuestionBank.Model.Api;
using QuestionBank.Model.Domain;
using QuestionBank.Service.Interface;


namespace QuestionBank.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserAccountController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IUserAccountService _userAccountService;
    private readonly ILogger<UserAccountController> _logger;

    private readonly AppSettings _appSettings;

    // GET
    public UserAccountController(IMapper mapper, IUserAccountService userAccountService, ILogger<UserAccountController> logger, IOptions<AppSettings> options)
    {
        _mapper = mapper;
        _userAccountService = userAccountService;
        _logger = logger;
        _appSettings = options.Value;
    }

    [HttpPost]
    //[Authorize]
    public async Task<IActionResult> Register([FromBody] UserAccountApiModel accountApiModel)
    {


        _logger.LogDebug("Executing user account api model");

        var userAccount = _mapper.Map<UserAccount>(accountApiModel);

        await _userAccountService.RegisterNewUserAsync(userAccount);

        _logger.LogDebug("Executing user account api model executed");

        return Ok(new ResponseBase());



    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateUser([FromBody] UserAccountApiModel accountApiModel)
    {
        if (accountApiModel.Id <= 0) return BadRequest();

        _logger.LogDebug("Executing user account api model");

        var userAccount = _mapper.Map<UserAccount>(accountApiModel);

        await _userAccountService.UpdateUserAsync(userAccount);

        _logger.LogDebug("Executing user account api model executed");

        return Ok(new ResponseBase());

    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUser()
    {
        var userId = UserSessionModel?.UserId ?? 0;
        _logger.LogDebug("Executing user account api model executed");
        return Ok(_mapper.Map<UserInfoApiModel>(await _userAccountService.GetUserInfoByIdAsync(userId)));
    }
    [HttpGet("reviewers")]
    //[Authorize]
    public async Task<IActionResult> GetAllReviewer()
    {
        _logger.LogDebug("Executing user account api model executed");
        var reviewers = await _userAccountService.GetAllReviewers();
        return Ok(reviewers.Select(_mapper.Map<UserInfoApiModel>));
    }
    [HttpGet("approvers")]
    //[Authorize]
    public async Task<IActionResult> GetAllApprover()
    {
        _logger.LogDebug("Executing user account api model executed");
        var reviewers = await _userAccountService.GetAllApprovers();
        return Ok(reviewers.Select(_mapper.Map<UserInfoApiModel>));
    }

    [HttpGet("users")]
    //[Authorize]
    public async Task<IActionResult> GetAllUsers()
    {
        _logger.LogDebug("Executing user account api model executed");
        var reviewers = await _userAccountService.GetAllUsersAsync();
        return Ok(reviewers.Select(_mapper.Map<UserInfoApiModel>));
    }

    [HttpGet("{userId}")]
    //[Authorize]
    public async Task<IActionResult> GetUserAccount(long userId)
    {
        _logger.LogDebug("Executing user account api model executed");
        var reviewers = await _userAccountService.GetUserByIdAsync(userId);
        return Ok(_mapper.Map<UserAccountApiModel>(reviewers));
    }

    [HttpPatch("resetpassword")]
    [Authorize]
    public async Task<IActionResult> ResetUserPassword([FromBody] PasswordModel password)
    {
        var userId = UserSessionModel?.UserId ?? 0;
        if (userId == 0) return BadRequest();
        _logger.LogDebug("Executing user account api model executed");
        if(await _userAccountService.ResetUserPasswordAsync(userId, password.CurrentPassword, password.NewPassword))
        {
            return Ok(new ResponseBase());
        }
        return BadRequest();
    }

    [HttpPost("UploadImage")]
    [Authorize]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile image)
    {
        var userId = UserSessionModel?.UserId ?? 0;
        if (userId == 0) return BadRequest();

        if (image == null || image.Length == 0)
            return BadRequest("No image uploaded.");

        using (var memoryStream = new MemoryStream())
        {
            await image.CopyToAsync(memoryStream);
            byte[] bytes = memoryStream.ToArray();

            // Convert byte array to base64 string
            string base64String = Convert.ToBase64String(bytes);

            await _userAccountService.UploadUserImage(userId,base64String);

            return Ok(new ResponseBase());
        }
    }


    [HttpGet("GetUserImage")]
    [Authorize]
    public async Task<IActionResult> GetUserImage()
    {
        var userId = UserSessionModel?.UserId ?? 0;
        if (userId <= 0)
            return BadRequest();

        var base64ImageData = await _userAccountService.GetUserImage(userId);

        if (string.IsNullOrEmpty(base64ImageData))
            return NotFound();

        return Ok("data:image/png;base64,"+base64ImageData);
    }


}