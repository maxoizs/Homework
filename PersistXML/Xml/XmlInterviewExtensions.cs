using System;
using System.Linq;
using PersistXML.Entities;

namespace PersistXML.Xml
{
    public static class XmlInterviewExtensions
    {
        public static PatientInterview ToPatientInterview(this XmlInterview xmlInterview)
        {
            var resultInterview = new PatientInterview();

            foreach (var item in xmlInterview.Items)
            {
                if (item is XmlHeader)
                {
                    var header = (XmlHeader)item;
                    resultInterview.TransactionId = header.TransactionId;
                    resultInterview.TransactionTime = DateTime.Parse(header.TransactionTime);
                }
                if (item is XmlPatients)
                {
                    var patients = ((XmlPatients)item).Patient;
                    foreach (var patient in patients)
                    {
                        resultInterview.Patients.Add(patient.ToPatient());
                    }
                }
            }
            return resultInterview;
        }

        public static Patient ToPatient(this XmlPatient xmlPatient)
        {
            var patient = xmlPatient.Basic.First().ToPatientDetials();
            patient.GpDetails = xmlPatient.GpDetails.First().ToGpDetails();
            patient.NextOfKin = xmlPatient.NextOfKin.First().ToNextOfKin();

            return patient;
        }

        public static Patient ToPatientDetials(this XmlPatientBasic xmlPatient)
        {
            return new Patient
                {
                    DateOfBirth = DateTime.Parse(xmlPatient.DateOfBirth),
                    Forenames = xmlPatient.Forenames,
                    PasNumber = xmlPatient.PasNumber,
                    HomeTelephoneNumber = xmlPatient.HomeTelephoneNumber,
                    Surname = xmlPatient.Surname,
                    SexCode = xmlPatient.SexCode
                };
        }

        public static GPDetails ToGpDetails(this XmlGpDetails xmlGpDetails)
        {
            return new GPDetails
                {
                    Code = xmlGpDetails.GpCode,
                    Initials = xmlGpDetails.GpInitials,
                    Phone = xmlGpDetails.GpPhone,
                    Surname = xmlGpDetails.GpSurname
                };
        }

        public static NextOfKin ToNextOfKin(this XmlNextOfKin xmlNextOfKin)
        {
            return new NextOfKin
            {
                Name = xmlNextOfKin.NokName,
                RelationshipCode = xmlNextOfKin.NokRelationshipCode,
                AddressLine1 = xmlNextOfKin.NokAddressLine1,
                AddressLine2 = xmlNextOfKin.NokAddressLine2,
                AddressLine3 = xmlNextOfKin.NokAddressLine3,
                AddressLine4 = xmlNextOfKin.NokAddressLine4,
                Postcode = xmlNextOfKin.NokPostcode
            };
        }

    }
}
