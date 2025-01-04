using RNE.Template.Node.Pointer.Value;

namespace RNE.Template.Node
{
    public class TextureOutputNode : NodeWithPreview
    {
        protected override void CodeToExecute()
        {
            if (Inputs[0].ConnectedOutputPointer != null)
            {
                ExecuteInputConnection(0);
                SetPreview(PreviewTexture.Generate(PointerValue.GetColorArray(Inputs[0])));
            }
        }
    }
}
