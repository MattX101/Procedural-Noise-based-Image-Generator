using RuntimeNodeEditor.Node.Pointer;
using UnityEngine;

namespace RNE.Template.Node.Pointer.Value
{
    public static partial class PointerValue
    {
        public static Vector2 GetVector2(OutputPointer Output)
        {
            return Output.ValueTypeIndex switch
            {
                (int)ValueType.Vector2 => Output.GetComponent<Vector2OutputPointer>().Value,
                (int)ValueType.Int => new Vector2(Output.GetComponent<IntOutputPointer>().Value, Output.GetComponent<IntOutputPointer>().Value),
                (int)ValueType.Float => new Vector2(Output.GetComponent<FloatOutputPointer>().Value, Output.GetComponent<FloatOutputPointer>().Value),
                (int)ValueType.Vector3 => new Vector2(Output.GetComponent<Vector3OutputPointer>().Value.x, Output.GetComponent<Vector3OutputPointer>().Value.y),
                _ => Vector2.zero
            };
        }

        public static Vector2 GetVector2(InputPointer Input)
        {
            return 
                IsValid(Input) ? 
                GetVector2(Input.ConnectedOutputPointer) : 
                Vector2.zero;
        }
        
        public static void GetVector2(InputPointer Input, ref Vector2 value)
        {
            value = 
                IsValid(Input) ? 
                GetVector2(Input.ConnectedOutputPointer) : 
                value;
        }
    }
}
