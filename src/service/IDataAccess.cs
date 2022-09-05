using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using Endor.DataAccess.Providers;

namespace Endor.DataAccess
{
  public interface IDataAccess
  {
    ConnectionProvider GetProvider();

    IList<T> ExecuteQuery<T>(string query);

    IEnumerable<T> GetAll<T>() where T : class;

    T GetById<T>(dynamic id) where T : class;

    void Insert<T>(ref T entity) where T : class;

    bool Update<T>(T entity) where T : class;

    bool Delete<T>(dynamic id) where T : class;

    IEnumerable<T> GetList<T>(object predicate) where T : class;

    T Get<T>(object predicate) where T : class;
  }
}