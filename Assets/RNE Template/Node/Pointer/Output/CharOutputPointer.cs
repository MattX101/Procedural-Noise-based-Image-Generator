using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node.Pointer;
using UnityEngine;

namespace RNE.Template.Node.Pointer
{
    public class CharOutputPointer : OutputPointer
    {
        public char Value = ' ';

        private void Awake()
        {
            ValueTypeIndex = (int)ValueType.Char;
        }

        protected override void ResetPointer()
        {
            Value = ' ';
        }

        protected override Color GetLineColor()
        {
            return ProjectData.Colors.Char;
        }
    }
}