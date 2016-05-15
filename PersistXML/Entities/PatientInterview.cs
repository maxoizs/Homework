using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersistXML.Entities
{
    public class PatientInterview
    {

        public PatientInterview()
        {
            Patients = new List<Patient>();
        }

        [Key]
        public string TransactionId { get; set; }

        public DateTime TransactionTime { get; set; }

        public List<Patient> Patients { get; set; }
    }
}
