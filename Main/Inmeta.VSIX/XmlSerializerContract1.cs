using System;
using System.Collections;
using System.Xml.Serialization;

namespace VSIXParser
{
    public class XmlSerializerContract1 : XmlSerializerImplementation
    {
        private Hashtable readMethods;
        private Hashtable writeMethods;
        private Hashtable typedSerializers;

        public override XmlSerializationReader Reader
        {
            get
            {
                return (XmlSerializationReader)new XmlSerializationReaderVsixLanguagePack();
            }
        }

        public override XmlSerializationWriter Writer
        {
            get
            {
                return (XmlSerializationWriter)new XmlSerializationWriterVsixLanguagePack();
            }
        }

        public override Hashtable ReadMethods
        {
            get
            {
                if (this.readMethods == null)
                {
                    Hashtable hashtable = new Hashtable();
                    hashtable[(object)"VsixExplorer.VsixManifest.VsixLanguagePack:http://schemas.microsoft.com/developer/vsx-schema-lp/2010:VsixLanguagePack:False:"] = (object)"Read3_VsixLanguagePack";
                    if (this.readMethods == null)
                        this.readMethods = hashtable;
                }
                return this.readMethods;
            }
        }

        public override Hashtable WriteMethods
        {
            get
            {
                if (this.writeMethods == null)
                {
                    Hashtable hashtable = new Hashtable();
                    hashtable[(object)"VsixExplorer.VsixManifest.VsixLanguagePack:http://schemas.microsoft.com/developer/vsx-schema-lp/2010:VsixLanguagePack:False:"] = (object)"Write3_VsixLanguagePack";
                    if (this.writeMethods == null)
                        this.writeMethods = hashtable;
                }
                return this.writeMethods;
            }
        }

        public override Hashtable TypedSerializers
        {
            get
            {
                if (this.typedSerializers == null)
                {
                    Hashtable hashtable = new Hashtable();
                    hashtable.Add((object)"VsixExplorer.VsixManifest.VsixLanguagePack:http://schemas.microsoft.com/developer/vsx-schema-lp/2010:VsixLanguagePack:False:", (object)new VsixLanguagePackSerializer());
                    if (this.typedSerializers == null)
                        this.typedSerializers = hashtable;
                }
                return this.typedSerializers;
            }
        }

        public override bool CanSerialize(Type type)
        {
            return type == typeof(VsixLanguagePack);
        }

        public override XmlSerializer GetSerializer(Type type)
        {
            if (type == typeof(VsixLanguagePack))
                return (XmlSerializer)new VsixLanguagePackSerializer();
            else
                return (XmlSerializer)null;
        }
    }
}