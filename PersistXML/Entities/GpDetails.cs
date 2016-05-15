using System.ComponentModel.DataAnnotations;

namespace PersistXML.Entities {
  public class GpDetails {
    [Key]
    public string GpCode { get; set; }

    public string GpSurname { get; set; }

    public string GpInitials { get; set; }

    public string GpPhone { get; set; }
  }
}