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
                Color[] colors = new Color[PreviewTexture.Length];
                buffer.GetData(colors);
                SetPreview(PreviewTexture.Generate(colors));
            }
        }
    }
}
