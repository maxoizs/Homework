using System.ComponentModel.DataAnnotations;

namespace PersistXML.Entities
{
    public class GpDetails
    {
        [Key]
        public string Code { get; set; }

        public string Surname { get; set; }

        public string Initials { get; set; }

        public string Phone { get; set; }
    }
}