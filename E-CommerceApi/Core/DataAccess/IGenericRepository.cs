using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IGenericRepository<T> where T : class,IEntity,new()
    {
        Task<IEnumerable<T>> GetAll(string tableName);
        Task<T> GetById(string tableName, int id);
        Task Add(string tableName ,T entity);
        Task Update (string tableName, T entity);
        Task Delete (string tableName, int id);
    }
}
