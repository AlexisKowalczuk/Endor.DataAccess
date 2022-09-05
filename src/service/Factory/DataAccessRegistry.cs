using System.Collections.Generic;
using System.Reflection;
using Endor.DataAccess.Config;
using Endor.DataAccess.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Endor.DataAccess.Factory
{
	public class DataAccessRegistry : IDataAccessRegistry
	{
		#region Fields
		/// <summary>
		/// The custom logger
		/// </summary>
		private ILogger<DataAccessRegistry> logger;
		#endregion

		#region Properties
		public DatabaseConfiguration config { get; set; }
		#endregion

		#region Constructor
		public DataAccessRegistry(ILogger<DataAccessRegistry> logger, IConfiguration configuration)
		{
			this.logger = logger;

			config = new DatabaseConfiguration();
			configuration.GetSection("DatabaseConfiguration").Bind(config);

			//FluentMapper.Initialize(c => { c.For Dommel(); });
			var assemblies = new List<Assembly>();
			foreach (string assemblyName in config.MappersAssemblies)
			{
				assemblies.Add(Assembly.Load(new AssemblyName(assemblyName)));
			}

			DapperExtensions.DapperExtensions.SetMappingAssemblies(assemblies);
		}
		#endregion

		#region Interface Implementation
		public IDataAccess GetDataAccess()
		{
			var provider = new ConnectionProvider(logger, config);

			return new DataAccess(logger, provider);
		}
		#endregion

	}
}
