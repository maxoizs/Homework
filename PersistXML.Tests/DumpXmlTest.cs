using System;
using System.Linq;
using System.Xml;
using NUnit.Framework;
using PersistXML.Entities;
using PersistXML.Helper;
using PersistXML.Repositories;
using PersistXML.Xml;

namespace PersistXML.Tests
{
    [TestFixture]
    public class DumpXmlTest
    {
        private const string XmlPatientsXSDResourcePath = "PersistXML.Xml.Patients.xsd";
        private Repository<DumpXml> _dumpRepo;
        private XmlReaderSettings _settings;

        [SetUp]
        public void Setup()
        {
            var dbFactory = new DbFactory();
            _dumpRepo = new Repository<DumpXml>(dbFactory);

            var patientSchema = InterviewReader.GetSchemaFromResources(typeof(XmlInterview).Assembly,
                                                                       XmlPatientsXSDResourcePath);
            _settings = InterviewReader.CreateSchemaSettings(patientSchema);
        }

        [Test]
        public void When_InvalidPath_ShouldThrowException()
        {
            Assert.Throws<NullReferenceException>(() => InterviewReader.Read(@"D:\Dev\Homework\invalid"));
        }

        [Test]
        public void When_EmptyContent_ShouldReturnNull()
        {
            var buffer = InterviewReader.Read(@"D:\Dev\Homework\empty.xml");

            Assert.That(buffer, Is.Empty);
        }

        [Test]
        public void When_InvalidContent_ShouldThrowException()
        {
            var buffer = InterviewReader.Read(@"D:\Dev\Homework\invalid.xml");
            Assert.Throws<InvalidOperationException>(() => InterviewReader.DeserializeXmlInterview(buffer, _settings));
        }

        [Test]
        public void When_FileIsCorrect_ShouldDeserialize()
        {
            var buffer = InterviewReader.Read(@"D:\Dev\Homework\Patients.xml");
            Assert.That(buffer, Is.Not.Empty);

            var interviews = InterviewReader.DeserializeXmlInterview(buffer, _settings);
            Assert.That(interviews, Is.Not.Null);
            Assert.That(interviews.Items.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Test_SaveDumpXml()
        {
            var buffer = InterviewReader.Read(@"D:\Dev\Homework\Patients.xml");
            var xml = System.Text.Encoding.UTF8.GetString(buffer);
            var dump = new DumpXml {XmlContent = xml};
            _dumpRepo.Save(dump);

            var result = _dumpRepo.Get(d => d.Id == dump.Id).First();
            Assert.That(result.Id, Is.EqualTo(dump.Id));
            Assert.That(result.XmlContent, Is.EqualTo(dump.XmlContent));
        }

    }
}