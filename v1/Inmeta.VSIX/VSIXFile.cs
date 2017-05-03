using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.IO.Packaging;
using System.Threading;

namespace VSIXParser
{
    public class VsixFile : IDisposable, INotifyPropertyChanged
    {
        private ZipPackage _package;
        private string _fileName;
        private bool _isWritable;
        private SortedObservableCollection<VsixFileNode> _nodes;
        private PropertyChangedEventHandler _PropertyChanged;

        public bool IsWritable
        {
            get
            {
                return this._isWritable;
            }
            private set
            {
                this._isWritable = value;
                this.OnPropertyChanged("IsWritable");
            }
        }

        public SortedObservableCollection<VsixFileNode> Nodes
        {
            get
            {
                if (this._nodes == null)
                    this._nodes = new SortedObservableCollection<VsixFileNode>();
                return this._nodes;
            }
            set
            {
                this._nodes = new SortedObservableCollection<VsixFileNode>((IEnumerable<VsixFileNode>)value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                PropertyChangedEventHandler changedEventHandler = this._PropertyChanged;
                PropertyChangedEventHandler comparand;
                do
                {
                    comparand = changedEventHandler;
                    changedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this._PropertyChanged, comparand + value, comparand);
                }
                while (changedEventHandler != comparand);
            }
            remove
            {
                PropertyChangedEventHandler changedEventHandler = this._PropertyChanged;
                PropertyChangedEventHandler comparand;
                do
                {
                    comparand = changedEventHandler;
                    changedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this._PropertyChanged, comparand - value, comparand);
                }
                while (changedEventHandler != comparand);
            }
        }

        private VsixFile(PackagePart parent, string fileName, FileAccess fileAccess)
        {
            this._package = Package.Open(parent.GetStream(FileMode.Open, fileAccess), FileMode.Open, fileAccess) as ZipPackage;
            this._fileName = fileName;
            this.IsWritable = fileAccess != FileAccess.Read;
            this.Init(parent);
        }

        public VsixFile(string filePath, FileAccess fileAccess)
        {
            this._package = Package.Open(filePath, FileMode.Open, fileAccess) as ZipPackage;
            this._fileName = Path.GetFileName(filePath);
            this.IsWritable = fileAccess != FileAccess.Read;
            this.Init((PackagePart)null);
        }

        ~VsixFile()
        {
            this.Dispose(false);
        }

        private void Init(PackagePart parentPart)
        {
            SortedList<string, PackagePart> sortedList = new SortedList<string, PackagePart>();
            foreach (PackagePart packagePart in this._package.GetParts())
                sortedList.Add(packagePart.Uri.OriginalString.TrimStart(new char[1]
        {
          '/'
        }), packagePart);
            VsixPackageNode vsixPackageNode = new VsixPackageNode(this._fileName, (VsixFileNode)null, this._package, parentPart);
            vsixPackageNode.Nodes = new SortedObservableCollection<VsixFileNode>((IEnumerable<VsixFileNode>)this.CreateFileTree((IEnumerable<KeyValuePair<string, PackagePart>>)sortedList, (VsixFileNode)vsixPackageNode));
            this.Nodes.Add((VsixFileNode)vsixPackageNode);
        }

        private IList<VsixFileNode> CreateFileTree(IEnumerable<KeyValuePair<string, PackagePart>> parts, VsixFileNode parent)
        {
            List<VsixFileNode> list = new List<VsixFileNode>();
            while (Enumerable.Count<KeyValuePair<string, PackagePart>>(parts) > 0)
            {
                string[] strArray = Enumerable.First<KeyValuePair<string, PackagePart>>(parts).Key.Split(new char[1]
        {
          '/'
        }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length > 1)
                {
                    string topNodeDirectory = strArray[0] + "/";
                    IEnumerable<KeyValuePair<string, PackagePart>> source = Enumerable.Where<KeyValuePair<string, PackagePart>>(parts, (Func<KeyValuePair<string, PackagePart>, bool>)(x => x.Key.StartsWith(topNodeDirectory)));
                    SortedList<string, PackagePart> sortedList = new SortedList<string, PackagePart>();
                    foreach (KeyValuePair<string, PackagePart> keyValuePair in source)
                        sortedList.Add(keyValuePair.Key.Substring(topNodeDirectory.Length), keyValuePair.Value);
                    VsixFolderNode vsixFolderNode = new VsixFolderNode(strArray[0], parent);
                    vsixFolderNode.Nodes = new SortedObservableCollection<VsixFileNode>((IEnumerable<VsixFileNode>)this.CreateFileTree((IEnumerable<KeyValuePair<string, PackagePart>>)sortedList, (VsixFileNode)vsixFolderNode));
                    list.Add((VsixFileNode)vsixFolderNode);
                    parts = Enumerable.Skip<KeyValuePair<string, PackagePart>>(parts, Enumerable.Count<KeyValuePair<string, PackagePart>>(source));
                }
                else
                {
                    PackagePart part = Enumerable.First<KeyValuePair<string, PackagePart>>(parts).Value;
                    list.Add(this.CreateFileNodeFromPart(part, parent));
                    parts = Enumerable.Skip<KeyValuePair<string, PackagePart>>(parts, 1);
                }
            }
            return (IList<VsixFileNode>)list;
        }

