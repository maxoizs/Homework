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

        // Since we don't have the xml xsd the generated one expect everything to be an array 
        // therefore using the deserialized file in test is a bit annoying
        [Test]
        public void Test_ToPatientDetials()
        {/*     <PasNumber>19083207</PasNumber>
                <Forenames>John</Forenames>
                <Surname>Smith</Surname>
                <DateOfBirth>1992-08-19T00:00:00</DateOfBirth>
                <SexCode>M</SexCode>
                <HomeTelephoneNumber>07708111957</HomeTelephoneNumber>          */
            var xmlPatients = (XmlPatients)_xmlInterview.Items[1];
            var patient = xmlPatients.Patient
                .Select(p => p.ToPatient())
                .First(i => i.PasNumber == "19083207");
            Assert.That(patient.Forenames, Is.EqualTo("John"));
            Assert.That(patient.Surname, Is.EqualTo("Smith"));
            Assert.That(patient.DateOfBirth, Is.EqualTo(DateTime.Parse("1992-08-19T00:00:00")));
            Assert.That(patient.SexCode, Is.EqualTo("M"));
            Assert.That(patient.HomeTelephoneNumber, Is.EqualTo("07708111957"));

        }

        [Test]
        public void Test_ToNextOfKin()
        {
            /*<NextOfKin>
                  <NokName>Gemma Cruise</NokName>
                  <NokRelationshipCode>Wife</NokRelationshipCode>
                  <NokAddressLine1>24 Claremont Road</NokAddressLine1>
                  <NokAddressLine2>Portsmouth</NokAddressLine2>
                  <NokAddressLine4>HANTS</NokAddressLine4>
                  <NokPostcode>PO15BN</NokPostcode>
              </NextOfKin>*/
            var xmlPatients = (XmlPatients)_xmlInterview.Items[1];
            var nextOfKin = xmlPatients.Patient
                .SelectMany(p => p.NextOfKin)
                .First(nok => nok.NokPostcode == "PO15BN")
                .ToNextOfKin();
            Assert.That(nextOfKin.Name, Is.EqualTo("Gemma Cruise"));
            Assert.That(nextOfKin.RelationshipCode, Is.EqualTo("Wife"));
            Assert.That(nextOfKin.AddressLine1, Is.EqualTo("24 Claremont Road"));
            Assert.That(nextOfKin.AddressLine2, Is.EqualTo("Portsmouth"));
            Assert.That(nextOfKin.AddressLine4, Is.EqualTo("HANTS"));
        }

        [Test]
        public void Test_ToGpDetails()
        {
            /*<GpDetails>
                   <GpCode>871334</GpCode>
                   <GpSurname>CHADWICK</GpSurname>
                   <GpInitials>KC</GpInitials>
                   <GpPhone>01243 388740</GpPhone>
               </GpDetails> */

            var xmlPatients = (XmlPatients)_xmlInterview.Items[1];
            var gp = xmlPatients.Patient
                .SelectMany(p => p.GpDetails)
                .First(g => g.GpCode == "871334")
                .ToGpDetails();
            Assert.That(gp.GpSurname, Is.EqualTo("CHADWICK"));
            Assert.That(gp.GpInitials, Is.EqualTo("KC"));
            Assert.That(gp.GpPhone, Is.EqualTo("01243 388740"));
        }
    }
}
