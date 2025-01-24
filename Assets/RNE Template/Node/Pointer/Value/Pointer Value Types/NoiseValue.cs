using RuntimeNodeEditor.Node.Pointer;
using UnityEngine;

namespace RNE.Template.Node.Pointer.Value
{
    public static partial class PointerValue
    {
        public static ComputeBuffer GetNoise(OutputPointer Output)
        {
            return Output.GetComponent<NoiseOutputPointer>().Buffer;
        }

        public static ComputeBuffer GetNoise(InputPointer Input)
        {
            return 
                IsValid(Input) ?
                GetNoise(Input.ConnectedOutputPointer) : 
                null;
        }
        
        public static void GetNoise(InputPointer Input, ref ComputeBuffer values)
        {
            values = 
                IsValid(Input) ?
                GetNoise(Input.ConnectedOutputPointer) : 
                values;
        }
    }
}
