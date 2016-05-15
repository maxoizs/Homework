using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using PersistXML.Helper;
using PersistXML.Xml;

namespace PersistXML.Tests
{

    [TestFixture]
    public class XmlInterviewExtensionsTest
    {

        XmlInterview _xmlInterview;
        private const string XmlPatientsPath = "PersistXML.Tests.Patients.xml";
        private const string XmlPatientsXSDResourcePath = "PersistXML.Xml.Patients.xsd";

        [SetUp]
        public void Setup()
        {
            var patientSchema = XmlInterviewReader.GetSchemaFromResources(typeof(XmlInterview).Assembly, XmlPatientsXSDResourcePath);
            var settings = XmlInterviewReader.GetSchemaSettings(patientSchema);

            using (var fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(XmlPatientsPath))
            {
                _xmlInterview = XmlInterviewReader.DeserializeXmlInterview(fileStream, settings);
            }

            //var buffer = XmlInterviewReader.Read(@"D:\Dev\Homework\Patients.xml");
            //_xmlInterview = XmlInterviewReader.DeserializeXmlInterview(buffer, settings);
        }

        [Test]
        public void Test_ToPatientInterview_HeaderSection()
        {
            var result = _xmlInterview.ToPatientInterview();
            var header = (XmlHeader)_xmlInterview.Items[0];
            Assert.That(result.TransactionId, Is.EqualTo(header.TransactionId));
            Assert.That(result.TransactionTime, Is.EqualTo(DateTime.Parse(header.TransactionTime)));
        }

        [Test]
        public void Test_ToPatient()
        {
            var result = _xmlInterview.ToPatientInterview();
            var xmlPatients = (XmlPatients)_xmlInterview.Items[1];
            Assert.That(result.Patients.Count, Is.EqualTo(xmlPatients.Patient.Length));
        }

        [Test]
        public void Test_ToPatientDetials()
        {
            var xmlPatients = (XmlPatients)_xmlInterview.Items[1];
            var result = xmlPatients.Patient
                .Select(p => p.ToPatient())
                .First(i => i.PasNumber == "19083207");

            var patientBasic = xmlPatients.Patient
                .First(p => p.Basic.Any(b => b.PasNumber == "19083207")).Basic.First();

            Assert.That(result.Forenames, Is.EqualTo(patientBasic.Forenames));
            Assert.That(result.Surname, Is.EqualTo(patientBasic.Surname));
            Assert.That(result.DateOfBirth, Is.EqualTo(DateTime.Parse(patientBasic.DateOfBirth)));
            Assert.That(result.SexCode, Is.EqualTo(patientBasic.SexCode));
            Assert.That(result.HomeTelephoneNumber, Is.EqualTo(patientBasic.HomeTelephoneNumber));
        }

        [Test]
        public void Test_ToNextOfKin()
        {
            var xmlPatients = (XmlPatients)_xmlInterview.Items[1];
            var result = xmlPatients.Patient
                .SelectMany(p => p.NextOfKin)
                .First(nok => nok.NokPostcode == "PO15BN")
                .ToNextOfKin();

            var nextOfKin = xmlPatients.Patient
               .First(p => p.NextOfKin.Any(b => b.NokPostcode == "PO15BN")).NextOfKin.First();

            Assert.That(result.Name, Is.EqualTo(nextOfKin.NokName));
            Assert.That(result.RelationshipCode, Is.EqualTo(nextOfKin.NokRelationshipCode));
            Assert.That(result.AddressLine1, Is.EqualTo(nextOfKin.NokAddressLine1));
            Assert.That(result.AddressLine2, Is.EqualTo(nextOfKin.NokAddressLine2));
            Assert.That(result.AddressLine3, Is.EqualTo(nextOfKin.NokAddressLine3));
            Assert.That(result.AddressLine4, Is.EqualTo(nextOfKin.NokAddressLine4));
        }

        [Test]
        public void Test_ToGpDetails()
        {
            var xmlPatients = (XmlPatients)_xmlInterview.Items[1];
            var result = xmlPatients.Patient
                .SelectMany(p => p.GpDetails)
                .First(g => g.GpCode == "871334")
                .ToGpDetails();

            var gp = xmlPatients.Patient
              .First(p => p.GpDetails.Any(b => b.GpCode == "871334")).GpDetails.First();

            Assert.That(result.GpSurname, Is.EqualTo(gp.GpSurname));
            Assert.That(result.GpInitials, Is.EqualTo(gp.GpInitials));
            Assert.That(result.GpPhone, Is.EqualTo(gp.GpPhone));
        }
    }
}
