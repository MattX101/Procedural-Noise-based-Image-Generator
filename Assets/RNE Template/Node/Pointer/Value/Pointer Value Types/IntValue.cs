using RuntimeNodeEditor.Node.Pointer;

namespace RNE.Template.Node.Pointer.Value
{
    public static partial class PointerValue
    {
        public static int GetInt(OutputPointer Output)
        {
            return Output.ValueTypeIndex switch
            {
                (int)ValueType.Int => Output.GetComponent<IntOutputPointer>().Value,
                (int)ValueType.Float => (int)Output.GetComponent<FloatOutputPointer>().Value,
                _ => 0
            };
        }

        public static int GetInt(InputPointer Input)
        {
            return 
                IsValid(Input) ? 
                GetInt(Input.ConnectedOutputPointer) : 
                0;
        }

        public static void GetInt(InputPointer Input, ref int value)
        {
            value = 
                IsValid(Input) ? 
                GetInt(Input.ConnectedOutputPointer) : 
                value;
        }
    }
}
