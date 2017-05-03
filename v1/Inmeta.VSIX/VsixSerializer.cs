using System.Xml;
using System.Xml.Serialization;

namespace VSIXParser
{
    public sealed class VsixSerializer : XmlSerializer1
    {
        public override bool CanDeserialize(XmlReader xmlReader)
        {
            return xmlReader.IsStartElement("Vsix", "http://schemas.microsoft.com/developer/vsx-schema/2010");
        }

        protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
        {
            ((XmlSerializationWriterVsix)writer).Write18_Vsix(objectToSerialize);
        }

        protected override object Deserialize(XmlSerializationReader reader)
        {
            return ((XmlSerializationReaderVsix)reader).Read18_Vsix();
        }
    }
}