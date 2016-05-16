using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace PersistXML.Entities
{
    public class DumpXml
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "xml")]
        public string XmlContent { get; set; }

        [NotMapped]
        public XElement XmlValueWrapper
        {
            get { return XElement.Parse(XmlContent); }
            set { XmlContent = value.ToString(); }
        }
    }
}