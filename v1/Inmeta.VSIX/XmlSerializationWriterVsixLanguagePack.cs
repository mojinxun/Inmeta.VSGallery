using System.Xml.Serialization;

namespace VSIXParser
{
    public class XmlSerializationWriterVsixLanguagePack : XmlSerializationWriter
    {
        public void Write3_VsixLanguagePack(object o)
        {
            this.WriteStartDocument();
            if (o == null)
            {
                this.WriteEmptyTag("VsixLanguagePack", "http://schemas.microsoft.com/developer/vsx-schema-lp/2010");
            }
            else
            {
                this.TopLevelElement();
                this.Write2_VsixLanguagePack("VsixLanguagePack", "http://schemas.microsoft.com/developer/vsx-schema-lp/2010", (VsixLanguagePack)o, false, false);
            }
        }

        private void Write2_VsixLanguagePack(string n, string ns, VsixLanguagePack o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (!isNullable)
                    return;
                this.WriteNullTagLiteral(n, ns);
            }
            else
            {
                if (!needType && !(o.GetType() == typeof(VsixLanguagePack)))
                    throw this.CreateUnknownTypeException((object)o);
                this.WriteStartElement(n, ns, (object)o, false, (XmlSerializerNamespaces)null);
                if (needType)
                    this.WriteXsiType((string)null, "http://schemas.microsoft.com/developer/vsx-schema-lp/2010");
                this.WriteAttribute("Version", "", o.Version);
                this.WriteElementString("LocalizedName", "http://schemas.microsoft.com/developer/vsx-schema-lp/2010", o.LocalizedName);
                this.WriteElementString("LocalizedDescription", "http://schemas.microsoft.com/developer/vsx-schema-lp/2010", o.LocalizedDescription);
                this.WriteElementString("MoreInfoUrl", "http://schemas.microsoft.com/developer/vsx-schema-lp/2010", o.MoreInfoUrl);
                this.WriteElementString("License", "http://schemas.microsoft.com/developer/vsx-schema-lp/2010", o.License);
                this.WriteEndElement((object)o);
            }
        }

        protected override void InitCallbacks()
        {
        }
    }
}