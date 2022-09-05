using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Dapper;
using DapperExtensions;
using Endor.DataAccess.Providers;
using Microsoft.Extensions.Logging;

namespace Endor.DataAccess
{
	public class DataAccess : IDataAccess
	{
		#region Fields
		/// <summary>
		/// The custom logger
		/// </summary>
		private ILogger logger;
		#endregion

		#region Properties
		public ConnectionProvider provider { get; set; }
		#endregion

		#region Constructor
		public DataAccess(ILogger logger, ConnectionProvider connectionProvider)
		{
			this.logger = logger;

			provider = connectionProvider;
		}
		#endregion

		#region Interface Implementation
		public ConnectionProvider GetProvider()
		{
			return provider;
		}

		public virtual IList<T> ExecuteQuery<T>(string query)
		{
			using (var conn = provider.GetDbConnection())
			{
				return conn.Query<T>(query).ToList();
			}
		}

		public virtual IEnumerable<T> GetAll<T>() where T : class
		{
			using (var db = provider.GetDbConnection())
			{
				return db.GetList<T>();
			}
		}

		public virtual T GetById<T>(dynamic id) where T : class
		{
			using (SqlConnection db = (SqlConnection)provider.GetDbConnection())
			{
				return db.Get<T>((object)id);
			}
		}

		public virtual void Insert<T>(ref T entity) where T : class
		{
			using (var db = provider.GetDbConnection())
			{
				var id = db.Insert(entity);

				entity = GetById<T>(id);
			}
		}

		public virtual bool Update<T>(T entity) where T : class
		{
			using (var db = provider.GetDbConnection())
			{
				return db.Update(entity);
			}
		}

		public virtual bool Delete<T>(dynamic id) where T : class
		{
			using (var db = provider.GetDbConnection())
			{
				var entity = GetById<T>((object)id);

				if (entity == null) throw new Exception("Entity not Found");

				return db.Delete(entity);
			}
		}

		public virtual IEnumerable<T> GetList<T>(object predicate) where T : class
		{
			using (var db = provider.GetDbConnection())
			{
				return db.GetList<T>(predicate);
			}
		}

		public T Get<T>(object predicate) where T : class
		{
			using (var db = provider.GetDbConnection())
			{
				return db.Get<T>(predicate);
			}
		}

		#endregion

	}
}
