using PersistXML.Entities;

namespace PersistXML.Repositories {
  public class PatientRepository: Repository<Patient> {
      public PatientRepository(DbFactory dbContextFactory) : base(dbContextFactory)
      {
      }
  }
}
