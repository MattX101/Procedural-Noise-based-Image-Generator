using RuntimeNodeEditor.Node.Pointer;

namespace RNE.Template.Node.Pointer.Value
{
    public static partial class PointerValue
    {
        public static bool GetBool(OutputPointer Output)
        {
            return Output.GetComponent<BoolOutputPointer>().Value;
        }

        public static bool GetBool(InputPointer Input)
        {
            return 
                IsValid(Input) ? 
                GetBool(Input.ConnectedOutputPointer) : 
                false;
        }

        public static void GetBool(InputPointer Input, ref bool value)
        {
            value = 
                IsValid(Input) ? 
                GetBool(Input.ConnectedOutputPointer) : 
                value;
        }
    }
}
