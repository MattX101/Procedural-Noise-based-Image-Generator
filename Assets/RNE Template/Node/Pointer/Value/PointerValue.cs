using RuntimeNodeEditor.Node.Pointer;

namespace RNE.Template.Node.Pointer.Value
{
    public static partial class PointerValue
    {
        private static bool IsValid(InputPointer Input)
        {
            return Input.Node.IsValid(Input);
        }
    }
}