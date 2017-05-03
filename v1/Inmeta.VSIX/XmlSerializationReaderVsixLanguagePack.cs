using System.Xml;
using System.Xml.Serialization;

namespace VSIXParser
{
    public class XmlSerializationReaderVsixLanguagePack : XmlSerializationReader
    {
        private string id2_Item;
        private string id7_MoreInfoUrl;
        private string id8_License;
        private string id1_VsixLanguagePack;
        private string id5_LocalizedName;
        private string id6_LocalizedDescription;
        private string id3_Item;
        private string id4_Version;

        public object Read3_VsixLanguagePack()
        {
            object obj = (object)null;
            int num = (int)this.Reader.MoveToContent();
            if (this.Reader.NodeType == XmlNodeType.Element)
            {
                if (this.Reader.LocalName != this.id1_VsixLanguagePack || this.Reader.NamespaceURI != this.id2_Item)
                    throw this.CreateUnknownNodeException();
                obj = (object)this.Read2_VsixLanguagePack(false, true);
            }
            else
                this.UnknownNode((object)null, "http://schemas.microsoft.com/developer/vsx-schema-lp/2010:VsixLanguagePack");
            return obj;
        }

        private VsixLanguagePack Read2_VsixLanguagePack(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? this.GetXsiType() : (XmlQualifiedName)null;
            bool flag = false;
            if (isNullable)
                flag = this.ReadNull();
            if (checkType && !(type == (XmlQualifiedName)null) && (type.Name != this.id3_Item || type.Namespace != this.id2_Item))
                throw this.CreateUnknownTypeException(type);
            if (flag)
                return (VsixLanguagePack)null;
            VsixLanguagePack vsixLanguagePack = new VsixLanguagePack();
            bool[] flagArray = new bool[5];
            while (this.Reader.MoveToNextAttribute())
            {
                if (!flagArray[4] && this.Reader.LocalName == this.id4_Version && this.Reader.NamespaceURI == this.id3_Item)
                {
                    vsixLanguagePack.Version = this.Reader.Value;
                    flagArray[4] = true;
                }
                else if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode((object)vsixLanguagePack, ":Version");
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                return vsixLanguagePack;
            }
            else
            {
                this.Reader.ReadStartElement();
                int num1 = (int)this.Reader.MoveToContent();
                int whileIterations = 0;
                int readerCount = this.ReaderCount;
                while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
                {
                    if (this.Reader.NodeType == XmlNodeType.Element)
                    {
                        if (!flagArray[0] && this.Reader.LocalName == this.id5_LocalizedName && this.Reader.NamespaceURI == this.id2_Item)
                        {
                            vsixLanguagePack.LocalizedName = this.Reader.ReadElementString();
                            flagArray[0] = true;
                        }
                        else if (!flagArray[1] && this.Reader.LocalName == this.id6_LocalizedDescription && this.Reader.NamespaceURI == this.id2_Item)
                        {
                            vsixLanguagePack.LocalizedDescription = this.Reader.ReadElementString();
                            flagArray[1] = true;
                        }
                        else if (!flagArray[2] && this.Reader.LocalName == this.id7_MoreInfoUrl && this.Reader.NamespaceURI == this.id2_Item)
                        {
                            vsixLanguagePack.MoreInfoUrl = this.Reader.ReadElementString();
                            flagArray[2] = true;
                        }
                        else if (!flagArray[3] && this.Reader.LocalName == this.id8_License && this.Reader.NamespaceURI == this.id2_Item)
                        {
                            vsixLanguagePack.License = this.Reader.ReadElementString();
                            flagArray[3] = true;
                        }
                        else
                            this.UnknownNode((object)vsixLanguagePack, "http://schemas.microsoft.com/developer/vsx-schema-lp/2010:LocalizedName, http://schemas.microsoft.com/developer/vsx-schema-lp/2010:LocalizedDescription, http://schemas.microsoft.com/developer/vsx-schema-lp/2010:MoreInfoUrl, http://schemas.microsoft.com/developer/vsx-schema-lp/2010:License");
                    }
                    else
                        this.UnknownNode((object)vsixLanguagePack, "http://schemas.microsoft.com/developer/vsx-schema-lp/2010:LocalizedName, http://schemas.microsoft.com/developer/vsx-schema-lp/2010:LocalizedDescription, http://schemas.microsoft.com/developer/vsx-schema-lp/2010:MoreInfoUrl, http://schemas.microsoft.com/developer/vsx-schema-lp/2010:License");
                    int num2 = (int)this.Reader.MoveToContent();
                    this.CheckReaderCount(ref whileIterations, ref readerCount);
                }
                this.ReadEndElement();
                return vsixLanguagePack;
            }
        }

        protected override void InitCallbacks()
        {
        }

        protected override void InitIDs()
        {
            this.id2_Item = this.Reader.NameTable.Add("http://schemas.microsoft.com/developer/vsx-schema-lp/2010");
            this.id7_MoreInfoUrl = this.Reader.NameTable.Add("MoreInfoUrl");
            this.id8_License = this.Reader.NameTable.Add("License");
            this.id1_VsixLanguagePack = this.Reader.NameTable.Add("VsixLanguagePack");
            this.id5_LocalizedName = this.Reader.NameTable.Add("LocalizedName");
            this.id6_LocalizedDescription = this.Reader.NameTable.Add("LocalizedDescription");
            this.id3_Item = this.Reader.NameTable.Add("");
            this.id4_Version = this.Reader.NameTable.Add("Version");
        }
    }
}