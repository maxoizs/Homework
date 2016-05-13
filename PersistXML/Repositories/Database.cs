using System.Data.Entity;
using PersistXML.Entities;
using PersistXML.Xml;

namespace PersistXML.Repositories {
  public class Database: DbContext {
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Interview> Interviews { get; set; }
    public DbSet<GpDetails> GpDetails { get; set; }
    public DbSet<NextOfKin> NextOfKins { get; set; }

    public Database( string connectionString )
      : base( connectionString ) {

    }
  }
}
