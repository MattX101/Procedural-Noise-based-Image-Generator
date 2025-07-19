using RuntimeNodeEditor.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.Noise.Profiles;
using UnityEngine;

namespace RNE.Template.Node.Pointer
{
    public class WarpProfileOutputPointer : OutputPointer
    {
        public WarpProfile WarpProfile;

        private void Awake()
        {
            ValueTypeIndex = (int)ValueType.NoiseWarpProfile;
        }

        protected override void ResetPointer()
        {
            WarpProfile = new();
        }

        protected override Color GetLineColor()
        {
            return ProjectData.Colors.NoiseWarpProfile;
        }
    }
}
