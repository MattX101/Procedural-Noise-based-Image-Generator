using RuntimeNodeEditor.Node.Pointer;

namespace RNE.Template.Node.Pointer.Value
{
    public static partial class PointerValue
    {
        public static char GetChar(OutputPointer Output)
        {
            return Output.GetComponent<CharOutputPointer>().Value;
        }

        public static char GetChar(InputPointer Input)
        {
            return 
                IsValid(Input) ? 
                GetChar(Input.ConnectedOutputPointer) : 
                ' ';
        }

        public static void GetChar(InputPointer Input, ref char value)
        {
            value = 
                IsValid(Input) ? 
                GetChar(Input.ConnectedOutputPointer) : 
                value;
        }
    }
}
