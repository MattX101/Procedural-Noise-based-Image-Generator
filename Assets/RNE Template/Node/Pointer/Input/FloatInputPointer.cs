using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node.Pointer;
using RuntimeNodeEditor.UI.Canvas.Node.UI;
using UnityEngine;

namespace RNE.Template.Node.Pointer
{
    public class FloatInputPointer : InputPointer
    {
        [SerializeField]
        private UIInputField _inputfield;

        private void Awake()
        {
            ValueTypeIndex = (int)ValueType.Float;
        }

        public override void DisableUIElement()
        {
            _inputfield.Disable();
        }

        public override void EnableUIElement()
        {
            _inputfield.Enable();
        }
    }
}