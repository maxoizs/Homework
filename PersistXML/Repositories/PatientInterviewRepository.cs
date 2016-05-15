using PersistXML.Entities;

namespace PersistXML.Repositories
{
    public class PatientInterviewRepository : Repository<PatientInterview>
    {
        private readonly Repository<Patient> _patientRepository;

        public PatientInterviewRepository(DbFactory dbContextFactory, Repository<Patient> patientRepository)
            : base(dbContextFactory)
        {
            _patientRepository = patientRepository;
        }

        public override void Save(params PatientInterview[] interviews)
        {
            foreach (var interview in interviews)
            {
                _patientRepository.Save(interview.Patients.ToArray());
            }
            base.Save(interviews);

        }
    }
}
