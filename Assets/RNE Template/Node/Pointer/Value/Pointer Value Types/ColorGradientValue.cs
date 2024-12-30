using RuntimeNodeEditor.Node.Pointer;
using UnityEngine;

namespace RNE.Template.Node.Pointer.Value
{
    public static partial class PointerValue
    {
        public static Gradient GetColorGradient(OutputPointer Output)
        {
            return Output.GetComponent<ColorGradientOutputPointer>().Value;
        }

        public static Gradient GetColorGradient(InputPointer Input)
        {
            return
                IsValid(Input) ?
                GetColorGradient(Input.ConnectedOutputPointer) :
                null;
        }

        public static void GetColorGradient(InputPointer Input, ref Gradient value)
        {
            value =
                IsValid(Input) ?
                GetColorGradient(Input.ConnectedOutputPointer) :
                value;
        }
    }
}
