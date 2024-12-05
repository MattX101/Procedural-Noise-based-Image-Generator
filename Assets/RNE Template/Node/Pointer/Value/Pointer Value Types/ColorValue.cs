using RuntimeNodeEditor.Node.Pointer;
using UnityEngine;

namespace RNE.Template.Node.Pointer.Value
{
    public static partial class PointerValue
    {
        public static Color GetColor(OutputPointer Output)
        {
            Color color = Output.GetComponent<ColorOutputPointer>().Value;
            color.r /= 255;
            color.g /= 255;
            color.b /= 255;

            return color;
        }

        public static Color GetColor(InputPointer Input)
        {
            return 
                IsValid(Input) ? 
                GetColor(Input.ConnectedOutputPointer) : 
                Color.white;
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
