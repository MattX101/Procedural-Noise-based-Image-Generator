using RuntimeNodeEditor.Node.Pointer;
using Utils.Noise.Profiles;

namespace RNE.Template.Node.Pointer.Value
{
    public static partial class PointerValue
    {
        public static WarpProfile GetWarpProfile(OutputPointer Output)
        {
            return Output.GetComponent<WarpProfileOutputPointer>().WarpProfile;
        }

        public static WarpProfile GetWarpProfile(InputPointer Input)
        {
            return 
                IsValid(Input) ?
                GetWarpProfile(Input.ConnectedOutputPointer) : 
                null;
        }

        public static void GetWarpProfile(InputPointer Input, ref WarpProfile value)
        {
            value = 
                IsValid(Input) ?
                GetWarpProfile(Input.ConnectedOutputPointer) : 
                value;
        }
    }
}
