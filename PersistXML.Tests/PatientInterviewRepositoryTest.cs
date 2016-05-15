using System.Linq;
using System.Reflection;
using NUnit.Framework;
using PersistXML.Entities;
using PersistXML.Helper;
using PersistXML.Repositories;
using PersistXML.Xml;

namespace PersistXML.Tests
{
    [TestFixture]
    public class PatientInterviewRepositoryTest
    {
        private PatientInterview _xmlInterview;
        private const string XmlPatientsPath = "PersistXML.Tests.Patients.xml";
        private const string XmlPatientsXSDResourcePath = "PersistXML.Xml.Patients.xsd";
        private PatientInterviewRepository _interviewRepository;
        private PatientRepository _patientRepo;
        private Repository<GPDetails> _gpRepo;
        private Repository<NextOfKin> _nokRepo;
        private GPDetails _gpForTest;

        [SetUp]
        public void Setup()
        {
            var dbFactory = new DbFactory();
            _nokRepo = new Repository<NextOfKin>(dbFactory);
            _gpRepo  = new Repository<GPDetails>(dbFactory);
            _patientRepo = new PatientRepository(dbFactory, _gpRepo);
            _interviewRepository = new PatientInterviewRepository(dbFactory, _patientRepo);

            var patientSchema = InterviewReader.GetSchemaFromResources(typeof(XmlInterview).Assembly, XmlPatientsXSDResourcePath);
            var settings = InterviewReader.CreateSchemaSettings(patientSchema);

            XmlInterview interviews;
            using (var fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(XmlPatientsPath))
            {
                interviews = InterviewReader.DeserializeXmlInterview(fileStream, settings);
            }

            _xmlInterview = interviews.ToPatientInterview();
            _gpForTest = _xmlInterview.Patients.First().GpDetails;
            _interviewRepository.Save(_xmlInterview);
        }

        [Test]
        public void Test_ThatInterviewHasBeenSaved()
        {
            var result = _interviewRepository.Get(i => i.TransactionId == _xmlInterview.TransactionId).First();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.TransactionId , Is.EqualTo(_xmlInterview.TransactionId));
            Assert.That(result.TransactionTime, Is.EqualTo(_xmlInterview.TransactionTime));
        }

        [Test]
        public void Test_ThatPatientDetailsHasBeenSaved()
        {
            var patient = _xmlInterview.Patients.First(); 
            var result = _patientRepo.Get(p => p.Forenames == patient.Forenames).First();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Surname, Is.EqualTo(patient.Surname));
            Assert.That(result.SexCode, Is.EqualTo(patient.SexCode));
            Assert.That(result.DateOfBirth, Is.EqualTo(patient.DateOfBirth));
            Assert.That(result.GpCode, Is.EqualTo(patient.GpCode));
        }

        [Test]
        public void Test_ThatGPDetailsHasBeenSaved()
        {
            var result = _gpRepo.Get(g => g.Code ==_gpForTest.Code ).First();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Code, Is.EqualTo(_gpForTest.Code));
            Assert.That(result.Initials, Is.EqualTo(_gpForTest.Initials));
            Assert.That(result.Phone, Is.EqualTo(_gpForTest.Phone));
            Assert.That(result.Surname, Is.EqualTo(_gpForTest.Surname));
        }

        [Test]
        public void Test_ThatNextOfKinHasBeenSaved()
        {
            var nok = _xmlInterview.Patients.First().NextOfKin;
            var result = _nokRepo.Get(n =>  n.Name == nok.Name && n.Postcode == nok.Postcode).First();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RelationshipCode, Is.EqualTo(nok.RelationshipCode));
            Assert.That(result.AddressLine1, Is.EqualTo(nok.AddressLine1));
            Assert.That(result.AddressLine2, Is.EqualTo(nok.AddressLine2));
            Assert.That(result.AddressLine3, Is.EqualTo(nok.AddressLine3));
            Assert.That(result.AddressLine4, Is.EqualTo(nok.AddressLine4));
        }
    }
}
