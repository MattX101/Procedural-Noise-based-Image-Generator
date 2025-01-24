using RuntimeNodeEditor.Node.Pointer;
using UnityEngine;

namespace RNE.Template.Node.Pointer.Value
{
    public static partial class PointerValue
    {
        public static ComputeBuffer GetTexture(OutputPointer Output)
        {
            return Output.GetComponent<TextureOutputPointer>().Buffer;
        }

        public static ComputeBuffer GetTexture(InputPointer Input)
        {
            return 
                IsValid(Input) ?
                GetTexture(Input.ConnectedOutputPointer) : 
                null;
        }
        
        public static void GetTexture(InputPointer Input, ref ComputeBuffer values)
        {
            values = 
                IsValid(Input) ?
                GetTexture(Input.ConnectedOutputPointer) : 
                values;
        }
    }
}
