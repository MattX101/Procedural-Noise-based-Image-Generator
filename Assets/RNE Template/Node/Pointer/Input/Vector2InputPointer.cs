using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node.Pointer;

namespace RNE.Template.Node.Pointer
{
    public class Vector2InputPointer : InputPointer
    {
        private void Awake()
        {
            ValueTypeIndex = (int)ValueType.Vector2;
        }
    }
}