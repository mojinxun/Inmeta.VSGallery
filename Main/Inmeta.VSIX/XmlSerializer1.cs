using System.Xml.Serialization;

namespace VSIXParser
{
    public abstract class XmlSerializer1 : XmlSerializer
    {
        protected override XmlSerializationReader CreateReader()
        {
            return (XmlSerializationReader)new XmlSerializationReaderVsix();
        }

        protected override XmlSerializationWriter CreateWriter()
        {
            return (XmlSerializationWriter)new XmlSerializationWriterVsix();
        }
    }
}