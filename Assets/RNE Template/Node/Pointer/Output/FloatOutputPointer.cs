using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node.Pointer;
using UnityEngine;

namespace RNE.Template.Node.Pointer
{
    public class FloatOutputPointer : OutputPointer
    {
        public float Value = 0.0f;

        private void Awake()
        {
            ValueTypeIndex = (int)ValueType.Float;
        }

        protected override void ResetPointer()
        {
            Value = 0.0f;
        }

        protected override Color GetLineColor()
        {
            return ProjectData.Colors.Float;
        }
    }
}