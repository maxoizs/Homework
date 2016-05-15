using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistXML.Entities
{
    public class Patient 
    {
        [Key]
        public int Id { get; set; }
      
        public string PasNumber { get; set; }

        public string Forenames { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string SexCode { get; set; }

        public string HomeTelephoneNumber { get; set; }

        public int NextOfKinId { get; set; }

        public virtual NextOfKin NextOfKin { get; set; }

        public string GpCode { get; set; }

        [ForeignKey("GpCode")]
        public virtual GPDetails GpDetails { get; set; }
    }
}