using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node.Pointer;

namespace RNE.Template.Node.Pointer
{
    public class CellularProfileInputPointer : InputPointer
    {
        private void Awake()
        {
            ValueTypeIndex = (int)ValueType.CellularProfile;
        }
    }
}
