
using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using QuestionBank.Api.ClientEnum;
using QuestionBank.Api.Utility;
using QuestionBank.Common;
using QuestionBank.Model.Api;
using QuestionBank.Model.Domain;
using QuestionBank.Repository.Interface;
using QuestionBank.Service.Interface;
using QuestionBank.Utility;


namespace QuestionBank.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseController
{
    private const string RefreshTokenKeyInCookie = "refreshToken";
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStringLocalizer<ClientErrorMessage> _apiClientErrorMessageLocalizer;
    private readonly AppSettings _appSettings;

    // GET
    public AuthController(IMapper mapper, IAuthService authService, ILogger<AuthController> logger,
        IOptions<AppSettings> options, IUnitOfWork unitOfWork, IStringLocalizer<ClientErrorMessage> stringLocalizer
        )
    {
        _mapper = mapper;
        _authService = authService;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _apiClientErrorMessageLocalizer = stringLocalizer;
        _appSettings = options.Value;
    }



    [HttpPost("SignIn")]
    [Microsoft.AspNetCore.Authorization.AllowAnonymous]
    public async Task<IActionResult> SignIn(LoginRequestApiModel loginRequestApiModel)
    {

        var user = await _authService.AuthenticateUserAsync(loginRequestApiModel.UserName, loginRequestApiModel.Password);

        if (user == null)
        {
            return Ok(GetErrorResponse((int)ApiErrorCode.UserNameOrPasswordInvalid, _apiClientErrorMessageLocalizer[ApiErrorCode.UserNameOrPasswordInvalid.ToString()]));

        }

        var token = await JWTHelper.GenerateJwtToken(_appSettings.JwtSecretKey, new Dictionary<string, string>()
        {
            {"Id",user.Id.ToString()},
            { "Email",user.Email }
        }, DateTime.Now.AddMinutes(15));

        //Response.Cookies.Append(RefreshTokenKeyInCookie, token.RefreshToken, new CookieOptions
        //{
        //    HttpOnly = true,
        //    Expires = DateTime.UtcNow.AddDays(7) // Example: Refresh token expires in 7 days
        //});

        await _authService.AddRefreshToken(new UserRefreshToken() { UserName = user.Email, RefreshToken = token.RefreshToken });

        await _unitOfWork.CompleteAsync();

        return Ok(new TokenApiModel { AccessToken = token.AccessToken, RefreshToken = token.RefreshToken });
    }

    [HttpPost("refreshtoken")]
    [Microsoft.AspNetCore.Authorization.AllowAnonymous]
    public async Task<IActionResult> Refresh(TokenApiModel tokenApiModel)
    {
        //var refreshToken = Request.Cookies[RefreshTokenKeyInCookie];
        if (string.IsNullOrEmpty(tokenApiModel.AccessToken) || string.IsNullOrEmpty(tokenApiModel.RefreshToken))
        {
            return Unauthorized();
        }

        var storedRefreshToken = await _authService.GetRefreshToken(tokenApiModel.RefreshToken);

        if (storedRefreshToken == null)
        {

            return Unauthorized();
        }

        try
        {
            var principal = await JWTHelper.GetClaimsFromToken(_appSettings.JwtSecretKey, tokenApiModel.AccessToken, false);
            if (principal == null)
            {
                return Unauthorized();
            }

            await _authService.DeleteRfreshToken(tokenApiModel.RefreshToken);

            var sessionModel = SessionHelper.GetUserSessionModelFromClaim(principal);

            var token = await JWTHelper.GenerateJwtToken(_appSettings.JwtSecretKey, new Dictionary<string, string>()
            {
                {"Id",sessionModel.UserId.ToString() },
                { "Email",sessionModel.UserEmail }
            }, DateTime.Now.AddDays(7));

            await _authService.AddRefreshToken(new UserRefreshToken() { UserName = sessionModel.UserEmail, RefreshToken = token.RefreshToken });

            //Response.Cookies.Append(RefreshTokenKeyInCookie, token.RefreshToken, new CookieOptions
            //{
            //    HttpOnly = true,
            //    Expires = DateTime.UtcNow.AddMinutes(60) // Example: Refresh token expires in 7 days
            //});

            await _unitOfWork.CompleteAsync();

            return Ok(new TokenApiModel { AccessToken = token.AccessToken, RefreshToken = token.RefreshToken });
        }
        catch (Exception)
        {

            return Unauthorized();
        }
    }
}