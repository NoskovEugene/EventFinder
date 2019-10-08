using Microsoft.Extensions.Options;
using EventFinder.Configs.DbConfig;


namespace EventFinder.Configs
{
    public class RuntimeConfiguration : IRuntimeConfiguration
    {
        private readonly IOptions<ConnectionString> _connectionString;

        public RuntimeConfiguration(IOptions<ConnectionString> ConnectionString)
        {
            this._connectionString = ConnectionString;
        }



        public string ConnectionString => _connectionString.Value.DefaultConnection;
    }
}