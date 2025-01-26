using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node.Pointer;
using UnityEngine;

namespace RNE.Template.Node.Pointer
{
    public class Vector3OutputPointer : OutputPointer
    {
        public Vector3 Value = Vector3.zero;

        private void Awake()
        {
            ValueTypeIndex = (int)ValueType.Vector3;
        }

        protected override void ResetPointer()
        {
            Value = Vector3.zero;
        }

        protected override Color GetLineColor()
        {
            return ProjectData.Colors.Vector3;
        }
    }
}