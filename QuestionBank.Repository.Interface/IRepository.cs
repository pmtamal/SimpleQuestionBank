using System.Data.SqlClient;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace QuestionBank.Repository.Interface
{
    public interface IRepository<TEntity, Tkey> where TEntity : class
    {
        TEntity Get(Tkey id);

        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleOrDefaultAysnc(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        void Attach(TEntity entity);

        void Update(TEntity entity, params Expression<Func<TEntity, object>>[] propsToBeExcluded);
        void UpdateMinimal<TAnotherEntity>(TAnotherEntity entity, params Expression<Func<TAnotherEntity, object>>[] propsToBeUpdated) where TAnotherEntity : class;

        bool Any(Expression<Func<TEntity, bool>> predicate);

        void Remove<TOEntity>(TOEntity entity) where TOEntity : class;
        void RemoveRangeOther<TOEntity>(IEnumerable<TOEntity> entities) where TOEntity : class;
        void AttachEntity<TOEntity>(TOEntity otherEntity) where TOEntity : class;
        void ExecuteRawSql(string sql, params object[] parameters);
        IEnumerable<TOther> ExecuteSqlQuery<TOther>(string sql, params object[] parameters);
        IEnumerable<TOther> ExecuteSqlQuery<TOther>(string sql, SqlParameter[] parameters);
        IEnumerable<TOther> ExecuteProcedure<TOther>(string procedureName, List<SqlParameter> parameters) where TOther : class;
        DbSet<TOther> GetEntity<TOther>() where TOther : class;
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> FindInChunk<TSortedBy>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TSortedBy>> orderBy, int chunkSize = 500, params Expression<Func<TEntity, object>>[] includeProperties);
        bool All(Expression<Func<TEntity, bool>> predicate);
        void UpdateOther<TOtherEntity>(TOtherEntity entity, params Expression<Func<TOtherEntity, object>>[] propsToBeExcluded) where TOtherEntity : class;
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetAsync(Tkey id);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);
    }
}