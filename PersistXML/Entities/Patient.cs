using System;
using System.ComponentModel.DataAnnotations;

namespace PersistXML.Entities
{
    public class Patient 
    {
        [Key]
        public int Id { get; set; }

        public int TransactionId { get; set; }

        public virtual PatientInterview Interview { get; set; }

        public string PasNumber { get; set; }

        public string Forenames { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string SexCode { get; set; }

        public string HomeTelephoneNumber { get; set; }

        public int NextOfKinId { get; set; }

        public virtual NextOfKin NextOfKin { get; set; }

        public int GpDetailsId { get; set; }

        public virtual GpDetails GpDetails { get; set; }
    }
}