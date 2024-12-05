using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node.Pointer;
using UnityEngine;

namespace RNE.Template.Node.Pointer
{
    public class Vector2OutputPointer : OutputPointer
    {
        public Vector2 Value = Vector2.zero;

        private void Awake()
        {
            ValueTypeIndex = (int)ValueType.Vector2;
        }

        protected override void ResetPointer()
        {
            Value = Vector2.zero;
        }

        protected override Color GetLineColor()
        {
            return Data.Colors.Vector2;
        }
    }
}