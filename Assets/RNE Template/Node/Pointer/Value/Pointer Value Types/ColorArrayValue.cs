using RuntimeNodeEditor.Node.Pointer;
using UnityEngine;

namespace RNE.Template.Node.Pointer.Value
{
    public static partial class PointerValue
    {
        public static Color[] GetColorArray(OutputPointer Output)
        {
            return Output.GetComponent<ColorArrayOutputPointer>().Values;
        }

        public static Color[] GetColorArray(InputPointer Input)
        {
            return 
                IsValid(Input) ?
                GetColorArray(Input.ConnectedOutputPointer) : 
                null;
        }
        
        public static void GetColorArray(InputPointer Input, ref Color[] values)
        {
            values = 
                IsValid(Input) ?
                GetColorArray(Input.ConnectedOutputPointer) : 
                values;
        }
    }
}
