using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using PersistXML.Entities;
using PersistXML.Xml;

namespace PersistXML.Helper
{
    /// <summary>
    /// Helper class to read <see cref="PatientInterview"/> xml file, and deserialize it 
    /// </summary>
    public static class InterviewReader
    {
        private const string XmlInterviewRoot = "Interview";

        /// <summary>
        /// Read a file from a given file and return the file content buffer
        /// </summary>
        /// <returns>File buffer</returns>
        public static byte[] Read(string path)
        {
            if (!ValidateFileLocation(path))
            {
                throw new NullReferenceException(
                 string.Format("No file found, please make sure that the path [{0}] is correct", path));
            }

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }

        /// <summary>
        /// Check if path is correct or invalid
        /// </summary>
        /// <returns><code>true</code> means file exists</returns>
        public static bool ValidateFileLocation(string path)
        {
            if (File.Exists(path))
            {
                return true;
            }

            Console.WriteLine("No file found at {0}", path);
            return false;
        }

        /// <summary>
        /// Deserialize xml file using it's <code>byte[]</code> buffer
        /// </summary>
        public static XmlInterview DeserializeXmlInterview(byte[] buffer, XmlReaderSettings settings)
        {
            using (var stream = new MemoryStream(buffer))
            {
                return DeserializeXmlInterview(stream, settings);
            }
        }

        /// <summary>
        /// Deserialize xml file using it's <code>Stream</code> 
        /// </summary>
        public static XmlInterview DeserializeXmlInterview(Stream stream, XmlReaderSettings settings)
        {
            XmlInterview xmlInterview;

            using (XmlReader reader = XmlReader.Create(stream, settings))
            {
                var serializer = new XmlSerializer(typeof(XmlInterview), new XmlRootAttribute(XmlInterviewRoot));
                xmlInterview = (XmlInterview)serializer.Deserialize(reader);
            }
            return xmlInterview;
        }

        /// <summary>
        /// read <code>XmlSchema</code> from given <code>Assembly</code>, using it's name  
        /// </summary>
        public static XmlSchema GetSchemaFromResources(Assembly assembly, string resourceName)
        {
            XmlSchema patientSchema;

            //TODO: Handle invlaid paths exception
            using (var schemaStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (schemaStream == null)
                {
                    throw new NullReferenceException("The interview xml schema is not exists");
                }
                patientSchema = XmlSchema.Read(schemaStream, ErrorValidationHandler);
            }
            return patientSchema;
        }

        private static void ErrorValidationHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Error)
            {
                throw new Exception(e.Message);
            }

        }

        public static XmlReaderSettings CreateSchemaSettings(XmlSchema xmlSchema)
        {

            var schemas = new XmlSchemaSet();
            schemas.Add(xmlSchema);

            var settings = new XmlReaderSettings
            {
                Schemas = schemas,
                ValidationType = ValidationType.Schema,
            };

            return settings;
        }
    }
}