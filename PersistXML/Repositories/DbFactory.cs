using System.Data.Entity.Infrastructure;

namespace PersistXML.Repositories {
  public class DbFactory: IDbContextFactory<Database> {
    public Database Create() {
      return new Database( "Test" );
    }
  }
}