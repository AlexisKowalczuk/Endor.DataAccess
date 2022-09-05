using System.Data.Common;
using System.Data.SqlClient;
using Endor.DataAccess.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Endor.DataAccess.Providers
{
  public class ConnectionProvider
  {
    #region Fields
    /// <summary>
    /// The custom logger
    /// </summary>
    private ILogger logger;

    private DatabaseConfiguration config;
    #endregion

    #region Constructor
    public ConnectionProvider(ILogger logger, DatabaseConfiguration configuration)
    {
      this.logger = logger;

      config = configuration;
    }
    #endregion

    public DbConnection GetDbConnection()
    {
      if (config.ProviderName == nameof(System.Data.SqlClient))
      {
        var conn = new SqlConnection(config.ConnectionString);
        return conn;
      }

      return null;
    }
  }
}
