using Microsoft.AspNetCore.Mvc;
using QuestionBank.Model.Api;

namespace QuestionBank.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        

        public UserSessionModel? UserSessionModel
        {
            get
            {

                return HttpContext.Items["User"] as UserSessionModel;

            }
        }

        
        protected ResponseBase GetErrorResponse(int errorCode,string errorMessage) {

            return new ResponseBase()
            {

                HasError = true,
                ErrorCode = errorCode,
                ErrorMessage = errorMessage

            };
        
        }



    }
}
