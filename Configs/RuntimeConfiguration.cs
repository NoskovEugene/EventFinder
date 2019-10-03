using Microsoft.Extensions.Options;

namespace EventFinder.Configs
{
    public class RuntimeConfiguration : IRuntimeConfiguration
    {
        private readonly IOptions<DbConfig.ConnectionString> _connectionString;

        public RuntimeConfiguration(IOptions<DbConfig.ConnectionString> ConnectionString)
        {
            this._connectionString = ConnectionString;
        }



        public string ConnectionString => _connectionString.Value.DefaultConnection;
    }
}