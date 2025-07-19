using RuntimeNodeEditor.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using UnityEngine;

namespace RNE.Template.Node.Pointer.Assets.RNE_Template.Node.Pointer.Output
{
    public class CellularProfileOutputPointer : OutputPointer
    {
        private void Awake()
        {
            ValueTypeIndex = (int)ValueType.CellularProfile;
        }

        protected override Color GetLineColor()
        {
            return ProjectData.Colors.CellularProfile;
        }
    }
}