        public void AddFile(VsixFileNode targetNode, string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                this.AddFile(targetNode, Path.GetFileName(filePath), (Stream)fileStream);
        }

        public void AddFile(VsixFileNode targetNode, string fileName, Stream fileStream)
        {
            string str = "/";
            VsixFileNode vsixFileNode;
            for (vsixFileNode = targetNode; vsixFileNode != null && !(vsixFileNode is VsixPackageNode); vsixFileNode = vsixFileNode.Parent)
                str = "/" + vsixFileNode.Name + str;
            if (vsixFileNode == null)
                return;
            VsixPackageNode vsixPackageNode = vsixFileNode as VsixPackageNode;
            Uri partUri = PackUriHelper.CreatePartUri(new Uri(str + fileName, UriKind.Relative));
            if (vsixPackageNode.Package.PartExists(partUri))
                Enumerable.FirstOrDefault<VsixFileNode>((IEnumerable<VsixFileNode>)targetNode.Nodes, (Func<VsixFileNode, bool>)(node =>
                {
                    if (node is VsixPackagePartNode)
                        return (node as VsixPackagePartNode).Part.Uri == partUri;
                    else
                        return false;
                })).Remove(true);
            PackagePart part = vsixPackageNode.Package.CreatePart(partUri, "application/octet-stream", CompressionOption.Maximum);
            Stream stream = part.GetStream(FileMode.OpenOrCreate, FileAccess.Write);
            byte[] buffer = new byte[4096];
            fileStream.Seek(0L, SeekOrigin.Begin);
            int count;
            while ((count = fileStream.Read(buffer, 0, 4096)) > 0)
                stream.Write(buffer, 0, count);
            stream.Close();
            fileStream.Seek(0L, SeekOrigin.Begin);
            targetNode.Nodes.Add(this.CreateFileNodeFromPart(part, targetNode));
        }

        public void AddDirectory(VsixFileNode targetNode, string directoryPath)
        {
            VsixFolderNode vsixFolderNode = new VsixFolderNode(Path.GetFileName(directoryPath), targetNode);
            targetNode.Nodes.Add((VsixFileNode)vsixFolderNode);
            foreach (string directoryPath1 in Directory.GetDirectories(directoryPath))
                this.AddDirectory((VsixFileNode)vsixFolderNode, directoryPath1);
            foreach (string filePath in Directory.GetFiles(directoryPath))
                this.AddFile((VsixFileNode)vsixFolderNode, filePath);
        }

