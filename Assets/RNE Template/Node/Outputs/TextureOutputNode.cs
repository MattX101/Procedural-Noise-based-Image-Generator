using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node;
using UnityEngine;
using UnityEngine.UI;

namespace RNE.Template.Node
{
    public class TextureOutputNode : EndNode
    {
        [SerializeField]
        private RawImage _image;

        protected override void CodeToExecute()
        {
            if (Inputs[0].ConnectedOutputPointer != null)
            {
                ExecuteInputConnection(0);
                _image.texture = PreviewTexture.Generate(PointerValue.GetColorArray(Inputs[0]));
            }
        }
    }
}
