using Microsoft.Extensions.Logging;
using QuestionBank.Common.Enumeration;
using QuestionBank.Model.Domain;
using QuestionBank.Repository.Interface;
using QuestionBank.Service.Interface;

namespace QuestionBank.Service.Impl
{
    public class BaseService : IBaseService
    {
        private ServiceResult _serviceResult;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public BaseService(ILogger  logger,IUnitOfWork unitOfWork)
        {
            _serviceResult = new ServiceResult();
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public ServiceResult ServiceResult => _serviceResult;

        public void SetServiceError(ServiceError serviceError)
        {
            _serviceResult.HasError = true;
            _serviceResult.ErrorCode = serviceError;
            _serviceResult.ErrorMessage=serviceError.ToString();
        }

        public async Task ApplyDbChanges()
        {
            try
            {
                await _unitOfWork.CompleteAsync();   
            }
            catch (Exception e)
            {
                _logger.LogDebug(e, "Error occured while updating changes in database");
                _serviceResult.HasError = true;
                _serviceResult.ErrorCode = ServiceError.InternalServerError;
                _serviceResult.ErrorMessage = ServiceError.InternalServerError.ToString();
                
            }
        }

    }
}
