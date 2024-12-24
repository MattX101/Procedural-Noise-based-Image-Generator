using RuntimeNodeEditor.Node.Pointer;
using UnityEngine;

namespace RNE.Template.Node.Pointer.Value
{
    public static partial class PointerValue
    {
        public static Color GetColor(OutputPointer Output)
        {
            return Output.GetComponent<ColorOutputPointer>().Value;
        }

        public static Color GetColor(InputPointer Input)
        {
            return 
                IsValid(Input) ? 
                GetColor(Input.ConnectedOutputPointer) : 
                Color.black;
        }
        
        public static void GetColor(InputPointer Input, ref Color value)
        {
            value = 
                IsValid(Input) ? 
                GetColor(Input.ConnectedOutputPointer) : 
                value;
        }
    }
}
