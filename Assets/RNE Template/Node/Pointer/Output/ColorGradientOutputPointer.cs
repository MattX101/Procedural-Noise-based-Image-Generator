using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node.Pointer;
using UnityEngine;

namespace RNE.Template.Node.Pointer
{
    public class ColorGradienyOutputPointer : OutputPointer
    {
        public Gradient Value;

        private void Awake()
        {
            ValueTypeIndex = (int)ValueType.ColorGradient;
        }

        protected override void ResetPointer()
        {
            Value = null;
        }

        protected override Color GetLineColor()
        {
            return Data.Colors.ColorGradient;
        }
    }
}