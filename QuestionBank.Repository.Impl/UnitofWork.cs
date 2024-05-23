using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using QuestionBank.Repository.Interface;

namespace QuestionBank.Repository.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        private IDbContextTransaction _dbContextTransaction;

        
        private bool _disposed;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void ChangeDbConnectionString(string connectionString)
        {
            _dbContext.Database.GetDbConnection().ConnectionString = connectionString;
        }
        public void ChangeCommandTimeOut(int timeout)
        {
            _dbContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(timeout));
        }

        public int Complete()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            

            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                //_eventLogger?.Error("DbUpdate concurrency exception", dbUpdateConcurrencyException);
                throw;
            }
            catch (DbUpdateException dbUpdateException)
            {
                //_eventLogger?.Error("Error occurred while adding or updating entity", dbUpdateException);
                throw;
            }
            catch (NotSupportedException notSupportedException)
            {
                //_eventLogger?.Error("Not supported exception", notSupportedException);
                throw;
            }
            catch (ObjectDisposedException objectDisposedException)
            {
                //_eventLogger?.Error("Object dispose exception occurred", objectDisposedException);
                throw;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                //_eventLogger?.Error("Invalid operation exception", invalidOperationException);
                throw;
            }

        }

        public void BeginTransaction(IsolationLevel isolationLevel=IsolationLevel.ReadCommitted)
        {
            _dbContextTransaction=_dbContext.Database.BeginTransaction(isolationLevel);
        }

        public void CommitTrans()
        {
            _dbContextTransaction.Commit();
            _dbContextTransaction.Dispose();
        }
        public void RollBackTrans()
        {
            _dbContextTransaction.Rollback();
            _dbContextTransaction.Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _dbContext.Dispose();
                _dbContextTransaction?.Dispose();

            }

            _disposed = true;
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken=default)
        {
            try
            {
                return await _dbContext.SaveChangesAsync(cancellationToken);
            }


            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                //_eventLogger?.Error("DbUpdate concurrency exception", dbUpdateConcurrencyException);
                throw;
            }
            catch (DbUpdateException dbUpdateException)
            {
                //_eventLogger?.Error("Error occurred while adding or updating entity", dbUpdateException);
                throw;
            }
            catch (NotSupportedException notSupportedException)
            {
                //_eventLogger?.Error("Not supported exception", notSupportedException);
                throw;
            }
            catch (ObjectDisposedException objectDisposedException)
            {
                //_eventLogger?.Error("Object dispose exception occurred", objectDisposedException);
                throw;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                //_eventLogger?.Error("Invalid operation exception", invalidOperationException);
                throw;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

    }
}
