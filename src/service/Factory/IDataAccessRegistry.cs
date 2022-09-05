namespace Endor.DataAccess.Factory
{
  public interface IDataAccessRegistry
  {
    IDataAccess GetDataAccess();
  }
}