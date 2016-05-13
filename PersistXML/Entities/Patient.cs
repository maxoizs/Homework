using System;

namespace PersistXML.Entities
{
  public class Patient:IEntityWithId {
    public int Id { get; set; }

    public string PasNumber { get; set; }

    public string Forenames { get; set; }

    public string Surname { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string SexCode { get; set; }

    public string HomeTelephoneNumber { get; set; }

    public NextOfKin NextOfKin { get; set; }

    public GpDetails GpDetails { get; set; }
  }
}