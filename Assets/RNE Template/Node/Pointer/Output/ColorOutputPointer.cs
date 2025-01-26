using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node.Pointer;
using UnityEngine;

namespace RNE.Template.Node.Pointer
{
    public class ColorOutputPointer : OutputPointer
    {
        public Color Value = Color.black;

        private void Awake()
        {
            ValueTypeIndex = (int)ValueType.Color;
        }

        protected override void ResetPointer()
        {
            Value = Color.black;
        }

        protected override Color GetLineColor()
        {
            return ProjectData.Colors.Color;
        }
    }
}