﻿using System.Data.SqlClient;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using QuestionBank.Repository.Interface;


namespace QuestionBank.Repository.Impl
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly DbContext Context;
        private readonly DbSet<TEntity> _entities;

        public Repository(DbContext context)
        {

            Context = context;
            _entities = context.Set<TEntity>();
            
            
        }

        public virtual IQueryable<TEntity> Data => _entities;

        public TEntity Get(TKey id)
        {
            return _entities.Find(id);
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            return await _entities.FindAsync(id);
        }


        public IEnumerable<TEntity> GetAll()
        {
            return _entities;
        }

        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return ApplyIncludesOnQuery(_entities, includeProperties);
        }
        
        public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await ApplyIncludesOnQuery(_entities, includeProperties).ToListAsync();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }




        //private IEnumerable<IEnumerable<TSource>> ChunkData<TSource>(IEnumerable<TSource> source, int chunkSize)
        //{
        //    for (int i = 0; i < source.Count(); i += chunkSize)
        //        yield return source.Skip(i).Take(chunkSize);
        //}

        private IEnumerable<IEnumerable<TSource>> ChunkDataInternal<TSource>(IQueryable<TSource> source, int chunkSize)
        {
            for (int i = 0; i < source.Count(); i += chunkSize)
                yield return source.Skip(i).Take(chunkSize);
        }
        public IEnumerable<TEntity> FindInChunk<TSortedBy>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TSortedBy>> orderby,int chunkSize = 500, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _entities.Where(predicate);

            if (includeProperties != null)
            {
                query = ApplyIncludesOnQuery(query, includeProperties);
            }
            query = query.OrderBy(orderby);

            return ChunkDataInternal(query, chunkSize).SelectMany(_ => _);

        }
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _entities.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate).AsQueryable();
            }

            if (includeProperties != null)
            {
                query = ApplyIncludesOnQuery(query, includeProperties);
            }

            return query;
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.CountAsync(predicate);

        } 
        public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.LongCountAsync(predicate);

        }
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _entities.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate).AsQueryable();
            }

            if (includeProperties != null)
            {
                query = ApplyIncludesOnQuery(query, includeProperties);
            }

            return await query.ToListAsync();
        }

        public IQueryable<TEntity> ApplyIncludesOnQuery(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            // Return Applied Includes query
            return (includeProperties.Aggregate(query, (current, include) => current.Include(include)));
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.FirstOrDefault(predicate);
        }

        public async Task<TEntity> SingleOrDefaultAysnc(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.SingleOrDefaultAsync(predicate);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.FirstOrDefaultAsync(predicate);
        }

        public void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _entities.AddAsync(entity, cancellationToken);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void Remove<TOEntity>(TOEntity entity) where TOEntity : class
        {
            GetEntity<TOEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {

            _entities.RemoveRange(entities);
        }

        public void RemoveRangeOther<TOEntity>(IEnumerable<TOEntity> entities) where TOEntity : class

        {
            GetEntity<TOEntity>().RemoveRange(entities);
        }

        public void Attach(TEntity entity)
        {
            var entry = Context.Entry(entity);

            if (entry.State == EntityState.Detached)
                _entities.Attach(entity);

        }

        public void AttachEntity<TOEntity>(TOEntity otherEntity) where TOEntity : class
        {
            var entry = Context.Entry(otherEntity);
            //entry.State = EntityState.Unchanged;
            if (entry.State == EntityState.Detached)
                Context.Set<TOEntity>().Attach(otherEntity);

        }

        public void UpdateMinimal<TAnotherEntity>(TAnotherEntity entity,
                                           params Expression<Func<TAnotherEntity, object>>[] propsToBeUpdated) where TAnotherEntity : class
        {            
            
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                Context.Set<TAnotherEntity>().Attach(entity);
            }

            if (propsToBeUpdated.Length > 0)
            {
                foreach (var property in propsToBeUpdated)
                {
                    entry.Property(property).IsModified = true;
                }
            }
        }

        public void Update(TEntity entity, params Expression<Func<TEntity, object>>[] propsToBeExcluded)
        {
            Attach(entity);
            var entry = Context.Entry(entity);
            entry.State = EntityState.Modified;

            if (propsToBeExcluded.Length > 0)
            {
                foreach (var property in propsToBeExcluded)
                {
                    entry.Property(property).IsModified = false;
                }
            }
        }

        public void UpdateOther<TOtherEntity>(TOtherEntity entity, params Expression<Func<TOtherEntity, object>>[] propsToBeExcluded) where TOtherEntity : class
        {
            
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                Context.Set<TOtherEntity>().Attach(entity);
            }

            entry.State = EntityState.Modified;

            if (propsToBeExcluded.Length > 0)
            {
                foreach (var property in propsToBeExcluded)
                {
                    entry.Property(property).IsModified = false;
                }
            }
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Any(predicate);
        }
        public bool All(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.All(predicate);
        }

        public void ExecuteRawSql(string sql, params object[] parameters)
        {
            if (parameters?.Length == 0)
            {
                Context.Database.ExecuteSqlRaw(sql);
                return;
            }

            Context.Database.ExecuteSqlRaw(sql, parameters);
        }

        public  IEnumerable<IEnumerable<TSource>> ChunkData<TSource>(IQueryable<TSource> source, int chunkSize)
        {
            for (int i = 0; i < source.Count(); i += chunkSize)
                yield return source.Skip(i).Take(chunkSize);
        }

        public IEnumerable<TOther> ExecuteSqlQuery<TOther>(string sql, params object[] parameters) 
        {
            return Context.Database.SqlQueryRaw<TOther>(sql, parameters);

        }
        public IEnumerable<TOther> ExecuteSqlQuery<TOther>(string sql, SqlParameter[] parameters) 
        {
            return Context.Database.SqlQueryRaw<TOther>(sql, parameters);

        }
        public IEnumerable<TOther> ExecuteProcedure<TOther>(string procedureName, List<SqlParameter> parameters) where TOther : class
        {
            var sql = $"{procedureName} {string.Join(",", parameters.Select(param => param.ParameterName))}";
            return Context.Database.SqlQueryRaw<TOther>(sql, parameters.ToArray());

        }
        public void ExecuteProcedure(string procedureName, List<SqlParameter> parameters)
        {
            var sql = $"{procedureName} {string.Join(",", parameters.Select(param => param.ParameterName))}";
            Context.Database.ExecuteSqlRaw(sql, parameters.ToArray());

        }

        public DbSet<TOther> GetEntity<TOther>() where TOther : class
        {
            return Context.Set<TOther>();
        }

       
    }
}