using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node.Pointer;
using UnityEngine;

namespace RNE.Template.Node.Pointer
{
    public class BoolOutputPointer : OutputPointer
    {
        public bool Value = false;

        private void Awake()
        {
            ValueTypeIndex = (int)ValueType.Bool;
        }

        protected override void ResetPointer()
        {
            Value = false;
        }

        protected override Color GetLineColor()
        {
            return ProjectData.Colors.Bool;
        }
    }
}