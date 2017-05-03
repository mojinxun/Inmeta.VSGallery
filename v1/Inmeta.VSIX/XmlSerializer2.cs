using System.Xml.Serialization;

namespace VSIXParser
{
    public abstract class XmlSerializer2 : XmlSerializer
    {
        protected override XmlSerializationReader CreateReader()
        {
            return (XmlSerializationReader)new XmlSerializationReaderVsixLanguagePack();
        }

        protected override XmlSerializationWriter CreateWriter()
        {
            return (XmlSerializationWriter)new XmlSerializationWriterVsixLanguagePack();
        }
    }
}