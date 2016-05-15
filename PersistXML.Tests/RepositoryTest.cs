using System.Reflection;
using NUnit.Framework;
using PersistXML.Entities;
using PersistXML.Helper;
using PersistXML.Repositories;
using PersistXML.Xml;

namespace PersistXML.Tests
{
    [TestFixture]
    public class RepositoryTest
    {
        private XmlInterview _xmlInterview;
        private const string XmlPatientsPath = "PersistXML.Tests.Patients.xml";
        private const string XmlPatientsXSDResourcePath = "PersistXML.Xml.Patients.xsd";

        [SetUp]
        public void Setup()
        {
            var patientSchema = XmlInterviewReader.GetSchemaFromResources(typeof(XmlInterview).Assembly,
                                                                          XmlPatientsXSDResourcePath);
            var settings = XmlInterviewReader.GetSchemaSettings(patientSchema);

            using (var fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(XmlPatientsPath))
            {
                _xmlInterview = XmlInterviewReader.DeserializeXmlInterview(fileStream, settings);
            }


        }

        [Test]
        public void Test_SaveInterviews()
        {
            var interviews = _xmlInterview.ToPatientInterview();
            var repo = new Repository<PatientInterview>();
            repo.Save(interviews);
        }
    }
}
