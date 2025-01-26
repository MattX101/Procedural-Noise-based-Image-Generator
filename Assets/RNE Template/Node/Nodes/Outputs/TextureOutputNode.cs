using RNE.Template.Node.Pointer.Value;
using UnityEngine;

namespace RNE.Template.Node
{
    public class TextureOutputNode : NodeWithPreview
    {
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            ComputeBuffer buffer = PointerValue.GetTexture(Inputs[0]);

            if (buffer != null)
            {
                Color[] colors = new Color[ProjectData.Length];
                buffer.GetData(colors);

                if (ProjectData.Export && ProjectData.ExportPath != null)
                {
                    Texture2D texture = PreviewTexture.Generate(colors);
                    
                    SetPreview(texture);
                    ExportToImage.Export(texture, "Image_" + this.GetHashCode());
                }
                else
                {
                    SetPreview(PreviewTexture.Generate(colors));
                }
            }
        }
    }
}
