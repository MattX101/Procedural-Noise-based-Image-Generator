using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node.Pointer;
using RuntimeNodeEditor.UI.Canvas.Node.UI;
using UnityEngine;

namespace RNE.Template.Node.Pointer
{
    public class BoolInputPointer : InputPointer
    {
        [SerializeField]
        private UIBooleanButton _toggle;

        private void Awake()
        {
            ValueTypeIndex = (int)ValueType.Bool;
        }

        public override void DisableUIElement()
        {
            _toggle.Disable();
        }

        public override void EnableUIElement()
        {
            _toggle.Enable();
        }
    }
}