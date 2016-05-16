using System.ComponentModel.DataAnnotations;

namespace PersistXML.Entities
{
    public class PatientTest
    {
        [Key]
        public int Id { get; set; }
      
        public string PasNumber { get; set; }

        public string Forenames { get; set; }
    }
}