using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace VSIXParser
{
    public class XmlSerializationReaderVsix : XmlSerializationReader
    {
        private string id32_License;
        private string id17_AssemblyName;
        private string id43_Edition;
        private string id38_VisualStudio;
        private string id18_VsixVsPackagePath;
        private string id30_Description;
        private string id7_Reference;
        private string id6_References;
        private string id28_VsixPath;
        private string id4_Version;
        private string id8_Content;
        private string id40_Item;
        private string id35_PreviewImage;
        private string id23_Id;
        private string id37_SupportedProducts;
        private string id34_Icon;
        private string id27_MoreInfoUrl;
        private string id3_Item;
        private string id39_IsolatedShell;
        private string id33_GettingStartedGuide;
        private string id15_ProjectTemplate;
        private string id10_CustomExtension;
        private string id36_InstalledByMsi;
        private string id12_MefComponent;
        private string id21_Type;
        private string id9_ToolboxControl;
        private string id16_VsixProjectTemplatePath;
        private string id26_Name;
        private string id22_VsixToolboxControlPath;
        private string id19_VsixMefComponentPath;
        private string id1_Vsix;
        private string id14_Assembly;
        private string id31_Locale;
        private string id2_Item;
        private string id24_MinVersion;
        private string id29_Author;
        private string id11_ItemTemplate;
        private string id5_Identifier;
        private string id41_SystemComponent;
        private string id20_VsixItemTemplatePath;
        private string id13_VsPackage;
        private string id42_AllUsers;
        private string id25_MaxVersion;

        public object Read18_Vsix()
        {
            object obj = (object) null;
            int num = (int) this.Reader.MoveToContent();
            if (this.Reader.NodeType == XmlNodeType.Element)
            {
                if (this.Reader.LocalName != this.id1_Vsix || this.Reader.NamespaceURI != this.id2_Item)
                    throw this.CreateUnknownNodeException();
               obj = (object) this.Read17_Vsix(false, true);
            }
            else
                this.UnknownNode((object) null, "http://schemas.microsoft.com/developer/vsx-schema/2010:Vsix");
            return obj;
        }

        private Vsix Read17_Vsix(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? this.GetXsiType() : (XmlQualifiedName) null;
            bool flag = false;
            if (isNullable)
                flag = this.ReadNull();
            if (checkType && !(type == (XmlQualifiedName) null) &&
                (type.Name != this.id3_Item || type.Namespace != this.id2_Item))
                throw this.CreateUnknownTypeException(type);
            if (flag)
                return (Vsix) null;
            Vsix vsix = new Vsix();
            if (vsix.References == null)
                vsix.References = new ObservableCollection<VsixReference>();
            ObservableCollection<VsixReference> references1 = vsix.References;
            bool[] flagArray = new bool[4];
            while (this.Reader.MoveToNextAttribute())
            {
                if (!flagArray[0] && this.Reader.LocalName == this.id4_Version &&
                    this.Reader.NamespaceURI == this.id3_Item)
                {
                    vsix.Version = this.Reader.Value;
                    flagArray[0] = true;
                }
                else if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode((object) vsix, ":Version");
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                return vsix;
            }
            else
            {
                this.Reader.ReadStartElement();
                int num1 = (int) this.Reader.MoveToContent();
                int whileIterations1 = 0;
                int readerCount1 = this.ReaderCount;
                while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
                {
                    if (this.Reader.NodeType == XmlNodeType.Element)
                    {
                        if (!flagArray[1] && this.Reader.LocalName == this.id5_Identifier &&
                            this.Reader.NamespaceURI == this.id2_Item)
                        {
                            vsix.Identifier = this.Read5_VsixIdentifier(false, true);
                            flagArray[1] = true;
                        }
                        else if (this.Reader.LocalName == this.id6_References &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            if (!this.ReadNull())
                            {
                                if (vsix.References == null)
                                    vsix.References = new ObservableCollection<VsixReference>();
                                ObservableCollection<VsixReference> references2 = vsix.References;
                                if (this.Reader.IsEmptyElement)
                                {
                                    this.Reader.Skip();
                                }
                                else
                                {
                                    this.Reader.ReadStartElement();
                                    int num2 = (int) this.Reader.MoveToContent();
                                    int whileIterations2 = 0;
                                    int readerCount2 = this.ReaderCount;
                                    while (this.Reader.NodeType != XmlNodeType.EndElement &&
                                           this.Reader.NodeType != XmlNodeType.None)
                                    {
                                        if (this.Reader.NodeType == XmlNodeType.Element)
                                        {
                                            if (this.Reader.LocalName == this.id7_Reference &&
                                                this.Reader.NamespaceURI == this.id2_Item)
                                            {
                                                if (references2 == null)
                                                    this.Reader.Skip();
                                                else
                                                    references2.Add(this.Read6_VsixReference(false, true));
                                            }
                                            else
                                                this.UnknownNode((object) null,
                                                                 "http://schemas.microsoft.com/developer/vsx-schema/2010:Reference");
                                        }
                                        else
                                            this.UnknownNode((object) null,
                                                             "http://schemas.microsoft.com/developer/vsx-schema/2010:Reference");
                                        int num3 = (int) this.Reader.MoveToContent();
                                        this.CheckReaderCount(ref whileIterations2, ref readerCount2);
                                    }
                                    this.ReadEndElement();
                                }
                            }
                        }
                        else if (!flagArray[3] && this.Reader.LocalName == this.id8_Content &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            vsix.Content = this.Read16_VsixContent(false, true);
                            flagArray[3] = true;
                        }
                        else
                            this.UnknownNode((object) vsix,
                                             "http://schemas.microsoft.com/developer/vsx-schema/2010:Identifier, http://schemas.microsoft.com/developer/vsx-schema/2010:References, http://schemas.microsoft.com/developer/vsx-schema/2010:Content");
                    }
                    else
                        this.UnknownNode((object) vsix,
                                         "http://schemas.microsoft.com/developer/vsx-schema/2010:Identifier, http://schemas.microsoft.com/developer/vsx-schema/2010:References, http://schemas.microsoft.com/developer/vsx-schema/2010:Content");
                    int num4 = (int) this.Reader.MoveToContent();
                    this.CheckReaderCount(ref whileIterations1, ref readerCount1);
                }
                this.ReadEndElement();
                return vsix;
            }
        }

        private VsixContent Read16_VsixContent(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? this.GetXsiType() : (XmlQualifiedName) null;
            bool flag = false;
            if (isNullable)
                flag = this.ReadNull();
            if (checkType && !(type == (XmlQualifiedName) null) &&
                (type.Name != this.id3_Item || type.Namespace != this.id2_Item))
                throw this.CreateUnknownTypeException(type);
            if (flag)
                return (VsixContent) null;
            VsixContent vsixContent = new VsixContent();
            object[] objArray = (object[]) null;
            int num1 = 0;
            ContentItemTypes[] contentItemTypesArray = (ContentItemTypes[]) null;
            int num2 = 0;
            while (this.Reader.MoveToNextAttribute())
            {
                if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode((object) vsixContent);
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                vsixContent.Items = (object[]) this.ShrinkArray((Array) objArray, num1, typeof (object), true);
                vsixContent.ItemsElementName =
                    (ContentItemTypes[])
                    this.ShrinkArray((Array) contentItemTypesArray, num2, typeof (ContentItemTypes), true);
                return vsixContent;
            }
            else
            {
                this.Reader.ReadStartElement();
                int num3 = (int) this.Reader.MoveToContent();
                int whileIterations = 0;
                int readerCount = this.ReaderCount;
                while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
                {
                    if (this.Reader.NodeType == XmlNodeType.Element)
                    {
                        if (this.Reader.LocalName == this.id9_ToolboxControl &&
                            this.Reader.NamespaceURI == this.id2_Item)
                        {
                            objArray = (object[]) this.EnsureArrayIndex((Array) objArray, num1, typeof (object));
                            objArray[num1++] = (object) this.Read9_VsixToolboxControlPath(false, true);
                            contentItemTypesArray =
                                (ContentItemTypes[])
                                this.EnsureArrayIndex((Array) contentItemTypesArray, num2, typeof (ContentItemTypes));
                            contentItemTypesArray[num2++] = ContentItemTypes.ToolboxControl;
                        }
                        else if (this.Reader.LocalName == this.id10_CustomExtension &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            objArray = (object[]) this.EnsureArrayIndex((Array) objArray, num1, typeof (object));
                            objArray[num1++] = (object) this.Read10_VsixCustomExtension(false, true);
                            contentItemTypesArray =
                                (ContentItemTypes[])
                                this.EnsureArrayIndex((Array) contentItemTypesArray, num2, typeof (ContentItemTypes));
                            contentItemTypesArray[num2++] = ContentItemTypes.CustomExtension;
                        }
                        else if (this.Reader.LocalName == this.id11_ItemTemplate &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            objArray = (object[]) this.EnsureArrayIndex((Array) objArray, num1, typeof (object));
                            objArray[num1++] = (object) this.Read11_VsixItemTemplatePath(false, true);
                            contentItemTypesArray =
                                (ContentItemTypes[])
                                this.EnsureArrayIndex((Array) contentItemTypesArray, num2,
                                                      typeof (ContentItemTypes));
                            contentItemTypesArray[num2++] = ContentItemTypes.ItemTemplate;
                        }
                        else if (this.Reader.LocalName == this.id12_MefComponent &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            objArray =
                                (object[]) this.EnsureArrayIndex((Array) objArray, num1, typeof (object));
                            objArray[num1++] = (object) this.Read12_VsixMefComponentPath(false, true);
                            contentItemTypesArray =
                                (ContentItemTypes[])
                                this.EnsureArrayIndex((Array) contentItemTypesArray, num2,
                                                      typeof (ContentItemTypes));
                            contentItemTypesArray[num2++] = ContentItemTypes.MefComponent;
                        }
                        else if (this.Reader.LocalName == this.id13_VsPackage &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            objArray =
                                (object[])
                                this.EnsureArrayIndex((Array) objArray, num1, typeof (object));
                            objArray[num1++] = (object) this.Read13_VsixVsPackagePath(false, true);
                            contentItemTypesArray =
                                (ContentItemTypes[])
                                this.EnsureArrayIndex((Array) contentItemTypesArray, num2,
                                                      typeof (ContentItemTypes));
                            contentItemTypesArray[num2++] = ContentItemTypes.VsPackage;
                        }
                        else if (this.Reader.LocalName == this.id14_Assembly &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            objArray =
                                (object[])
                                this.EnsureArrayIndex((Array) objArray, num1, typeof (object));
                            objArray[num1++] = (object) this.Read14_VsixAssembly(false, true);
                            contentItemTypesArray =
                                (ContentItemTypes[])
                                this.EnsureArrayIndex((Array) contentItemTypesArray, num2,
                                                      typeof (ContentItemTypes));
                            contentItemTypesArray[num2++] = ContentItemTypes.Assembly;
                        }
                        else if (this.Reader.LocalName == this.id15_ProjectTemplate &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            objArray =
                                (object[])
                                this.EnsureArrayIndex((Array) objArray, num1, typeof (object));
                            objArray[num1++] =
                                (object) this.Read15_VsixProjectTemplatePath(false, true);
                            contentItemTypesArray =
                                (ContentItemTypes[])
                                this.EnsureArrayIndex((Array) contentItemTypesArray, num2,
                                                      typeof (ContentItemTypes));
                            contentItemTypesArray[num2++] = ContentItemTypes.ProjectTemplate;
                        }
                        else
                            this.UnknownNode((object) vsixContent,
                                             "http://schemas.microsoft.com/developer/vsx-schema/2010:ToolboxControl, http://schemas.microsoft.com/developer/vsx-schema/2010:CustomExtension, http://schemas.microsoft.com/developer/vsx-schema/2010:ItemTemplate, http://schemas.microsoft.com/developer/vsx-schema/2010:MefComponent, http://schemas.microsoft.com/developer/vsx-schema/2010:VsPackage, http://schemas.microsoft.com/developer/vsx-schema/2010:Assembly, http://schemas.microsoft.com/developer/vsx-schema/2010:ProjectTemplate");
                    }
                    else
                        this.UnknownNode((object) vsixContent,
                                         "http://schemas.microsoft.com/developer/vsx-schema/2010:ToolboxControl, http://schemas.microsoft.com/developer/vsx-schema/2010:CustomExtension, http://schemas.microsoft.com/developer/vsx-schema/2010:ItemTemplate, http://schemas.microsoft.com/developer/vsx-schema/2010:MefComponent, http://schemas.microsoft.com/developer/vsx-schema/2010:VsPackage, http://schemas.microsoft.com/developer/vsx-schema/2010:Assembly, http://schemas.microsoft.com/developer/vsx-schema/2010:ProjectTemplate");
                    int num4 = (int) this.Reader.MoveToContent();
                    this.CheckReaderCount(ref whileIterations, ref readerCount);
                }
                vsixContent.Items = (object[]) this.ShrinkArray((Array) objArray, num1, typeof (object), true);
                vsixContent.ItemsElementName =
                    (ContentItemTypes[])
                    this.ShrinkArray((Array) contentItemTypesArray, num2, typeof (ContentItemTypes), true);
                this.ReadEndElement();
                return vsixContent;
            }
        }

        private VsixProjectTemplatePath Read15_VsixProjectTemplatePath(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? this.GetXsiType() : (XmlQualifiedName) null;
            bool flag = false;
            if (isNullable)
                flag = this.ReadNull();
            if (checkType && !(type == (XmlQualifiedName) null) &&
                (type.Name != this.id16_VsixProjectTemplatePath || type.Namespace != this.id2_Item))
                throw this.CreateUnknownTypeException(type);
            if (flag)
                return (VsixProjectTemplatePath) null;
            VsixProjectTemplatePath projectTemplatePath = new VsixProjectTemplatePath();
            while (this.Reader.MoveToNextAttribute())
            {
                if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode((object) projectTemplatePath);
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                return projectTemplatePath;
            }
            else
            {
                this.Reader.ReadStartElement();
                int num1 = (int) this.Reader.MoveToContent();
                int whileIterations = 0;
                int readerCount = this.ReaderCount;
                while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
                {
                    string str1 = (string) null;
                    if (this.Reader.NodeType == XmlNodeType.Element)
                        this.UnknownNode((object) projectTemplatePath, "");
                    else if (this.Reader.NodeType == XmlNodeType.Text || this.Reader.NodeType == XmlNodeType.CDATA ||
                             (this.Reader.NodeType == XmlNodeType.Whitespace ||
                              this.Reader.NodeType == XmlNodeType.SignificantWhitespace))
                    {
                        string str2 = this.ReadString(str1, false);
                        projectTemplatePath.Value = str2;
                    }
                    else
                        this.UnknownNode((object) projectTemplatePath, "");
                    int num2 = (int) this.Reader.MoveToContent();
                    this.CheckReaderCount(ref whileIterations, ref readerCount);
                }
                this.ReadEndElement();
                return projectTemplatePath;
            }
        }

        private VsixAssembly Read14_VsixAssembly(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? this.GetXsiType() : (XmlQualifiedName) null;
            bool flag = false;
            if (isNullable)
                flag = this.ReadNull();
            if (checkType && !(type == (XmlQualifiedName) null) &&
                (type.Name != this.id3_Item || type.Namespace != this.id2_Item))
                throw this.CreateUnknownTypeException(type);
            if (flag)
                return (VsixAssembly) null;
            VsixAssembly vsixAssembly = new VsixAssembly();
            bool[] flagArray = new bool[2];
            while (this.Reader.MoveToNextAttribute())
            {
                if (!flagArray[0] && this.Reader.LocalName == this.id17_AssemblyName &&
                    this.Reader.NamespaceURI == this.id3_Item)
                {
                    vsixAssembly.AssemblyName = this.Reader.Value;
                    flagArray[0] = true;
                }
                else if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode((object) vsixAssembly, ":AssemblyName");
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                return vsixAssembly;
            }
            else
            {
                this.Reader.ReadStartElement();
                int num1 = (int) this.Reader.MoveToContent();
                int whileIterations = 0;
                int readerCount = this.ReaderCount;
                while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
                {
                    string str1 = (string) null;
                    if (this.Reader.NodeType == XmlNodeType.Element)
                        this.UnknownNode((object) vsixAssembly, "");
                    else if (this.Reader.NodeType == XmlNodeType.Text || this.Reader.NodeType == XmlNodeType.CDATA ||
                             (this.Reader.NodeType == XmlNodeType.Whitespace ||
                              this.Reader.NodeType == XmlNodeType.SignificantWhitespace))
                    {
                        string str2 = this.ReadString(str1, false);
                        vsixAssembly.Value = str2;
                    }
                    else
                        this.UnknownNode((object) vsixAssembly, "");
                    int num2 = (int) this.Reader.MoveToContent();
                    this.CheckReaderCount(ref whileIterations, ref readerCount);
                }
                this.ReadEndElement();
                return vsixAssembly;
            }
        }

        private VsixVsPackagePath Read13_VsixVsPackagePath(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? this.GetXsiType() : (XmlQualifiedName) null;
            bool flag = false;
            if (isNullable)
                flag = this.ReadNull();
            if (checkType && !(type == (XmlQualifiedName) null) &&
                (type.Name != this.id18_VsixVsPackagePath || type.Namespace != this.id2_Item))
                throw this.CreateUnknownTypeException(type);
            if (flag)
                return (VsixVsPackagePath) null;
            VsixVsPackagePath vsixVsPackagePath = new VsixVsPackagePath();
            while (this.Reader.MoveToNextAttribute())
            {
                if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode((object) vsixVsPackagePath);
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                return vsixVsPackagePath;
            }
            else
            {
                this.Reader.ReadStartElement();
                int num1 = (int) this.Reader.MoveToContent();
                int whileIterations = 0;
                int readerCount = this.ReaderCount;
                while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
                {
                    string str1 = (string) null;
                    if (this.Reader.NodeType == XmlNodeType.Element)
                        this.UnknownNode((object) vsixVsPackagePath, "");
                    else if (this.Reader.NodeType == XmlNodeType.Text || this.Reader.NodeType == XmlNodeType.CDATA ||
                             (this.Reader.NodeType == XmlNodeType.Whitespace ||
                              this.Reader.NodeType == XmlNodeType.SignificantWhitespace))
                    {
                        string str2 = this.ReadString(str1, false);
                        vsixVsPackagePath.Value = str2;
                    }
                    else
                        this.UnknownNode((object) vsixVsPackagePath, "");
                    int num2 = (int) this.Reader.MoveToContent();
                    this.CheckReaderCount(ref whileIterations, ref readerCount);
                }
                this.ReadEndElement();
                return vsixVsPackagePath;
            }
        }

        private VsixMefComponentPath Read12_VsixMefComponentPath(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? this.GetXsiType() : (XmlQualifiedName) null;
            bool flag = false;
            if (isNullable)
                flag = this.ReadNull();
            if (checkType && !(type == (XmlQualifiedName) null) &&
                (type.Name != this.id19_VsixMefComponentPath || type.Namespace != this.id2_Item))
                throw this.CreateUnknownTypeException(type);
            if (flag)
                return (VsixMefComponentPath) null;
            VsixMefComponentPath mefComponentPath = new VsixMefComponentPath();
            while (this.Reader.MoveToNextAttribute())
            {
                if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode((object) mefComponentPath);
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                return mefComponentPath;
            }
            else
            {
                this.Reader.ReadStartElement();
                int num1 = (int) this.Reader.MoveToContent();
                int whileIterations = 0;
                int readerCount = this.ReaderCount;
                while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
                {
                    string str1 = (string) null;
                    if (this.Reader.NodeType == XmlNodeType.Element)
                        this.UnknownNode((object) mefComponentPath, "");
                    else if (this.Reader.NodeType == XmlNodeType.Text || this.Reader.NodeType == XmlNodeType.CDATA ||
                             (this.Reader.NodeType == XmlNodeType.Whitespace ||
                              this.Reader.NodeType == XmlNodeType.SignificantWhitespace))
                    {
                        string str2 = this.ReadString(str1, false);
                        mefComponentPath.Value = str2;
                    }
                    else
                        this.UnknownNode((object) mefComponentPath, "");
                    int num2 = (int) this.Reader.MoveToContent();
                    this.CheckReaderCount(ref whileIterations, ref readerCount);
                }
                this.ReadEndElement();
                return mefComponentPath;
            }
        }

        private VsixItemTemplatePath Read11_VsixItemTemplatePath(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? this.GetXsiType() : (XmlQualifiedName) null;
            bool flag = false;
            if (isNullable)
                flag = this.ReadNull();
            if (checkType && !(type == (XmlQualifiedName) null) &&
                (type.Name != this.id20_VsixItemTemplatePath || type.Namespace != this.id2_Item))
                throw this.CreateUnknownTypeException(type);
            if (flag)
                return (VsixItemTemplatePath) null;
            VsixItemTemplatePath itemTemplatePath = new VsixItemTemplatePath();
            while (this.Reader.MoveToNextAttribute())
            {
                if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode((object) itemTemplatePath);
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                return itemTemplatePath;
            }
            else
            {
                this.Reader.ReadStartElement();
                int num1 = (int) this.Reader.MoveToContent();
                int whileIterations = 0;
                int readerCount = this.ReaderCount;
                while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
                {
                    string str1 = (string) null;
                    if (this.Reader.NodeType == XmlNodeType.Element)
                        this.UnknownNode((object) itemTemplatePath, "");
                    else if (this.Reader.NodeType == XmlNodeType.Text || this.Reader.NodeType == XmlNodeType.CDATA ||
                             (this.Reader.NodeType == XmlNodeType.Whitespace ||
                              this.Reader.NodeType == XmlNodeType.SignificantWhitespace))
                    {
                        string str2 = this.ReadString(str1, false);
                        itemTemplatePath.Value = str2;
                    }
                    else
                        this.UnknownNode((object) itemTemplatePath, "");
                    int num2 = (int) this.Reader.MoveToContent();
                    this.CheckReaderCount(ref whileIterations, ref readerCount);
                }
                this.ReadEndElement();
                return itemTemplatePath;
            }
        }

        private VsixCustomExtension Read10_VsixCustomExtension(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? this.GetXsiType() : (XmlQualifiedName) null;
            bool flag = false;
            if (isNullable)
                flag = this.ReadNull();
            if (checkType && !(type == (XmlQualifiedName) null) &&
                (type.Name != this.id3_Item || type.Namespace != this.id2_Item))
                throw this.CreateUnknownTypeException(type);
            if (flag)
                return (VsixCustomExtension) null;
            VsixCustomExtension vsixCustomExtension = new VsixCustomExtension();
            XmlAttribute[] xmlAttributeArray = (XmlAttribute[]) null;
            int num1 = 0;
            bool[] flagArray = new bool[3];
            while (this.Reader.MoveToNextAttribute())
            {
                if (!flagArray[0] && this.Reader.LocalName == this.id21_Type &&
                    this.Reader.NamespaceURI == this.id3_Item)
                {
                    vsixCustomExtension.Type = this.Reader.Value;
                    flagArray[0] = true;
                }
                else if (!this.IsXmlnsAttribute(this.Reader.Name))
                {
                    XmlAttribute attr = (XmlAttribute) this.Document.ReadNode(this.Reader);
                    this.ParseWsdlArrayType(attr);
                    xmlAttributeArray =
                        (XmlAttribute[]) this.EnsureArrayIndex((Array) xmlAttributeArray, num1, typeof (XmlAttribute));
                    xmlAttributeArray[num1++] = attr;
                }
            }
            vsixCustomExtension.AnyAttr =
                (XmlAttribute[]) this.ShrinkArray((Array) xmlAttributeArray, num1, typeof (XmlAttribute), true);
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                vsixCustomExtension.AnyAttr =
                    (XmlAttribute[]) this.ShrinkArray((Array) xmlAttributeArray, num1, typeof (XmlAttribute), true);
                return vsixCustomExtension;
            }
            else
            {
                this.Reader.ReadStartElement();
                int num2 = (int) this.Reader.MoveToContent();
                int whileIterations = 0;
                int readerCount = this.ReaderCount;
                while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
                {
                    string str1 = (string) null;
                    if (this.Reader.NodeType == XmlNodeType.Element)
                        this.UnknownNode((object) vsixCustomExtension, "");
                    else if (this.Reader.NodeType == XmlNodeType.Text || this.Reader.NodeType == XmlNodeType.CDATA ||
                             (this.Reader.NodeType == XmlNodeType.Whitespace ||
                              this.Reader.NodeType == XmlNodeType.SignificantWhitespace))
                    {
                        string str2 = this.ReadString(str1, false);
                        vsixCustomExtension.Value = str2;
                    }
                    else
                        this.UnknownNode((object) vsixCustomExtension, "");
                    int num3 = (int) this.Reader.MoveToContent();
                    this.CheckReaderCount(ref whileIterations, ref readerCount);
                }
                vsixCustomExtension.AnyAttr =
                    (XmlAttribute[]) this.ShrinkArray((Array) xmlAttributeArray, num1, typeof (XmlAttribute), true);
                this.ReadEndElement();
                return vsixCustomExtension;
            }
        }

        private VsixToolboxControlPath Read9_VsixToolboxControlPath(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? this.GetXsiType() : (XmlQualifiedName) null;
            bool flag = false;
            if (isNullable)
                flag = this.ReadNull();
            if (checkType && !(type == (XmlQualifiedName) null) &&
                (type.Name != this.id22_VsixToolboxControlPath || type.Namespace != this.id2_Item))
                throw this.CreateUnknownTypeException(type);
            if (flag)
                return (VsixToolboxControlPath) null;
            VsixToolboxControlPath toolboxControlPath = new VsixToolboxControlPath();
            while (this.Reader.MoveToNextAttribute())
            {
                if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode((object) toolboxControlPath);
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                return toolboxControlPath;
            }
            else
            {
                this.Reader.ReadStartElement();
                int num1 = (int) this.Reader.MoveToContent();
                int whileIterations = 0;
                int readerCount = this.ReaderCount;
                while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
                {
                    string str1 = (string) null;
                    if (this.Reader.NodeType == XmlNodeType.Element)
                        this.UnknownNode((object) toolboxControlPath, "");
                    else if (this.Reader.NodeType == XmlNodeType.Text || this.Reader.NodeType == XmlNodeType.CDATA ||
                             (this.Reader.NodeType == XmlNodeType.Whitespace ||
                              this.Reader.NodeType == XmlNodeType.SignificantWhitespace))
                    {
                        string str2 = this.ReadString(str1, false);
                        toolboxControlPath.Value = str2;
                    }
                    else
                        this.UnknownNode((object) toolboxControlPath, "");
                    int num2 = (int) this.Reader.MoveToContent();
                    this.CheckReaderCount(ref whileIterations, ref readerCount);
                }
                this.ReadEndElement();
                return toolboxControlPath;
            }
        }

        private VsixReference Read6_VsixReference(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? this.GetXsiType() : (XmlQualifiedName) null;
            bool flag = false;
            if (isNullable)
                flag = this.ReadNull();
            if (checkType && !(type == (XmlQualifiedName) null) &&
                (type.Name != this.id3_Item || type.Namespace != this.id2_Item))
                throw this.CreateUnknownTypeException(type);
            if (flag)
                return (VsixReference) null;
            VsixReference vsixReference = new VsixReference();
            bool[] flagArray = new bool[6];
            while (this.Reader.MoveToNextAttribute())
            {
                if (!flagArray[3] && this.Reader.LocalName == this.id23_Id && this.Reader.NamespaceURI == this.id3_Item)
                {
                    vsixReference.Id = this.Reader.Value;
                    flagArray[3] = true;
                }
                else if (!flagArray[4] && this.Reader.LocalName == this.id24_MinVersion &&
                         this.Reader.NamespaceURI == this.id3_Item)
                {
                    vsixReference.MinVersion = this.Reader.Value;
                    flagArray[4] = true;
                }
                else if (!flagArray[5] && this.Reader.LocalName == this.id25_MaxVersion &&
                         this.Reader.NamespaceURI == this.id3_Item)
                {
                    vsixReference.MaxVersion = this.Reader.Value;
                    flagArray[5] = true;
                }
                else if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode((object) vsixReference, ":Id, :MinVersion, :MaxVersion");
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                return vsixReference;
            }
            else
            {
                this.Reader.ReadStartElement();
                int num1 = (int) this.Reader.MoveToContent();
                int whileIterations = 0;
                int readerCount = this.ReaderCount;
                while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
                {
                    if (this.Reader.NodeType == XmlNodeType.Element)
                    {
                        if (!flagArray[0] && this.Reader.LocalName == this.id26_Name &&
                            this.Reader.NamespaceURI == this.id2_Item)
                        {
                            vsixReference.Name = this.Reader.ReadElementString();
                            flagArray[0] = true;
                        }
                        else if (!flagArray[1] && this.Reader.LocalName == this.id27_MoreInfoUrl &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            vsixReference.MoreInfoUrl = this.Reader.ReadElementString();
                            flagArray[1] = true;
                        }
                        else if (!flagArray[2] && this.Reader.LocalName == this.id28_VsixPath &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            vsixReference.VsixPath = this.Reader.ReadElementString();
                            flagArray[2] = true;
                        }
                        else
                            this.UnknownNode((object) vsixReference,
                                             "http://schemas.microsoft.com/developer/vsx-schema/2010:Name, http://schemas.microsoft.com/developer/vsx-schema/2010:MoreInfoUrl, http://schemas.microsoft.com/developer/vsx-schema/2010:VsixPath");
                    }
                    else
                        this.UnknownNode((object) vsixReference,
                                         "http://schemas.microsoft.com/developer/vsx-schema/2010:Name, http://schemas.microsoft.com/developer/vsx-schema/2010:MoreInfoUrl, http://schemas.microsoft.com/developer/vsx-schema/2010:VsixPath");
                    int num2 = (int) this.Reader.MoveToContent();
                    this.CheckReaderCount(ref whileIterations, ref readerCount);
                }
                this.ReadEndElement();
                return vsixReference;
            }
        }

        private VsixIdentifier Read5_VsixIdentifier(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? this.GetXsiType() : (XmlQualifiedName) null;
            bool flag = false;
            if (isNullable)
                flag = this.ReadNull();
            if (checkType && !(type == (XmlQualifiedName) null) &&
                (type.Name != this.id3_Item || type.Namespace != this.id2_Item))
                throw this.CreateUnknownTypeException(type);
            if (flag)
                return (VsixIdentifier) null;
            VsixIdentifier vsixIdentifier = new VsixIdentifier();
            if (vsixIdentifier.SupportedProducts == null)
                vsixIdentifier.SupportedProducts = new ObservableCollection<object>();
            ObservableCollection<object> supportedProducts1 = vsixIdentifier.SupportedProducts;
            bool[] flagArray = new bool[16];
            while (this.Reader.MoveToNextAttribute())
            {
                if (!flagArray[15] && this.Reader.LocalName == this.id23_Id && this.Reader.NamespaceURI == this.id3_Item)
                {
                    vsixIdentifier.Id = this.Reader.Value;
                    flagArray[15] = true;
                }
                else if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode((object) vsixIdentifier, ":Id");
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                return vsixIdentifier;
            }
            else
            {
                this.Reader.ReadStartElement();
                int num1 = (int) this.Reader.MoveToContent();
                int whileIterations1 = 0;
                int readerCount1 = this.ReaderCount;
                while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
                {
                    if (this.Reader.NodeType == XmlNodeType.Element)
                    {
                        if (!flagArray[0] && this.Reader.LocalName == this.id26_Name &&
                            this.Reader.NamespaceURI == this.id2_Item)
                        {
                            vsixIdentifier.Name = this.Reader.ReadElementString();
                            flagArray[0] = true;
                        }
                        else if (!flagArray[1] && this.Reader.LocalName == this.id29_Author &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            vsixIdentifier.Author = this.Reader.ReadElementString();
                            flagArray[1] = true;
                        }
                        else if (!flagArray[2] && this.Reader.LocalName == this.id4_Version &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            vsixIdentifier.Version = this.Reader.ReadElementString();
                            flagArray[2] = true;
                        }
                        else if (!flagArray[3] && this.Reader.LocalName == this.id30_Description &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            vsixIdentifier.Description = this.Reader.ReadElementString();
                            flagArray[3] = true;
                        }
                        else if (!flagArray[4] && this.Reader.LocalName == this.id31_Locale &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            vsixIdentifier.Locale = XmlConvert.ToUInt16(this.Reader.ReadElementString());
                            flagArray[4] = true;
                        }
                        else if (!flagArray[5] && this.Reader.LocalName == this.id27_MoreInfoUrl &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            vsixIdentifier.MoreInfoUrl = this.Reader.ReadElementString();
                            flagArray[5] = true;
                        }
                        else if (!flagArray[6] && this.Reader.LocalName == this.id32_License &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            vsixIdentifier.License = this.Reader.ReadElementString();
                            flagArray[6] = true;
                        }
                        else if (!flagArray[7] &&
                                 this.Reader.LocalName == this.id33_GettingStartedGuide &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            vsixIdentifier.GettingStartedGuide =
                                this.Reader.ReadElementString();
                            flagArray[7] = true;
                        }
                        else if (!flagArray[8] && this.Reader.LocalName == this.id34_Icon &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            vsixIdentifier.Icon = this.Reader.ReadElementString();
                            flagArray[8] = true;
                        }
                        else if (!flagArray[9] &&
                                 this.Reader.LocalName == this.id35_PreviewImage &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            vsixIdentifier.PreviewImage =
                                this.Reader.ReadElementString();
                            flagArray[9] = true;
                        }
                        else if (!flagArray[10] &&
                                 this.Reader.LocalName == this.id36_InstalledByMsi &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            vsixIdentifier.InstalledByMsiSpecified = true;
                            vsixIdentifier.InstalledByMsi =
                                XmlConvert.ToBoolean(
                                    this.Reader.ReadElementString());
                            flagArray[10] = true;
                        }
                        else if (this.Reader.LocalName ==
                                 this.id37_SupportedProducts &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            if (!this.ReadNull())
                            {
                                if (vsixIdentifier.SupportedProducts == null)
                                    vsixIdentifier.SupportedProducts =
                                        new ObservableCollection<object>();
                                ObservableCollection<object>
                                    supportedProducts2 =
                                        vsixIdentifier.SupportedProducts;
                                if (this.Reader.IsEmptyElement)
                                {
                                    this.Reader.Skip();
                                }
                                else
                                {
                                    this.Reader.ReadStartElement();
                                    int num2 =
                                        (int) this.Reader.MoveToContent();
                                    int whileIterations2 = 0;
                                    int readerCount2 = this.ReaderCount;
                                    while (this.Reader.NodeType !=
                                           XmlNodeType.EndElement &&
                                           this.Reader.NodeType !=
                                           XmlNodeType.None)
                                    {
                                        if (this.Reader.NodeType ==
                                            XmlNodeType.Element)
                                        {
                                            if (this.Reader.LocalName ==
                                                this.id38_VisualStudio &&
                                                this.Reader.NamespaceURI ==
                                                this.id2_Item)
                                            {
                                                if (supportedProducts2 ==
                                                    null)
                                                    this.Reader.Skip();
                                                else
                                                    supportedProducts2.Add(
                                                        (object)
                                                        this.
                                                            Read3_VsixIdentifierVisualStudio
                                                            (false, true));
                                            }
                                            else if (this.Reader.LocalName ==
                                                     this.id39_IsolatedShell &&
                                                     this.Reader.NamespaceURI ==
                                                     this.id2_Item)
                                            {
                                                if (
                                                    supportedProducts2 ==
                                                    null)
                                                    this.Reader.Skip();
                                                else
                                                    supportedProducts2.
                                                        Add(
                                                            (object)
                                                            this.
                                                                Read2_VsixIdentifierIsolatedShell
                                                                (false,
                                                                 true));
                                            }
                                            else
                                                this.UnknownNode(
                                                    (object) null,
                                                    "http://schemas.microsoft.com/developer/vsx-schema/2010:VisualStudio, http://schemas.microsoft.com/developer/vsx-schema/2010:IsolatedShell");
                                        }
                                        else
                                            this.UnknownNode((object) null,
                                                             "http://schemas.microsoft.com/developer/vsx-schema/2010:VisualStudio, http://schemas.microsoft.com/developer/vsx-schema/2010:IsolatedShell");
                                        int num3 =
                                            (int)
                                            this.Reader.MoveToContent();
                                        this.CheckReaderCount(
                                            ref whileIterations2,
                                            ref readerCount2);
                                    }
                                    this.ReadEndElement();
                                }
                            }
                        }
                        else if (!flagArray[12] &&
                                 this.Reader.LocalName == this.id40_Item &&
                                 this.Reader.NamespaceURI == this.id2_Item)
                        {
                            vsixIdentifier.
                                SupportedFrameworkRuntimeEdition =
                                this.Read4_Item(false, true);
                            flagArray[12] = true;
                        }
                        else if (!flagArray[13] &&
                                 this.Reader.LocalName ==
                                 this.id41_SystemComponent &&
                                 this.Reader.NamespaceURI ==
                                 this.id2_Item)
                        {
                            vsixIdentifier.SystemComponentSpecified
                                = true;
                            vsixIdentifier.SystemComponent =
                                XmlConvert.ToBoolean(
                                    this.Reader.ReadElementString());
                            flagArray[13] = true;
                        }
                        else if (!flagArray[14] &&
                                 this.Reader.LocalName ==
                                 this.id42_AllUsers &&
                                 this.Reader.NamespaceURI ==
                                 this.id2_Item)
                        {
                            vsixIdentifier.AllUsersSpecified =
                                true;
                            vsixIdentifier.AllUsers =
                                XmlConvert.ToBoolean(
                                    this.Reader.
                                        ReadElementString());
                            flagArray[14] = true;
                        }
                        else
                            this.UnknownNode(
                                (object) vsixIdentifier,
                                "http://schemas.microsoft.com/developer/vsx-schema/2010:Name, http://schemas.microsoft.com/developer/vsx-schema/2010:Author, http://schemas.microsoft.com/developer/vsx-schema/2010:Version, http://schemas.microsoft.com/developer/vsx-schema/2010:Description, http://schemas.microsoft.com/developer/vsx-schema/2010:Locale, http://schemas.microsoft.com/developer/vsx-schema/2010:MoreInfoUrl, http://schemas.microsoft.com/developer/vsx-schema/2010:License, http://schemas.microsoft.com/developer/vsx-schema/2010:GettingStartedGuide, http://schemas.microsoft.com/developer/vsx-schema/2010:Icon, http://schemas.microsoft.com/developer/vsx-schema/2010:PreviewImage, http://schemas.microsoft.com/developer/vsx-schema/2010:InstalledByMsi, http://schemas.microsoft.com/developer/vsx-schema/2010:SupportedProducts, http://schemas.microsoft.com/developer/vsx-schema/2010:SupportedFrameworkRuntimeEdition, http://schemas.microsoft.com/developer/vsx-schema/2010:SystemComponent, http://schemas.microsoft.com/developer/vsx-schema/2010:AllUsers");
                    }
                    else
                        this.UnknownNode((object) vsixIdentifier,
                                         "http://schemas.microsoft.com/developer/vsx-schema/2010:Name, http://schemas.microsoft.com/developer/vsx-schema/2010:Author, http://schemas.microsoft.com/developer/vsx-schema/2010:Version, http://schemas.microsoft.com/developer/vsx-schema/2010:Description, http://schemas.microsoft.com/developer/vsx-schema/2010:Locale, http://schemas.microsoft.com/developer/vsx-schema/2010:MoreInfoUrl, http://schemas.microsoft.com/developer/vsx-schema/2010:License, http://schemas.microsoft.com/developer/vsx-schema/2010:GettingStartedGuide, http://schemas.microsoft.com/developer/vsx-schema/2010:Icon, http://schemas.microsoft.com/developer/vsx-schema/2010:PreviewImage, http://schemas.microsoft.com/developer/vsx-schema/2010:InstalledByMsi, http://schemas.microsoft.com/developer/vsx-schema/2010:SupportedProducts, http://schemas.microsoft.com/developer/vsx-schema/2010:SupportedFrameworkRuntimeEdition, http://schemas.microsoft.com/developer/vsx-schema/2010:SystemComponent, http://schemas.microsoft.com/developer/vsx-schema/2010:AllUsers");
                    int num4 = (int) this.Reader.MoveToContent();
                    this.CheckReaderCount(ref whileIterations1, ref readerCount1);
                }
                this.ReadEndElement();
                return vsixIdentifier;
            }
        }

        private VsixIdentifierSupportedFrameworkRuntimeEdition Read4_Item(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? this.GetXsiType() : (XmlQualifiedName) null;
            bool flag = false;
            if (isNullable)
                flag = this.ReadNull();
            if (checkType && !(type == (XmlQualifiedName) null) &&
                (type.Name != this.id3_Item || type.Namespace != this.id2_Item))
                throw this.CreateUnknownTypeException(type);
            if (flag)
                return (VsixIdentifierSupportedFrameworkRuntimeEdition) null;
            VsixIdentifierSupportedFrameworkRuntimeEdition frameworkRuntimeEdition =
                new VsixIdentifierSupportedFrameworkRuntimeEdition();
            bool[] flagArray = new bool[2];
            while (this.Reader.MoveToNextAttribute())
            {
                if (!flagArray[0] && this.Reader.LocalName == this.id24_MinVersion &&
                    this.Reader.NamespaceURI == this.id3_Item)
                {
                    frameworkRuntimeEdition.MinVersion = this.Reader.Value;
                    flagArray[0] = true;
                }
                else if (!flagArray[1] && this.Reader.LocalName == this.id25_MaxVersion &&
                         this.Reader.NamespaceURI == this.id3_Item)
                {
                    frameworkRuntimeEdition.MaxVersion = this.Reader.Value;
                    flagArray[1] = true;
                }
                else if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode((object) frameworkRuntimeEdition, ":MinVersion, :MaxVersion");
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                return frameworkRuntimeEdition;
            }
            else
            {
                this.Reader.ReadStartElement();
                int num1 = (int) this.Reader.MoveToContent();
                int whileIterations = 0;
                int readerCount = this.ReaderCount;
                while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
                {
                    if (this.Reader.NodeType == XmlNodeType.Element)
                        this.UnknownNode((object) frameworkRuntimeEdition, "");
                    else
                        this.UnknownNode((object) frameworkRuntimeEdition, "");
                    int num2 = (int) this.Reader.MoveToContent();
                    this.CheckReaderCount(ref whileIterations, ref readerCount);
                }
                this.ReadEndElement();
                return frameworkRuntimeEdition;
            }
        }

        private VsixIdentifierIsolatedShell Read2_VsixIdentifierIsolatedShell(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? this.GetXsiType() : (XmlQualifiedName) null;
            bool flag = false;
            if (isNullable)
                flag = this.ReadNull();
            if (checkType && !(type == (XmlQualifiedName) null) &&
                (type.Name != this.id3_Item || type.Namespace != this.id2_Item))
                throw this.CreateUnknownTypeException(type);
            if (flag)
                return (VsixIdentifierIsolatedShell) null;
            VsixIdentifierIsolatedShell identifierIsolatedShell = new VsixIdentifierIsolatedShell();
            bool[] flagArray = new bool[2];
            while (this.Reader.MoveToNextAttribute())
            {
                if (!flagArray[0] && this.Reader.LocalName == this.id4_Version &&
                    this.Reader.NamespaceURI == this.id3_Item)
                {
                    identifierIsolatedShell.Version = this.Reader.Value;
                    flagArray[0] = true;
                }
                else if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode((object) identifierIsolatedShell, ":Version");
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                return identifierIsolatedShell;
            }
            else
            {
                this.Reader.ReadStartElement();
                int num1 = (int) this.Reader.MoveToContent();
                int whileIterations = 0;
                int readerCount = this.ReaderCount;
                while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
                {
                    string str1 = (string) null;
                    if (this.Reader.NodeType == XmlNodeType.Element)
                        this.UnknownNode((object) identifierIsolatedShell, "");
                    else if (this.Reader.NodeType == XmlNodeType.Text || this.Reader.NodeType == XmlNodeType.CDATA ||
                             (this.Reader.NodeType == XmlNodeType.Whitespace ||
                              this.Reader.NodeType == XmlNodeType.SignificantWhitespace))
                    {
                        string str2 = this.ReadString(str1, false);
                        identifierIsolatedShell.Value = str2;
                    }
                    else
                        this.UnknownNode((object) identifierIsolatedShell, "");
                    int num2 = (int) this.Reader.MoveToContent();
                    this.CheckReaderCount(ref whileIterations, ref readerCount);
                }
                this.ReadEndElement();
                return identifierIsolatedShell;
            }
        }

        private VsixIdentifierVisualStudio Read3_VsixIdentifierVisualStudio(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? this.GetXsiType() : (XmlQualifiedName) null;
            bool flag = false;
            if (isNullable)
                flag = this.ReadNull();
            if (checkType && !(type == (XmlQualifiedName) null) &&
                (type.Name != this.id3_Item || type.Namespace != this.id2_Item))
                throw this.CreateUnknownTypeException(type);
            if (flag)
                return (VsixIdentifierVisualStudio) null;
            VsixIdentifierVisualStudio identifierVisualStudio = new VsixIdentifierVisualStudio();
            if (identifierVisualStudio.Edition == null)
                identifierVisualStudio.Edition = new ObservableCollection<string>();
            ObservableCollection<string> edition = identifierVisualStudio.Edition;
            bool[] flagArray = new bool[2];
            while (this.Reader.MoveToNextAttribute())
            {
                if (!flagArray[1] && this.Reader.LocalName == this.id4_Version &&
                    this.Reader.NamespaceURI == this.id3_Item)
                {
                    identifierVisualStudio.Version = this.Reader.Value;
                    flagArray[1] = true;
                }
                else if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode((object) identifierVisualStudio, ":Version");
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                return identifierVisualStudio;
            }
            else
            {
                this.Reader.ReadStartElement();
                int num1 = (int) this.Reader.MoveToContent();
                int whileIterations = 0;
                int readerCount = this.ReaderCount;
                while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
                {
                    if (this.Reader.NodeType == XmlNodeType.Element)
                    {
                        if (this.Reader.LocalName == this.id43_Edition && this.Reader.NamespaceURI == this.id2_Item)
                            edition.Add(this.Reader.ReadElementString());
                        else
                            this.UnknownNode((object) identifierVisualStudio,
                                             "http://schemas.microsoft.com/developer/vsx-schema/2010:Edition");
                    }
                    else
                        this.UnknownNode((object) identifierVisualStudio,
                                         "http://schemas.microsoft.com/developer/vsx-schema/2010:Edition");
                    int num2 = (int) this.Reader.MoveToContent();
                    this.CheckReaderCount(ref whileIterations, ref readerCount);
                }
                this.ReadEndElement();
                return identifierVisualStudio;
            }
        }

        protected override void InitCallbacks()
        {
        }

        protected override void InitIDs()
        {
            this.id32_License = this.Reader.NameTable.Add("License");
            this.id17_AssemblyName = this.Reader.NameTable.Add("AssemblyName");
            this.id43_Edition = this.Reader.NameTable.Add("Edition");
            this.id38_VisualStudio = this.Reader.NameTable.Add("VisualStudio");
            this.id18_VsixVsPackagePath = this.Reader.NameTable.Add("VsixVsPackagePath");
            this.id30_Description = this.Reader.NameTable.Add("Description");
            this.id7_Reference = this.Reader.NameTable.Add("Reference");
            this.id6_References = this.Reader.NameTable.Add("References");
            this.id28_VsixPath = this.Reader.NameTable.Add("VsixPath");
            this.id4_Version = this.Reader.NameTable.Add("Version");
            this.id8_Content = this.Reader.NameTable.Add("Content");
            this.id40_Item = this.Reader.NameTable.Add("SupportedFrameworkRuntimeEdition");
            this.id35_PreviewImage = this.Reader.NameTable.Add("PreviewImage");
            this.id23_Id = this.Reader.NameTable.Add("Id");
            this.id37_SupportedProducts = this.Reader.NameTable.Add("SupportedProducts");
            this.id34_Icon = this.Reader.NameTable.Add("Icon");
            this.id27_MoreInfoUrl = this.Reader.NameTable.Add("MoreInfoUrl");
            this.id3_Item = this.Reader.NameTable.Add("");
            this.id39_IsolatedShell = this.Reader.NameTable.Add("IsolatedShell");
            this.id33_GettingStartedGuide = this.Reader.NameTable.Add("GettingStartedGuide");
            this.id15_ProjectTemplate = this.Reader.NameTable.Add("ProjectTemplate");
            this.id10_CustomExtension = this.Reader.NameTable.Add("CustomExtension");
            this.id36_InstalledByMsi = this.Reader.NameTable.Add("InstalledByMsi");
            this.id12_MefComponent = this.Reader.NameTable.Add("MefComponent");
            this.id21_Type = this.Reader.NameTable.Add("Type");
            this.id9_ToolboxControl = this.Reader.NameTable.Add("ToolboxControl");
            this.id16_VsixProjectTemplatePath = this.Reader.NameTable.Add("VsixProjectTemplatePath");
            this.id26_Name = this.Reader.NameTable.Add("Name");
            this.id22_VsixToolboxControlPath = this.Reader.NameTable.Add("VsixToolboxControlPath");
            this.id19_VsixMefComponentPath = this.Reader.NameTable.Add("VsixMefComponentPath");
            this.id1_Vsix = this.Reader.NameTable.Add("Vsix");
            this.id14_Assembly = this.Reader.NameTable.Add("Assembly");
            this.id31_Locale = this.Reader.NameTable.Add("Locale");
            this.id2_Item = this.Reader.NameTable.Add("http://schemas.microsoft.com/developer/vsx-schema/2010");
            this.id24_MinVersion = this.Reader.NameTable.Add("MinVersion");
            this.id29_Author = this.Reader.NameTable.Add("Author");
            this.id11_ItemTemplate = this.Reader.NameTable.Add("ItemTemplate");
            this.id5_Identifier = this.Reader.NameTable.Add("Identifier");
            this.id41_SystemComponent = this.Reader.NameTable.Add("SystemComponent");
            this.id20_VsixItemTemplatePath = this.Reader.NameTable.Add("VsixItemTemplatePath");
            this.id13_VsPackage = this.Reader.NameTable.Add("VsPackage");
            this.id42_AllUsers = this.Reader.NameTable.Add("AllUsers");
            this.id25_MaxVersion = this.Reader.NameTable.Add("MaxVersion");
        }
    }
}