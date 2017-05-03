using System;
using System.Collections;
using System.Xml.Serialization;

namespace VSIXParser
{
    public class XmlSerializerContract : XmlSerializerImplementation
    {
        private Hashtable readMethods;
        private Hashtable writeMethods;
        private Hashtable typedSerializers;

        public override XmlSerializationReader Reader
        {
            get
            {
                return (XmlSerializationReader)new XmlSerializationReaderVsix();
            }
        }

        public override XmlSerializationWriter Writer
        {
            get
            {
                return (XmlSerializationWriter)new XmlSerializationWriterVsix();
            }
        }

        public override Hashtable ReadMethods
        {
            get
            {
                if (this.readMethods == null)
                {
                    Hashtable hashtable = new Hashtable();
                    hashtable[(object)"VsixExplorer.VsixManifest.Vsix:http://schemas.microsoft.com/developer/vsx-schema/2010:Vsix:False:"] = (object)"Read18_Vsix";
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
                    hashtable[(object)"VsixExplorer.VsixManifest.Vsix:http://schemas.microsoft.com/developer/vsx-schema/2010:Vsix:False:"] = (object)"Write18_Vsix";
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
                    hashtable.Add((object)"VsixExplorer.VsixManifest.Vsix:http://schemas.microsoft.com/developer/vsx-schema/2010:Vsix:False:", (object)new VsixSerializer());
                    if (this.typedSerializers == null)
                        this.typedSerializers = hashtable;
                }
                return this.typedSerializers;
            }
        }

        public override bool CanSerialize(Type type)
        {
            return type == typeof(Vsix);
        }

        public override XmlSerializer GetSerializer(Type type)
        {
            if (type == typeof(Vsix))
                return (XmlSerializer)new VsixSerializer();
            else
                return (XmlSerializer)null;
        }
    }
}