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
                if (ProjectData.Export && ProjectData.ExportPath != null)
                {
                    Color[] colors = new Color[ProjectData.Length];
                    buffer.GetData(colors);

                    ExportToImage.Export(PreviewTexture.Generate(colors), "Image_" + this.GetHashCode());
                }

                SetPreview(buffer);
            }
        }
    } 
}
