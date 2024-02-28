﻿using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int id);

        Task<T> GetByIdAsync(int id);

        IEnumerable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        //------------------------------------------------------------------------------------------------
        T Find(Expression<Func<T, bool>> criteria, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool isNoTracking = false);

        Task<T> FindAsync(Expression<Func<T, bool>> criteria, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool isNoTracking = false);

        IEnumerable<T> FindAll(
            Expression<Func<T, bool>> criteria,
            int? take = null, int? skip = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool isNoTracking = false
        );

        Task<IEnumerable<T>> FindAllAsync(
            Expression<Func<T, bool>> criteria,
            int? take = null, int? skip = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool isNoTracking = false
        );


        IQueryable<T> FindByQuery(
            Expression<Func<T, bool>> criteria,
            int? take = null, int? skip = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool isNoTracking = false
        );

        //-------------------------------------------------------------------
        T Add(T entity);

        Task<T> AddAsync(T entity);

        IEnumerable<T> AddRange(IEnumerable<T> entities);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        //-------------------------------------------------------------------
        bool IsExist(Expression<Func<T, bool>> criteria);

        //-------------------------------------------------------------------

        //public int UpdateAll(Expression<Func<T, bool>> criteria,
        //    Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression = null);

        T Update(T entity);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);

        void Attach(T entity);

        void AttachRange(IEnumerable<T> entities);

        void DeAttach(T entity);

        //-------------------------------------------------------------------
        int Count();

        int Count(Expression<Func<T, bool>> criteria);

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<T, bool>> criteria);
    }
}
