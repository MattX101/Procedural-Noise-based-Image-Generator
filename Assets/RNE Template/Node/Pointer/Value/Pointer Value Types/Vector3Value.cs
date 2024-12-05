using RuntimeNodeEditor.Node.Pointer;
using UnityEngine;

namespace RNE.Template.Node.Pointer.Value
{
    public static partial class PointerValue
    {
        public static Vector3 GetVector3(OutputPointer Output)
        {
            return Output.ValueTypeIndex switch
            {
                (int)ValueType.Vector3 => Output.GetComponent<Vector3OutputPointer>().Value,
                (int)ValueType.Int => new Vector3(Output.GetComponent<IntOutputPointer>().Value, Output.GetComponent<IntOutputPointer>().Value, Output.GetComponent<IntOutputPointer>().Value),
                (int)ValueType.Float => new Vector3(Output.GetComponent<FloatOutputPointer>().Value, Output.GetComponent<FloatOutputPointer>().Value, Output.GetComponent<FloatOutputPointer>().Value),
                (int)ValueType.Vector2 => new Vector3(Output.GetComponent<Vector2OutputPointer>().Value.x, Output.GetComponent<Vector2OutputPointer>().Value.y, 0),
                _ => Vector3.zero
            };
        }
        public static Vector3 GetVector3(InputPointer Input)
        {
            return 
                IsValid(Input) ? 
                GetVector3(Input.ConnectedOutputPointer) : 
                Vector3.zero;
        }

        public static void GetVector3(InputPointer Input, ref Vector3 value)
        {
            value = 
                IsValid(Input) ? 
                GetVector3(Input.ConnectedOutputPointer) : 
                value;
        }
    }
}
