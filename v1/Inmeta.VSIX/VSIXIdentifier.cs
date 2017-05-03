using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace VSIXParser
{
    [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2010")]
    [DesignerCategory("code")]
    [GeneratedCode("xsd", "2.0.20207.0")]
    [DebuggerStepThrough]
    [Serializable]
    public class VsixIdentifier
    {
        private string nameField;
        private string authorField;
        private string versionField;
        private string descriptionField;
        private ushort localeField;
        private string moreInfoUrlField;
        private string licenseField;
        private string gettingStartedGuideField;
        private string iconField;
        private string previewImageField;
        private bool installedByMsiField;
        private bool installedByMsiFieldSpecified;
        private ObservableCollection<object> supportedProductsField;
        private VsixIdentifierSupportedFrameworkRuntimeEdition supportedFrameworkRuntimeEditionField;
        private bool systemComponentField;
        private bool systemComponentFieldSpecified;
        private bool allUsersField;
        private bool allUsersFieldSpecified;
        private string idField;

        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        public string Author
        {
            get
            {
                return this.authorField;
            }
            set
            {
                this.authorField = value;
            }
        }

        public string Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        public ushort Locale
        {
            get
            {
                return this.localeField;
            }
            set
            {
                this.localeField = value;
            }
        }

        public string MoreInfoUrl
        {
            get
            {
                return this.moreInfoUrlField;
            }
            set
            {
                this.moreInfoUrlField = value;
            }
        }

        public string License
        {
            get
            {
                return this.licenseField;
            }
            set
            {
                this.licenseField = value;
            }
        }

        public string GettingStartedGuide
        {
            get
            {
                return this.gettingStartedGuideField;
            }
            set
            {
                this.gettingStartedGuideField = value;
            }
        }

        public string Icon
        {
            get
            {
                return this.iconField;
            }
            set
            {
                this.iconField = value;
            }
        }

        public string PreviewImage
        {
            get
            {
                return this.previewImageField;
            }
            set
            {
                this.previewImageField = value;
            }
        }

        public bool InstalledByMsi
        {
            get
            {
                return this.installedByMsiField;
            }
            set
            {
                this.installedByMsiField = value;
            }
        }

        [XmlIgnore]
        public bool InstalledByMsiSpecified
        {
            get
            {
                return this.installedByMsiFieldSpecified;
            }
            set
            {
                this.installedByMsiFieldSpecified = value;
            }
        }

        [XmlArrayItem("VisualStudio", typeof(VsixIdentifierVisualStudio), IsNullable = false)]
        [XmlArrayItem("IsolatedShell", typeof(VsixIdentifierIsolatedShell), IsNullable = false)]
        public ObservableCollection<object> SupportedProducts
        {
            get
            {
                return this.supportedProductsField;
            }
            set
            {
                this.supportedProductsField = value;
            }
        }

        public VsixIdentifierSupportedFrameworkRuntimeEdition SupportedFrameworkRuntimeEdition
        {
            get
            {
                return this.supportedFrameworkRuntimeEditionField;
            }
            set
            {
                this.supportedFrameworkRuntimeEditionField = value;
            }
        }

        public bool SystemComponent
        {
            get
            {
                return this.systemComponentField;
            }
            set
            {
                this.systemComponentField = value;
            }
        }

        [XmlIgnore]
        public bool SystemComponentSpecified
        {
            get
            {
                return this.systemComponentFieldSpecified;
            }
            set
            {
                this.systemComponentFieldSpecified = value;
            }
        }

        public bool AllUsers
        {
            get
            {
                return this.allUsersField;
            }
            set
            {
                this.allUsersField = value;
            }
        }

        [XmlIgnore]
        public bool AllUsersSpecified
        {
            get
            {
                return this.allUsersFieldSpecified;
            }
            set
            {
                this.allUsersFieldSpecified = value;
            }
        }

        [XmlAttribute]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    [DebuggerStepThrough]
    [GeneratedCode("xsd", "2.0.20207.0")]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2010")]
    [Serializable]
    public class VsixIdentifierSupportedFrameworkRuntimeEdition
    {
        private string minVersionField;
        private string maxVersionField;

        [XmlAttribute]
        public string MinVersion
        {
            get
            {
                return this.minVersionField;
            }
            set
            {
                this.minVersionField = value;
            }
        }

        [XmlAttribute]
        public string MaxVersion
        {
            get
            {
                return this.maxVersionField;
            }
            set
            {
                this.maxVersionField = value;
            }
        }
    }

    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2010")]
    [GeneratedCode("xsd", "2.0.20207.0")]
    [DesignerCategory("code")]
    [Serializable]
    public class VsixIdentifierVisualStudio
    {
        private ObservableCollection<string> editionField;
        private string versionField;

        [XmlElement("Edition")]
        public ObservableCollection<string> Edition
        {
            get
            {
                return this.editionField;
            }
            set
            {
                this.editionField = value;
            }
        }

        [XmlAttribute]
        public string Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }

    [GeneratedCode("xsd", "2.0.20207.0")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2010")]
    [Serializable]
    public class VsixIdentifierIsolatedShell
    {
        private string versionField;
        private string valueField;

        [XmlAttribute]
        public string Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        [XmlText]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }
}
