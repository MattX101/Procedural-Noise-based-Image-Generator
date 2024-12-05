using RuntimeNodeEditor.Node.Pointer;

namespace RNE.Template.Node.Pointer.Value
{
    public static partial class PointerValue
    {
        public static string GetString(OutputPointer Output)
        {
            return Output.ValueTypeIndex switch
            {
                (int)ValueType.String => Output.GetComponent<StringOutputPointer>().Value,
                (int)ValueType.Int => Output.GetComponent<IntOutputPointer>().Value.ToString(),
                (int)ValueType.Float => Output.GetComponent<FloatOutputPointer>().Value.ToString(),
                (int)ValueType.Bool => Output.GetComponent<BoolOutputPointer>().Value.ToString(),
                (int)ValueType.Char => Output.GetComponent<CharOutputPointer>().Value.ToString(),
                _ => ""
            };
        }
        public static string GetString(InputPointer Input)
        {
            return 
                IsValid(Input) ? 
                GetString(Input.ConnectedOutputPointer) : 
                "";
        }

        public static void GetString(InputPointer Input, ref string value)
        {
            value = 
                IsValid(Input) ? 
                GetString(Input.ConnectedOutputPointer) : 
                value;
        }
    }
}
