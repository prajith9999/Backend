using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace vueproject_asp.Data
{
    public class DapperDbContext
    {
        private readonly string? _connectionString;  // Nullable connection string

        public DapperDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("DefaultConnection", "Connection string not found.");
        }

        // This method will provide a connection to the database
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString!);  // Use '!' to assure the compiler it's not null
        }
    }
}
