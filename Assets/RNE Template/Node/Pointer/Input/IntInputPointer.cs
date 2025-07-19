using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node.Pointer;

namespace RNE.Template.Node.Pointer
{
    public class IntInputPointer : InputPointer
    {
        private void Awake()
        {
            ValueTypeIndex = (int)ValueType.Int;
        }
    }
}