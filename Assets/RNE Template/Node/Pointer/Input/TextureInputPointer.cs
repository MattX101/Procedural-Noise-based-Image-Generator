using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node.Pointer;

namespace RNE.Template.Node.Pointer
{
    public class TextureInputPointer : InputPointer
    {
        private void Awake()
        {
            ValueTypeIndex = (int)ValueType.Texture;
        }
    }
}
