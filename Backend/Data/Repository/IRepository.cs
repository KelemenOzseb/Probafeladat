using Entity.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IRepository<T> where T : class, IIdentity
    {
        Task<T> Create(T entity);
        Task DeleteById(string id);
        Task<T?> GetOne(string id);
        IQueryable<T> GetAll();
        Task<T> Update(T entity);
    }
}
