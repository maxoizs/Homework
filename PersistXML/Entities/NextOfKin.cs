using System.ComponentModel.DataAnnotations;

namespace PersistXML.Entities
{
    public class NextOfKin
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string RelationshipCode { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string AddressLine4 { get; set; }

        public string Postcode { get; set; }
    }
}