using System.Xml;
using System.Xml.Serialization;

namespace VSIXParser
{
    public sealed class VsixLanguagePackSerializer : XmlSerializer2
    {
        public override bool CanDeserialize(XmlReader xmlReader)
        {
            return xmlReader.IsStartElement("VsixLanguagePack", "http://schemas.microsoft.com/developer/vsx-schema-lp/2010");
        }

        protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
        {
            ((XmlSerializationWriterVsixLanguagePack)writer).Write3_VsixLanguagePack(objectToSerialize);
        }

        protected override object Deserialize(XmlSerializationReader reader)
        {
            return ((XmlSerializationReaderVsixLanguagePack)reader).Read3_VsixLanguagePack();
        }
    }


}
