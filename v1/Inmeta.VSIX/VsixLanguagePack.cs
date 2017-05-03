using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace VSIXParser
{
    [DebuggerStepThrough]
    [XmlRoot(ElementName = "VsixLanguagePack", IsNullable = false, Namespace = "http://schemas.microsoft.com/developer/vsx-schema-lp/2010")]
    [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/vsx-schema-lp/2010")]
    [Serializable]
    public sealed class VsixLanguagePack
    {
        private string _version = "1.0";

        public string LocalizedName { get; set; }

        public string LocalizedDescription { get; set; }

        public string MoreInfoUrl { get; set; }

        public string License { get; set; }

        [XmlAttribute]
        public string Version
        {
            get
            {
                return this._version;
            }
            set
            {
                this._version = value;
            }
        }
    }
}