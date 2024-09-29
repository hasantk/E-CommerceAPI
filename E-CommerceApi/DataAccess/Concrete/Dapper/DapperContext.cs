using DataAccess.Abstract;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Dapper
{
    //DefaultConnection
    public class DapperContext : IDapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connString = _configuration.GetConnectionString("DefaultConnection");
        }
        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connString);
        }

        public void Dispose()
        {
            if (_connString != null)
            {
                _connString.Dispose();
                _connString = null;
            }
        }

        //public DapperContext(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        //public IDbConnection Connection
        //{
        //    get
        //    {
        //        if (_connection == null)
        //        {
        //            string connectionString = _configuration.GetConnectionString("DefaultConnection");
        //            _connection = new MySqlConnection(connectionString);
        //        }
        //        return _connection;
        //    }
        //}

    

    }
}
