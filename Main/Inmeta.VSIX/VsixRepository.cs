using System;
using System.IO;
using System.Linq;
using VSIX.v20;
using VSIXParser;

namespace Inmeta.VSIX
{
    public class VsixRepository
    {
        public static VsixItem Read(byte[] vsixContent, string name)
        {
            using (var contentStream = new MemoryStream(vsixContent))
            {
                using (var file = new VsixFile(contentStream, name, FileAccess.Read))
                {
                    var root = file.Nodes[0];

                    var manifestNode = root.Nodes.FirstOrDefault(n => n.Name.EndsWith(".vsixmanifest"));
                    var packageNode = manifestNode as VsixPackagePartNode;
                    try
                    {
                        return ReadVsixV2(name, packageNode, vsixContent);
                    }
                    catch (Exception)
                    {
                        return ReadVsixV1(packageNode, vsixContent);
                    }
                }
            }
        }

        private static VsixItem ReadVsixV1(VsixPackagePartNode packageNode, byte[] content)
        {
            using (var stream = packageNode.Part.GetStream(FileMode.Open))
            {
                using (var sr = new StreamReader(stream))
                {
                    var vsixManifest = PackageManifest.Deserialize(sr.ReadToEnd());
                    return new VsixItem(vsixManifest, content);
                }
            }
        }

        private static VsixItem ReadVsixV2(string name, VsixPackagePartNode packageNode, byte[] content)
        {
            using (var stream = packageNode.Part.GetStream(FileMode.Open))
            {
                using (var sr = new StreamReader(stream))
                {
                    var vsixSerializer = new VsixSerializer();
                    var vsixManifest = vsixSerializer.Deserialize(sr) as Vsix;
                    if (vsixManifest == null)
                        throw new Exception("Could not deserialize VSIX manifest from " + name);

                    return new VsixItem(vsixManifest, content);
                }
            }
        }
    }
}
