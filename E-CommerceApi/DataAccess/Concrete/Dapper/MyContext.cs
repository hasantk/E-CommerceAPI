using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Dapper
{
    //DefaultConnection
    public class MyContext : IDisposable
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _connection;

        public MyContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    string connectionString = _configuration.GetConnectionString("DefaultConnection");
                    _connection = new MySqlConnection(connectionString);
                }
                return _connection;
            }
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