        private VsixFileNode CreateFileNodeFromPart(PackagePart part, VsixFileNode parentNode)
        {
            string str = part.Uri.OriginalString.Substring(part.Uri.OriginalString.LastIndexOf('/') + 1);
            VsixFileNode vsixFileNode;
            if (str.EndsWith(".vsix"))
            {
                VsixFile vsixFile = (VsixFile)null;
                try
                {
                    vsixFile = new VsixFile(part, str, this.IsWritable ? FileAccess.ReadWrite : FileAccess.Read);
                }
                catch (Exception)
                {
                }
                if (vsixFile != null)
                {
                    vsixFileNode = Enumerable.First<VsixFileNode>((IEnumerable<VsixFileNode>)vsixFile.Nodes);
                    vsixFileNode.Parent = parentNode;
                }
                else
                    vsixFileNode = (VsixFileNode)new VsixPackagePartNode(str, parentNode, part);
            }
            else
                vsixFileNode = string.Compare(str, "extension.vsixmanifest", true) != 0 ? (string.Compare(str, "extension.vsixlangpack", true) != 0 ? (VsixFileNode)new VsixPackagePartNode(str, parentNode, part) : (VsixFileNode)new VsixLangPackManifestNode(str, parentNode, part)) : (VsixFileNode)new VsixManifestNode(str, parentNode, part);
            return vsixFileNode;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing || this._package == null)
                return;
            Enumerable.First<VsixFileNode>((IEnumerable<VsixFileNode>)this.Nodes).Dispose();
            this._package.Close();
            this._package = (ZipPackage)null;
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this._PropertyChanged == null)
                return;
            this._PropertyChanged((object)this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SortedObservableCollection<TValue> : IEnumerable<TValue>, IEnumerable, INotifyCollectionChanged
    {
        private SortedList<TValue, TValue> m_sortedList;
        private NotifyCollectionChangedEventHandler _CollectionChanged;

        public int Count
        {
            get
            {
                return this.m_sortedList.Count;
            }
        }

        public TValue this[int index]
        {
            get
            {
                return Enumerable.ElementAt<KeyValuePair<TValue, TValue>>((IEnumerable<KeyValuePair<TValue, TValue>>)this.m_sortedList, index).Value;
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                NotifyCollectionChangedEventHandler changedEventHandler = this._CollectionChanged;
                NotifyCollectionChangedEventHandler comparand;
                do
                {
                    comparand = changedEventHandler;
                    changedEventHandler = Interlocked.CompareExchange<NotifyCollectionChangedEventHandler>(ref this._CollectionChanged, comparand + value, comparand);
                }
                while (changedEventHandler != comparand);
            }
            remove
            {
                NotifyCollectionChangedEventHandler changedEventHandler = this._CollectionChanged;
                NotifyCollectionChangedEventHandler comparand;
                do
                {
                    comparand = changedEventHandler;
                    changedEventHandler = Interlocked.CompareExchange<NotifyCollectionChangedEventHandler>(ref this._CollectionChanged, comparand - value, comparand);
                }
                while (changedEventHandler != comparand);
            }
        }

        public SortedObservableCollection()
        {
            this.m_sortedList = new SortedList<TValue, TValue>();
        }

        public SortedObservableCollection(IComparer<TValue> comparer)
        {
            this.m_sortedList = new SortedList<TValue, TValue>(comparer);
        }

        public SortedObservableCollection(IEnumerable<TValue> list)
        {
            this.m_sortedList = new SortedList<TValue, TValue>();
            foreach (TValue obj in list)
                this.Add(obj);
        }

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this._CollectionChanged == null)
                return;
            this._CollectionChanged((object)this, e);
        }

        public void Add(TValue value)
        {
            this.m_sortedList.Add(value, value);
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (object)value, this.m_sortedList.IndexOfKey(value)));
        }

        public void Clear()
        {
            this.m_sortedList.Clear();
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
        {
            return this.m_sortedList.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.m_sortedList.Values.GetEnumerator();
        }

        public bool Remove(TValue value)
        {
            TValue obj;
            this.m_sortedList.TryGetValue(value, out obj);
            int index = this.m_sortedList.IndexOfKey(value);
            bool flag = this.m_sortedList.Remove(value);
            if (flag)
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, (object)obj, index));
            return flag;
        }
    }

    public abstract class VsixFileNode : IDisposable, IComparable<VsixFileNode>, INotifyPropertyChanged
    {
        private string _previousName;
        private SortedObservableCollection<VsixFileNode> _nodes;
        private string _name;
        private bool _isSelected;
        private bool _isExpanded;
        private bool _isRenaming;
        private long _size;
        private PropertyChangedEventHandler _PropertyChanged;

        public SortedObservableCollection<VsixFileNode> Nodes
        {
            get
            {
                if (this._nodes == null)
                    this._nodes = new SortedObservableCollection<VsixFileNode>();
                return this._nodes;
            }
            set
            {
                this._nodes = value;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
                this.OnPropertyChanged("Name");
            }
        }

        public VsixFileNode Parent { get; set; }

        public bool IsSelected
        {
            get
            {
                return this._isSelected;
            }
            set
            {
                this._isSelected = value;
                this.OnPropertyChanged("IsSelected");
            }
        }

        public bool IsExpanded
        {
            get
            {
                return this._isExpanded;
            }
            set
            {
                this._isExpanded = value;
                this.OnPropertyChanged("IsExpanded");
            }
        }

        public bool IsRenaming
        {
            get
            {
                return this._isRenaming;
            }
            set
            {
                this._isRenaming = value;
                this.OnPropertyChanged("IsRenaming");
                if (!(this._previousName != this.Name))
                    return;
                this.Rename();
                this._previousName = this.Name;
            }
        }

        public virtual long Size
        {
            get
            {
                return this._size;
            }
            set
            {
                this._size = value;
                this.OnPropertyChanged("Size");
            }
        }

        public abstract string ContentType { get; }

        protected abstract int SortPriority { get; }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                PropertyChangedEventHandler changedEventHandler = this._PropertyChanged;
                PropertyChangedEventHandler comparand;
                do
                {
                    comparand = changedEventHandler;
                    changedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this._PropertyChanged, comparand + value, comparand);
                }
                while (changedEventHandler != comparand);
            }
            remove
            {
                PropertyChangedEventHandler changedEventHandler = this._PropertyChanged;
                PropertyChangedEventHandler comparand;
                do
                {
                    comparand = changedEventHandler;
                    changedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this._PropertyChanged, comparand - value, comparand);
                }
                while (changedEventHandler != comparand);
            }
        }

        public VsixFileNode(string name, VsixFileNode parent, IList<VsixFileNode> nodes)
            : this(name, parent)
        {
            this.Nodes = new SortedObservableCollection<VsixFileNode>((IEnumerable<VsixFileNode>)nodes);
        }

        public VsixFileNode(string name, VsixFileNode parent)
        {
            this.Name = Uri.UnescapeDataString(name);
            this._previousName = this.Name;
            this.Parent = parent;
        }

        ~VsixFileNode()
        {
            this.Dispose(false);
        }

        public void CancelRename()
        {
            this.Name = this._previousName;
            this.IsRenaming = false;
        }

        public abstract void Move(VsixFileNode targetNode);

        public abstract void Remove(bool removeSelf);

        public abstract void Rename();

        public abstract void RebindParentPackage();

        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing || this.Nodes == null)
                return;
            foreach (VsixFileNode vsixFileNode in (IEnumerable<VsixFileNode>)this.Nodes)
                vsixFileNode.Dispose();
        }

        public int CompareTo(VsixFileNode other)
        {
            if (this.SortPriority == other.SortPriority)
                return string.Compare(this.Name, other.Name);
            else
                return this.SortPriority - other.SortPriority;
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this._PropertyChanged == null)
                return;
            this._PropertyChanged((object)this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class VsixPackageNode : VsixPackagePartNode
    {
        public ZipPackage Package { get; set; }

        public override string ContentType
        {
            get
            {
                return "VSIX File";
            }
        }

        public VsixPackageNode(string name, VsixFileNode parent, ZipPackage package, PackagePart parentPackagePart)
            : base(name, parent, parentPackagePart)
        {
            this.Package = package;
        }

        ~VsixPackageNode()
        {
            this.Dispose(false);
        }

        public override void Rename()
        {
            base.Rename();
            this.Package = System.IO.Packaging.Package.Open(this.Part.GetStream(FileMode.Open, FileAccess.ReadWrite), FileMode.Open, FileAccess.ReadWrite) as ZipPackage;
            foreach (VsixFileNode vsixFileNode in (IEnumerable<VsixFileNode>)this.Nodes)
                vsixFileNode.RebindParentPackage();
        }

        public override void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        private void Dispose(bool disposing)
        {
            base.Dispose();
            if (!disposing || this.Package == null)
                return;
            this.Package.Close();
            this.Package = (ZipPackage)null;
        }
    }

    public class VsixPackagePartNode : VsixFileNode
    {
        private PackagePart _part;

        public PackagePart Part
        {
            get
            {
                return this._part;
            }
            set
            {
                this._part = value;
                if (this._part == null)
                    return;
                this.Size = this._part.GetStream(FileMode.Open, FileAccess.Read).Length;
            }
        }

        public override string ContentType
        {
            get
            {
                return this.Part.ContentType;
            }
        }

        public string CompressionOption
        {
            get
            {
                return ((object)this.Part.CompressionOption).ToString();
            }
        }

        protected override int SortPriority
        {
            get
            {
                return int.MaxValue;
            }
        }

        public VsixPackagePartNode(string name, VsixFileNode parent, PackagePart packagePart)
            : base(name, parent)
        {
            this.Part = packagePart;
        }

        public override void Remove(bool removeSelf)
        {
            this.Part.Package.DeletePart(this.Part.Uri);
            if (!removeSelf)
                return;
            this.Parent.Nodes.Remove((VsixFileNode)this);
        }

        public override void Move(VsixFileNode targetNode)
        {
            if (this.Parent != targetNode)
            {
                this.Parent.Nodes.Remove((VsixFileNode)this);
                this.Parent = targetNode;
                this.Parent.Nodes.Add((VsixFileNode)this);
            }
            string uriString = this.Part.Uri.OriginalString.Substring(this.Part.Uri.OriginalString.LastIndexOf('/'));
            VsixFileNode vsixFileNode;
            for (vsixFileNode = targetNode; vsixFileNode != null && !(vsixFileNode is VsixPackageNode); vsixFileNode = vsixFileNode.Parent)
                uriString = "/" + vsixFileNode.Name + uriString;
            if (vsixFileNode == null || !(uriString != this.Part.Uri.OriginalString))
                return;
            PackagePart part = (vsixFileNode as VsixPackageNode).Package.CreatePart(new Uri(uriString, UriKind.Relative), this.Part.ContentType, System.IO.Packaging.CompressionOption.Maximum);
            Stream stream1 = part.GetStream(FileMode.OpenOrCreate, FileAccess.Write);
            Stream stream2 = this.Part.GetStream(FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int count;
            while ((count = stream2.Read(buffer, 0, 4096)) > 0)
                stream1.Write(buffer, 0, count);
            stream1.Close();
            this.Part.Package.DeletePart(this.Part.Uri);
            this.Part = part;
        }

        public override void Rename()
        {
            string str = "/";
            for (VsixFileNode parent = this.Parent; parent != null && !(parent is VsixPackageNode); parent = parent.Parent)
                str = "/" + parent.Name + str;
            Uri partUri = PackUriHelper.CreatePartUri(new Uri(str + this.Name, UriKind.RelativeOrAbsolute));
            if (this.Part.Package.PartExists(partUri))
                return;
            PackagePart part = this.Part.Package.CreatePart(partUri, this.Part.ContentType);
            Utilities.CopyStream(this.Part.GetStream(), part.GetStream(FileMode.Open, FileAccess.Write));
            this.Part.Package.DeletePart(this.Part.Uri);
            this.Part = part;
        }

        public override void RebindParentPackage()
        {
            VsixFileNode parent = this.Parent;
            while (parent != null && !(parent is VsixPackageNode))
                parent = parent.Parent;
            this.Part = (parent as VsixPackageNode).Package.GetPart(this.Part.Uri);
        }
    }

    internal class Utilities
    {
        public static string VsixFileNodeDataFormat = "VsixFileNode";

        static Utilities()
        {
        }

        public static void CopyStream(Stream source, Stream target)
        {
            source.Seek(0L, SeekOrigin.Begin);
            target.Seek(0L, SeekOrigin.Begin);
            byte[] buffer = new byte[4096];
            int count;
            while ((count = source.Read(buffer, 0, buffer.Length)) > 0)
                target.Write(buffer, 0, count);
        }
    }

    public class VsixFolderNode : VsixFileNode
    {
        public override long Size
        {
            get
            {
                long num = 0L;
                foreach (VsixFileNode vsixFileNode in (IEnumerable<VsixFileNode>)this.Nodes)
                    num += vsixFileNode.Size;
                return num;
            }
        }

        public override string ContentType
        {
            get
            {
                return "File folder";
            }
        }

        protected override int SortPriority
        {
            get
            {
                return 1;
            }
        }

        public VsixFolderNode(string name, VsixFileNode parent)
            : base(name, parent)
        {
        }

        public VsixFolderNode(string name, VsixFileNode parent, IList<VsixFileNode> nodes)
            : base(name, parent, nodes)
        {
        }

        public override void Remove(bool removeSelf)
        {
            foreach (VsixFileNode vsixFileNode in (IEnumerable<VsixFileNode>)this.Nodes)
                vsixFileNode.Remove(false);
            if (!removeSelf)
                return;
            this.Parent.Nodes.Remove((VsixFileNode)this);
        }

        public override void Move(VsixFileNode targetNode)
        {
            this.Parent.Nodes.Remove((VsixFileNode)this);
            this.Parent = targetNode;
            this.Parent.Nodes.Add((VsixFileNode)this);
            foreach (VsixFileNode vsixFileNode in (IEnumerable<VsixFileNode>)this.Nodes)
                vsixFileNode.Move((VsixFileNode)this);
        }

        public override void Rename()
        {
            foreach (VsixFileNode vsixFileNode in (IEnumerable<VsixFileNode>)this.Nodes)
                vsixFileNode.Rename();
        }

        public override void RebindParentPackage()
        {
            foreach (VsixFileNode vsixFileNode in (IEnumerable<VsixFileNode>)this.Nodes)
                vsixFileNode.RebindParentPackage();
        }
    }

    public class VsixLangPackManifestNode : VsixPackagePartNode
    {
        protected override int SortPriority
        {
            get
            {
                return 0;
            }
        }

        public VsixLangPackManifestNode(string name, VsixFileNode parent, PackagePart packagePart)
            : base(name, parent, packagePart)
        {
        }
    }

    public class VsixManifestNode : VsixPackagePartNode
    {
        protected override int SortPriority
        {
            get
            {
                return 0;
            }
        }

        public VsixManifestNode(string name, VsixFileNode parent, PackagePart packagePart)
            : base(name, parent, packagePart)
        {
        }
    }
}
