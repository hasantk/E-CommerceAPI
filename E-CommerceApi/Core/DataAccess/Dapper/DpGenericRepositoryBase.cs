using Core.Entities.Abstract;
using Dapper;
using DataAccess.Abstract;
using DataAccess.Concrete.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Core.DataAccess.Dapper
{
    public class DpGenericRepositoryBase<TEntity, TContext> : IGenericRepository<TEntity> 
        where TEntity : class, IEntity, new()
        where TContext : IDapperContext, new()
    {
        private readonly DapperContext _context;

        //public DpGenericRepositoryBase(DapperContext context)
        //{
        //    _context = context;
        //}

        public async Task Add(string _TableName, TEntity _Entity)
        {
            using (TContext context = new TContext()) 
            {
                var _EntityTypOf = typeof(TEntity);
                var _GetProperties = _EntityTypOf.GetProperties().Where(x =>x.Name !="Id");
                DynamicParameters _DynamicParameters = new();

                foreach (var property in _GetProperties)
                {
                    var value = property.GetValue(_Entity);
                    _DynamicParameters.Add("@" + property.Name, value);
                }
                var idProperty = _EntityTypOf.GetProperty("Id");
                if (idProperty != null)
                {
                    string query = $"Insert Into {_TableName} ({string.Join(",",_GetProperties.Select(p=>p.Name))})"
                        +$"Values({string.Join(",",_GetProperties.Select(p=> "@"+p.Name))})";

                    await connection.ExecuteAsync(query, _DynamicParameters);
                }
                else
                {
                    throw new ArgumentException("Varlığın Bir 'Id' Özelliğine Sahip Olması Gerekir.");
                }
            }
        }

        public async Task Delete(string tableName, int id)
        {
            using (TContext context=new TContext())
            {
                string query = $"Select * From {tableName}Where Id = @Id";
                await context.ExecuteAsync(query, new { Id = id });
            }
        }

        public async Task<IEnumerable<TEntity>> GetAll(string tableName)
        {
            using (var connection = _context.CreateConnection()) 
            {
                string query = $"Select * From {tableName}";
                return await connection.QueryAsync<TEntity>(query); 
            }
        }

        public async Task<TEntity> GetById(string tableName, int id)
        {
            using (var connection = _context.CreateConnection())
            {
                string query = $"Select * From {tableName} Where Id = @Id";
                return await connection.QuerySingleOrDefaultAsync<TEntity>(query,new {Id=id});
            }
        }

        public async Task Update(string _TableName, TEntity _Entity)
        {
            using (var connection = _context.CreateConnection())
            {
                var _EntityTypOf=typeof(TEntity);
                var _GetProperties = _EntityTypOf.GetProperties();
                DynamicParameters _DynamicParameters = new();

                foreach (var property in _GetProperties)
                {
                    var value = property.GetValue(_Entity);
                    _DynamicParameters.Add("@"+property.Name,value);
                }
                var idProperty = _EntityTypOf.GetProperty("Id");
                if (idProperty != null) 
                {
                    string query = $"Update * From {_TableName} SET {string.Join(",", _GetProperties
                        .Where(p=>p.Name != "Id").Select(p=>p.Name+"=@"+p.Name))}"+$"Where Id=@Id";

                    await connection.ExecuteAsync(query,_DynamicParameters);
                }
                else
                {
                    throw new ArgumentException("Varlığın Bir 'Id' Özelliğine Sahip Olması Gerekir.");
                }
            }
        }
    }
}
