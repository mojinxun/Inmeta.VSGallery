using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace VSIXParser
{
    public class XmlSerializationWriterVsix : XmlSerializationWriter
    {
        public void Write18_Vsix(object o)
        {
            this.WriteStartDocument();
            if (o == null)
            {
                this.WriteEmptyTag("Vsix", "http://schemas.microsoft.com/developer/vsx-schema/2010");
            }
            else
            {
                this.TopLevelElement();
                this.Write17_Vsix("Vsix", "http://schemas.microsoft.com/developer/vsx-schema/2010", (Vsix)o, false, false);
            }
        }

        private void Write17_Vsix(string n, string ns, Vsix o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (!isNullable)
                    return;
                this.WriteNullTagLiteral(n, ns);
            }
            else
            {
                if (!needType && !(o.GetType() == typeof(Vsix)))
                    throw this.CreateUnknownTypeException((object)o);
                this.WriteStartElement(n, ns, (object)o, false, (XmlSerializerNamespaces)null);
                if (needType)
                    this.WriteXsiType((string)null, "http://schemas.microsoft.com/developer/vsx-schema/2010");
                this.WriteAttribute("Version", "", o.Version);
                this.Write5_VsixIdentifier("Identifier", "http://schemas.microsoft.com/developer/vsx-schema/2010", o.Identifier, false, false);
                ObservableCollection<VsixReference> references = o.References;
                if (references != null)
                {
                    this.WriteStartElement("References", "http://schemas.microsoft.com/developer/vsx-schema/2010", (object)null, false);
                    for (int index = 0; index < references.Count; ++index)
                        this.Write6_VsixReference("Reference", "http://schemas.microsoft.com/developer/vsx-schema/2010", references[index], false, false);
                    this.WriteEndElement();
                }
                this.Write16_VsixContent("Content", "http://schemas.microsoft.com/developer/vsx-schema/2010", o.Content, false, false);
                this.WriteEndElement((object)o);
            }
        }

        private void Write16_VsixContent(string n, string ns, VsixContent o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (!isNullable)
                    return;
                this.WriteNullTagLiteral(n, ns);
            }
            else
            {
                if (!needType && !(o.GetType() == typeof(VsixContent)))
                    throw this.CreateUnknownTypeException((object)o);
                this.WriteStartElement(n, ns, (object)o, false, (XmlSerializerNamespaces)null);
                if (needType)
                    this.WriteXsiType((string)null, "http://schemas.microsoft.com/developer/vsx-schema/2010");
                object[] items = o.Items;
                if (items != null)
                {
                    ContentItemTypes[] itemsElementName = o.ItemsElementName;
                    if (itemsElementName == null || itemsElementName.Length < items.Length)
                        throw this.CreateInvalidChoiceIdentifierValueException("VsixExplorer.VsixManifest.ContentItemTypes", "ItemsElementName");
                    for (int index = 0; index < items.Length; ++index)
                    {
                        object o1 = items[index];
                        ContentItemTypes contentItemTypes = itemsElementName[index];
                        if (contentItemTypes == ContentItemTypes.MefComponent && o1 != null)
                        {
                            if (o1 != null && !(o1 is VsixMefComponentPath))
                                throw this.CreateMismatchChoiceException("VsixExplorer.VsixManifest.VsixMefComponentPath", "ItemsElementName", "VsixExplorer.VsixManifest.ContentItemTypes.@MefComponent");
                            this.Write12_VsixMefComponentPath("MefComponent", "http://schemas.microsoft.com/developer/vsx-schema/2010", (VsixMefComponentPath)o1, false, false);
                        }
                        else if (contentItemTypes == ContentItemTypes.VsPackage && o1 != null)
                        {
                            if (o1 != null && !(o1 is VsixVsPackagePath))
                                throw this.CreateMismatchChoiceException("VsixExplorer.VsixManifest.VsixVsPackagePath", "ItemsElementName", "VsixExplorer.VsixManifest.ContentItemTypes.@VsPackage");
                            this.Write13_VsixVsPackagePath("VsPackage", "http://schemas.microsoft.com/developer/vsx-schema/2010", (VsixVsPackagePath)o1, false, false);
                        }
                        else if (contentItemTypes == ContentItemTypes.ProjectTemplate && o1 != null)
                        {
                            if (o1 != null && !(o1 is VsixProjectTemplatePath))
                                throw this.CreateMismatchChoiceException("VsixExplorer.VsixManifest.VsixProjectTemplatePath", "ItemsElementName", "VsixExplorer.VsixManifest.ContentItemTypes.@ProjectTemplate");
                            this.Write15_VsixProjectTemplatePath("ProjectTemplate", "http://schemas.microsoft.com/developer/vsx-schema/2010", (VsixProjectTemplatePath)o1, false, false);
                        }
                        else if (contentItemTypes == ContentItemTypes.ToolboxControl && o1 != null)
                        {
                            if (o1 != null && !(o1 is VsixToolboxControlPath))
                                throw this.CreateMismatchChoiceException("VsixExplorer.VsixManifest.VsixToolboxControlPath", "ItemsElementName", "VsixExplorer.VsixManifest.ContentItemTypes.@ToolboxControl");
                            this.Write9_VsixToolboxControlPath("ToolboxControl", "http://schemas.microsoft.com/developer/vsx-schema/2010", (VsixToolboxControlPath)o1, false, false);
                        }
                        else if (contentItemTypes == ContentItemTypes.ItemTemplate && o1 != null)
                        {
                            if (o1 != null && !(o1 is VsixItemTemplatePath))
                                throw this.CreateMismatchChoiceException("VsixExplorer.VsixManifest.VsixItemTemplatePath", "ItemsElementName", "VsixExplorer.VsixManifest.ContentItemTypes.@ItemTemplate");
                            this.Write11_VsixItemTemplatePath("ItemTemplate", "http://schemas.microsoft.com/developer/vsx-schema/2010", (VsixItemTemplatePath)o1, false, false);
                        }
                        else if (contentItemTypes == ContentItemTypes.CustomExtension && o1 != null)
                        {
                            if (o1 != null && !(o1 is VsixCustomExtension))
                                throw this.CreateMismatchChoiceException("VsixExplorer.VsixManifest.VsixCustomExtension", "ItemsElementName", "VsixExplorer.VsixManifest.ContentItemTypes.@CustomExtension");
                            this.Write10_VsixCustomExtension("CustomExtension", "http://schemas.microsoft.com/developer/vsx-schema/2010", (VsixCustomExtension)o1, false, false);
                        }
                        else if (contentItemTypes == ContentItemTypes.Assembly && o1 != null)
                        {
                            if (o1 != null && !(o1 is VsixAssembly))
                                throw this.CreateMismatchChoiceException("VsixExplorer.VsixManifest.VsixAssembly", "ItemsElementName", "VsixExplorer.VsixManifest.ContentItemTypes.@Assembly");
                            this.Write14_VsixAssembly("Assembly", "http://schemas.microsoft.com/developer/vsx-schema/2010", (VsixAssembly)o1, false, false);
                        }
                        else if (o1 != null)
                            throw this.CreateUnknownTypeException(o1);
                    }
                }
                this.WriteEndElement((object)o);
            }
        }

        private void Write14_VsixAssembly(string n, string ns, VsixAssembly o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (!isNullable)
                    return;
                this.WriteNullTagLiteral(n, ns);
            }
            else
            {
                if (!needType && !(o.GetType() == typeof(VsixAssembly)))
                    throw this.CreateUnknownTypeException((object)o);
                this.WriteStartElement(n, ns, (object)o, false, (XmlSerializerNamespaces)null);
                if (needType)
                    this.WriteXsiType((string)null, "http://schemas.microsoft.com/developer/vsx-schema/2010");
                this.WriteAttribute("AssemblyName", "", o.AssemblyName);
                if (o.Value != null)
                    this.WriteValue(o.Value);
                this.WriteEndElement((object)o);
            }
        }

        private void Write10_VsixCustomExtension(string n, string ns, VsixCustomExtension o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (!isNullable)
                    return;
                this.WriteNullTagLiteral(n, ns);
            }
            else
            {
                if (!needType && !(o.GetType() == typeof(VsixCustomExtension)))
                    throw this.CreateUnknownTypeException((object)o);
                this.WriteStartElement(n, ns, (object)o, false, (XmlSerializerNamespaces)null);
                if (needType)
                    this.WriteXsiType((string)null, "http://schemas.microsoft.com/developer/vsx-schema/2010");
                this.WriteAttribute("Type", "", o.Type);
                XmlAttribute[] anyAttr = o.AnyAttr;
                if (anyAttr != null)
                {
                    for (int index = 0; index < anyAttr.Length; ++index)
                        this.WriteXmlAttribute((XmlNode)anyAttr[index], (object)o);
                }
                if (o.Value != null)
                    this.WriteValue(o.Value);
                this.WriteEndElement((object)o);
            }
        }

        private void Write11_VsixItemTemplatePath(string n, string ns, VsixItemTemplatePath o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (!isNullable)
                    return;
                this.WriteNullTagLiteral(n, ns);
            }
            else
            {
                if (!needType && !(o.GetType() == typeof(VsixItemTemplatePath)))
                    throw this.CreateUnknownTypeException((object)o);
                this.WriteStartElement(n, ns, (object)o, false, (XmlSerializerNamespaces)null);
                if (needType)
                    this.WriteXsiType("VsixItemTemplatePath", "http://schemas.microsoft.com/developer/vsx-schema/2010");
                if (o.Value != null)
                    this.WriteValue(o.Value);
                this.WriteEndElement((object)o);
            }
        }

        private void Write9_VsixToolboxControlPath(string n, string ns, VsixToolboxControlPath o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (!isNullable)
                    return;
                this.WriteNullTagLiteral(n, ns);
            }
            else
            {
                if (!needType && !(o.GetType() == typeof(VsixToolboxControlPath)))
                    throw this.CreateUnknownTypeException((object)o);
                this.WriteStartElement(n, ns, (object)o, false, (XmlSerializerNamespaces)null);
                if (needType)
                    this.WriteXsiType("VsixToolboxControlPath", "http://schemas.microsoft.com/developer/vsx-schema/2010");
                if (o.Value != null)
                    this.WriteValue(o.Value);
                this.WriteEndElement((object)o);
            }
        }

        private void Write15_VsixProjectTemplatePath(string n, string ns, VsixProjectTemplatePath o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (!isNullable)
                    return;
                this.WriteNullTagLiteral(n, ns);
            }
            else
            {
                if (!needType && !(o.GetType() == typeof(VsixProjectTemplatePath)))
                    throw this.CreateUnknownTypeException((object)o);
                this.WriteStartElement(n, ns, (object)o, false, (XmlSerializerNamespaces)null);
                if (needType)
                    this.WriteXsiType("VsixProjectTemplatePath", "http://schemas.microsoft.com/developer/vsx-schema/2010");
                if (o.Value != null)
                    this.WriteValue(o.Value);
                this.WriteEndElement((object)o);
            }
        }

        private void Write13_VsixVsPackagePath(string n, string ns, VsixVsPackagePath o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (!isNullable)
                    return;
                this.WriteNullTagLiteral(n, ns);
            }
            else
            {
                if (!needType && !(o.GetType() == typeof(VsixVsPackagePath)))
                    throw this.CreateUnknownTypeException((object)o);
                this.WriteStartElement(n, ns, (object)o, false, (XmlSerializerNamespaces)null);
                if (needType)
                    this.WriteXsiType("VsixVsPackagePath", "http://schemas.microsoft.com/developer/vsx-schema/2010");
                if (o.Value != null)
                    this.WriteValue(o.Value);
                this.WriteEndElement((object)o);
            }
        }

        private void Write12_VsixMefComponentPath(string n, string ns, VsixMefComponentPath o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (!isNullable)
                    return;
                this.WriteNullTagLiteral(n, ns);
            }
            else
            {
                if (!needType && !(o.GetType() == typeof(VsixMefComponentPath)))
                    throw this.CreateUnknownTypeException((object)o);
                this.WriteStartElement(n, ns, (object)o, false, (XmlSerializerNamespaces)null);
                if (needType)
                    this.WriteXsiType("VsixMefComponentPath", "http://schemas.microsoft.com/developer/vsx-schema/2010");
                if (o.Value != null)
                    this.WriteValue(o.Value);
                this.WriteEndElement((object)o);
            }
        }

        private void Write6_VsixReference(string n, string ns, VsixReference o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (!isNullable)
                    return;
                this.WriteNullTagLiteral(n, ns);
            }
            else
            {
                if (!needType && !(o.GetType() == typeof(VsixReference)))
                    throw this.CreateUnknownTypeException((object)o);
                this.WriteStartElement(n, ns, (object)o, false, (XmlSerializerNamespaces)null);
                if (needType)
                    this.WriteXsiType((string)null, "http://schemas.microsoft.com/developer/vsx-schema/2010");
                this.WriteAttribute("Id", "", o.Id);
                this.WriteAttribute("MinVersion", "", o.MinVersion);
                this.WriteAttribute("MaxVersion", "", o.MaxVersion);
                this.WriteElementString("Name", "http://schemas.microsoft.com/developer/vsx-schema/2010", o.Name);
                this.WriteElementString("MoreInfoUrl", "http://schemas.microsoft.com/developer/vsx-schema/2010", o.MoreInfoUrl);
                this.WriteElementString("VsixPath", "http://schemas.microsoft.com/developer/vsx-schema/2010", o.VsixPath);
                this.WriteEndElement((object)o);
            }
        }

        private void Write5_VsixIdentifier(string n, string ns, VsixIdentifier o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (!isNullable)
                    return;
                this.WriteNullTagLiteral(n, ns);
            }
            else
            {
                if (!needType && !(o.GetType() == typeof(VsixIdentifier)))
                    throw this.CreateUnknownTypeException((object)o);
                this.WriteStartElement(n, ns, (object)o, false, (XmlSerializerNamespaces)null);
                if (needType)
                    this.WriteXsiType((string)null, "http://schemas.microsoft.com/developer/vsx-schema/2010");
                this.WriteAttribute("Id", "", o.Id);
                this.WriteElementString("Name", "http://schemas.microsoft.com/developer/vsx-schema/2010", o.Name);
                this.WriteElementString("Author", "http://schemas.microsoft.com/developer/vsx-schema/2010", o.Author);
                this.WriteElementString("Version", "http://schemas.microsoft.com/developer/vsx-schema/2010", o.Version);
                this.WriteElementString("Description", "http://schemas.microsoft.com/developer/vsx-schema/2010", o.Description);
                this.WriteElementStringRaw("Locale", "http://schemas.microsoft.com/developer/vsx-schema/2010", XmlConvert.ToString(o.Locale));
                this.WriteElementString("MoreInfoUrl", "http://schemas.microsoft.com/developer/vsx-schema/2010", o.MoreInfoUrl);
                this.WriteElementString("License", "http://schemas.microsoft.com/developer/vsx-schema/2010", o.License);
                this.WriteElementString("GettingStartedGuide", "http://schemas.microsoft.com/developer/vsx-schema/2010", o.GettingStartedGuide);
                this.WriteElementString("Icon", "http://schemas.microsoft.com/developer/vsx-schema/2010", o.Icon);
                this.WriteElementString("PreviewImage", "http://schemas.microsoft.com/developer/vsx-schema/2010", o.PreviewImage);
                if (o.InstalledByMsiSpecified)
                    this.WriteElementStringRaw("InstalledByMsi", "http://schemas.microsoft.com/developer/vsx-schema/2010", XmlConvert.ToString(o.InstalledByMsi));
                ObservableCollection<object> supportedProducts = o.SupportedProducts;
                if (supportedProducts != null)
                {
                    this.WriteStartElement("SupportedProducts", "http://schemas.microsoft.com/developer/vsx-schema/2010", (object)null, false);
                    for (int index = 0; index < supportedProducts.Count; ++index)
                    {
                        object o1 = supportedProducts[index];
                        if (o1 != null)
                        {
                            if (o1 is VsixIdentifierIsolatedShell)
                                this.Write2_VsixIdentifierIsolatedShell("IsolatedShell", "http://schemas.microsoft.com/developer/vsx-schema/2010", (VsixIdentifierIsolatedShell)o1, false, false);
                            else if (o1 is VsixIdentifierVisualStudio)
                                this.Write3_VsixIdentifierVisualStudio("VisualStudio", "http://schemas.microsoft.com/developer/vsx-schema/2010", (VsixIdentifierVisualStudio)o1, false, false);
                            else if (o1 != null)
                                throw this.CreateUnknownTypeException(o1);
                        }
                    }
                    this.WriteEndElement();
                }
                this.Write4_Item("SupportedFrameworkRuntimeEdition", "http://schemas.microsoft.com/developer/vsx-schema/2010", o.SupportedFrameworkRuntimeEdition, false, false);
                if (o.SystemComponentSpecified)
                    this.WriteElementStringRaw("SystemComponent", "http://schemas.microsoft.com/developer/vsx-schema/2010", XmlConvert.ToString(o.SystemComponent));
                if (o.AllUsersSpecified)
                    this.WriteElementStringRaw("AllUsers", "http://schemas.microsoft.com/developer/vsx-schema/2010", XmlConvert.ToString(o.AllUsers));
                this.WriteEndElement((object)o);
            }
        }

        private void Write4_Item(string n, string ns, VsixIdentifierSupportedFrameworkRuntimeEdition o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (!isNullable)
                    return;
                this.WriteNullTagLiteral(n, ns);
            }
            else
            {
                if (!needType && !(o.GetType() == typeof(VsixIdentifierSupportedFrameworkRuntimeEdition)))
                    throw this.CreateUnknownTypeException((object)o);
                this.WriteStartElement(n, ns, (object)o, false, (XmlSerializerNamespaces)null);
                if (needType)
                    this.WriteXsiType((string)null, "http://schemas.microsoft.com/developer/vsx-schema/2010");
                this.WriteAttribute("MinVersion", "", o.MinVersion);
                this.WriteAttribute("MaxVersion", "", o.MaxVersion);
                this.WriteEndElement((object)o);
            }
        }

        private void Write3_VsixIdentifierVisualStudio(string n, string ns, VsixIdentifierVisualStudio o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (!isNullable)
                    return;
                this.WriteNullTagLiteral(n, ns);
            }
            else
            {
                if (!needType && !(o.GetType() == typeof(VsixIdentifierVisualStudio)))
                    throw this.CreateUnknownTypeException((object)o);
                this.WriteStartElement(n, ns, (object)o, false, (XmlSerializerNamespaces)null);
                if (needType)
                    this.WriteXsiType((string)null, "http://schemas.microsoft.com/developer/vsx-schema/2010");
                this.WriteAttribute("Version", "", o.Version);
                ObservableCollection<string> edition = o.Edition;
                if (edition != null)
                {
                    for (int index = 0; index < edition.Count; ++index)
                        this.WriteElementString("Edition", "http://schemas.microsoft.com/developer/vsx-schema/2010", edition[index]);
                }
                this.WriteEndElement((object)o);
            }
        }

        private void Write2_VsixIdentifierIsolatedShell(string n, string ns, VsixIdentifierIsolatedShell o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (!isNullable)
                    return;
                this.WriteNullTagLiteral(n, ns);
            }
            else
            {
                if (!needType && !(o.GetType() == typeof(VsixIdentifierIsolatedShell)))
                    throw this.CreateUnknownTypeException((object)o);
                this.WriteStartElement(n, ns, (object)o, false, (XmlSerializerNamespaces)null);
                if (needType)
                    this.WriteXsiType((string)null, "http://schemas.microsoft.com/developer/vsx-schema/2010");
                this.WriteAttribute("Version", "", o.Version);
                if (o.Value != null)
                    this.WriteValue(o.Value);
                this.WriteEndElement((object)o);
            }
        }

        protected override void InitCallbacks()
        {
        }
    }
}