using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node.Pointer;
using UnityEngine;

namespace RNE.Template.Node.Pointer
{
    public class IntOutputPointer : OutputPointer
    {
        public int Value = 0;

        private void Awake()
        {
            ValueTypeIndex = (int)ValueType.Int;
        }

        protected override void ResetPointer()
        {
            Value = 0;
        }

        protected override Color GetLineColor()
        {
            return ProjectData.Colors.Int;
        }
    }
}