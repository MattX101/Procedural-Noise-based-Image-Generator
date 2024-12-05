using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node.Pointer;
using UnityEngine;

namespace RNE.Template.Node.Pointer
{
    public class StringOutputPointer : OutputPointer
    {
        public string Value = "";

        private void Awake()
        {
            ValueTypeIndex = (int)ValueType.String;
        }

        protected override void ResetPointer()
        {
            Value = "";
        }

        protected override Color GetLineColor()
        {
            return Data.Colors.String;
        }
    }
}