using System.Data;

namespace QuestionBank.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void ChangeDbConnectionString(string connectionString);
        int Complete();

        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
        void BeginTransaction(IsolationLevel isolationLevel=IsolationLevel.ReadCommitted);

        void CommitTrans();
        void RollBackTrans();
        void ChangeCommandTimeOut(int timeout);
    }
}