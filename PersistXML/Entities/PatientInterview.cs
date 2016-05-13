using System;
using System.Collections.Generic;

namespace PersistXML.Entities {
  public class PatientInterview {

    public string TransactionId { get; set; }

    public DateTime TransactionTime { get; set; }

    public ICollection<Patient> Patients { get; set; }
  }
}
