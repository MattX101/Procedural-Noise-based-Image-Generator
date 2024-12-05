using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node.Pointer;
using RuntimeNodeEditor.UI.Canvas.Node.UI;
using UnityEngine;

namespace RNE.Template.Node.Pointer
{
    public class IntInputPointer : InputPointer
    {
        [SerializeField]
        private UIInputField _inputfield;

        private void Awake()
        {
            ValueTypeIndex = (int)ValueType.Int;
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