using System.Linq;
using PersistXML.Entities;

namespace PersistXML.Repositories
{
    public class PatientRepository : Repository<Patient>
    {
        private readonly Repository<GPDetails> _gpRepository;

        public PatientRepository(DbFactory dbContextFactory, Repository<GPDetails> gbRepository)
            : base(dbContextFactory)
        {
            _gpRepository = gbRepository;                    
        }

        public override void Save(params Patient[] patients)
        {
            foreach (var patient in patients.Where(p => p.GpDetails != null))
            {
                // Since I'm using GPCode to identify GPs I have to sepreatly save them 
                // same for patients and nextOfKin it they has a unique key rather than the Id. 
                HandleGPDetails(patient);
            }
            base.Save(patients);
        }

        /// <summary>
        /// Save <see cref="GPDetails"/> and remove it from the parent object
        /// </summary>
        /// <param name="patient"></param>
        private void HandleGPDetails(Patient patient)
        {
            var gpDetails = patient.GpDetails;
            patient.GpCode = gpDetails.Code;
            _gpRepository.Save(gpDetails);
            patient.GpDetails = null;
        }
    }
}
