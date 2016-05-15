using System.Data.Entity;
using PersistXML.Entities;

namespace PersistXML.Repositories
{
    public class Database : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<GPDetails> GpDetails { get; set; }
        public DbSet<NextOfKin> NextOfKins { get; set; }
        public DbSet<PatientInterview> Interviews { get; set; }

        public Database(string connectionString)
            : base(connectionString)
        {

        }
    }
}
