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

                Texture2D texture = new Texture2D(128, 128);
                texture.wrapMode = TextureWrapMode.Clamp;
                texture.filterMode = FilterMode.Point;
                texture.SetPixels(PointerValue.GetColorArray(Inputs[0]));
                texture.Apply();

                _image.texture = texture;
            }
        }
    }
}
