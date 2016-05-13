using System;
using System.Collections.Generic;

namespace PersistXML.Entities {
  public class PatientInterview {

      public PatientInterview()
      {
          Patients = new List<Patient>();
      }

    public string TransactionId { get; set; }

    public DateTime TransactionTime { get; set; }

    public List<Patient> Patients { get; set; }
  }
}
