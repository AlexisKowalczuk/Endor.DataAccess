namespace Endor.DataAccess.Config
{
  public class DatabaseConfiguration
  {
    public string ConnectionString { get; set; }

    public string ProviderName { get; set; }

    public string[] MappersAssemblies { get; set; }

  }
}