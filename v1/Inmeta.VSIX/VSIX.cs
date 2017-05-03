using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace VSIXParser
{
    [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2010")]
    [XmlRoot(ElementName = "Vsix", IsNullable = false, Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2010")]
    [DebuggerStepThrough]
    [Serializable]
    public sealed class Vsix
    {
        private string _version = "1.0";

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

        public VsixIdentifier Identifier { get; set; }

        [XmlArrayItem("Reference", IsNullable = false)]
        public ObservableCollection<VsixReference> References { get; set; }

        public VsixContent Content { get; set; }
    }

    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2010")]
    [Serializable]
    public sealed class VsixReference
    {
        public string Name { get; set; }

        public string MoreInfoUrl { get; set; }

        public string VsixPath { get; set; }

        [XmlAttribute]
        public string Id { get; set; }

        [XmlAttribute]
        public string MinVersion { get; set; }

        [XmlAttribute]
        public string MaxVersion { get; set; }
    }

    public sealed class VsixVsPackagePath : VsixExtensionPath
    {
        public override ContentItemTypes? ExtensionType
        {
            get
            {
                return new ContentItemTypes?(ContentItemTypes.VsPackage);
            }
        }
    }

    public sealed class VsixMefComponentPath : VsixExtensionPath
    {
        public override ContentItemTypes? ExtensionType
        {
            get
            {
                return new ContentItemTypes?(ContentItemTypes.MefComponent);
            }
        }
    }

    public sealed class VsixProjectTemplatePath : VsixExtensionPath
    {
        public override ContentItemTypes? ExtensionType
        {
            get
            {
                return new ContentItemTypes?(ContentItemTypes.ProjectTemplate);
            }
        }
    }

    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2010")]
    [Serializable]
    public sealed class VsixContent
    {
        [XmlElement("ItemTemplate", typeof(VsixItemTemplatePath))]
        [XmlElement("ToolboxControl", typeof(VsixToolboxControlPath))]
        [XmlElement("Assembly", typeof(VsixAssembly))]
        [XmlElement("CustomExtension", typeof(VsixCustomExtension))]
        [XmlChoiceIdentifier("ItemsElementName")]
        [XmlElement("VsPackage", typeof(VsixVsPackagePath))]
        [XmlElement("MefComponent", typeof(VsixMefComponentPath))]
        [XmlElement("ProjectTemplate", typeof(VsixProjectTemplatePath))]
        public object[] Items { get; set; }

        [XmlElement("ItemsElementName")]
        [XmlIgnore]
        public ContentItemTypes[] ItemsElementName { get; set; }
    }

    [XmlType(IncludeInSchema = false, Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2010")]
    [Serializable]
    public enum ContentItemTypes
    {
        Assembly,
        CustomExtension,
        ItemTemplate,
        MefComponent,
        ProjectTemplate,
        ToolboxControl,
        VsPackage,
    }

    public sealed class VsixItemTemplatePath : VsixExtensionPath
    {
        public override ContentItemTypes? ExtensionType
        {
            get
            {
                return new ContentItemTypes?(ContentItemTypes.ItemTemplate);
            }
        }
    }

    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2010")]
    [Serializable]
    public class VsixExtensionPath
    {
        [XmlIgnore]
        public virtual ContentItemTypes? ExtensionType
        {
            get
            {
                return new ContentItemTypes?();
            }
        }

        [XmlText]
        public string Value { get; set; }
    }

    public sealed class VsixToolboxControlPath : VsixExtensionPath
    {
        public override ContentItemTypes? ExtensionType
        {
            get
            {
                return new ContentItemTypes?(ContentItemTypes.ToolboxControl);
            }
        }
    }

    internal class VsixTextWriter : XmlTextWriter
    {
        private bool _skip;

        public VsixTextWriter(TextWriter w)
            : base(w)
        {
            this.Formatting = Formatting.Indented;
        }

        public VsixTextWriter(Stream w, Encoding encoding)
            : base(w, encoding)
        {
            this.Formatting = Formatting.Indented;
        }

        public VsixTextWriter(string filename, Encoding encoding)
            : base(filename, encoding)
        {
            this.Formatting = Formatting.Indented;
        }

        public override void WriteStartAttribute(string prefix, string localName, string ns)
        {
            if (prefix == "xmlns" && (localName == "xsd" || localName == "xsi" || localName.StartsWith("q")) || prefix == null && localName == "type")
                this._skip = true;
            else
                base.WriteStartAttribute(prefix, localName, ns);
        }

        public override void WriteString(string text)
        {
            if (this._skip)
                return;
            base.WriteString(text);
        }

        public override void WriteEndAttribute()
        {
            if (this._skip)
                this._skip = false;
            else
                base.WriteEndAttribute();
        }
    }

    [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2010")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [Serializable]
    public sealed class VsixAssembly
    {
        [XmlAttribute]
        public string AssemblyName { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2010")]
    [Serializable]
    public sealed class VsixCustomExtension
    {
        [XmlAttribute]
        public string Type { get; set; }

        [XmlAnyAttribute]
        public XmlAttribute[] AnyAttr { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
