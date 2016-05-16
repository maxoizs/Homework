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
                var xmlHeader = item as XmlHeader;
                if (xmlHeader != null)
                {
                    var header = xmlHeader;
                    resultInterview.TransactionId = header.TransactionId;
                    resultInterview.TransactionTime = DateTime.Parse(header.TransactionTime);
                }

                var xmlPatients = item as XmlPatients;
                if (xmlPatients != null)
                {
                    var patients = xmlPatients.Patient;
                    foreach (var patient in patients)
                    {
                        resultInterview.Patients.Add(patient.ToPatient());
                    }
                }
            }
            return resultInterview;
        }

        /// <summary>
        /// Convert <see cref="XmlPatient"/> to <see cref="Patient"/>
        /// Will convert it's <see cref="XmlPatientBasic"/> if exists
        /// Will convert it's <see cref="XmlGpDetails"/> if exists
        /// Will convert it's <see cref="XmlNextOfKin"/> if exists
        /// </summary>
        /// <returns>Returns a full <see cref="Patient"/>, and returns empty one if basic element is not exists</returns>
        public static Patient ToPatient(this XmlPatient xmlPatient)
        {
            if (!xmlPatient.Basic.Any())
            {
                return new Patient();
            }
            var patient = xmlPatient.Basic.First().ToPatientDetials();
            patient.GpDetails = xmlPatient.GpDetails.Any()? xmlPatient.GpDetails.First().ToGpDetails(): null;
            patient.NextOfKin = xmlPatient.NextOfKin.Any()? xmlPatient.NextOfKin.First().ToNextOfKin(): null;
            return patient;
        }

        /// <summary>
        /// Convert <see cref="XmlPatientBasic"/> to <see cref="Patient"/>
        /// </summary>
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

        /// <summary>
        /// Convert <see cref="XmlGpDetails"/> to <see cref="GpDetails"/>
        /// </summary>
        public static GpDetails ToGpDetails(this XmlGpDetails xmlGpDetails)
        {
            return new GpDetails
                {
                    Code = xmlGpDetails.GpCode,
                    Initials = xmlGpDetails.GpInitials,
                    Phone = xmlGpDetails.GpPhone,
                    Surname = xmlGpDetails.GpSurname
                };
        }

        /// <summary>
        /// Convert <see cref="XmlNextOfKin"/> to <see cref="NextOfKin"/>
        /// </summary>
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
