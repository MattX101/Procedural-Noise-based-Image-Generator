using UnityEngine;
using System.IO;

namespace RNE.Template.Node
{
    public static class ExportToImage
    {
        public static void Export(Texture2D texture, string name)
        {
            File.WriteAllBytes(ProjectData.ExportPath + "/" + name + ".png", texture.EncodeToPNG());
        }
    }
}
