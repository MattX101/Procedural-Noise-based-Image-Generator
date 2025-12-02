using RNE.Template.Node.Pointer.Value;
using UnityEngine;
using TMPro;

namespace RNE.Template.Node
{
    public class Vector2OutputNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField] private TMP_InputField _inputfieldX;
        [SerializeField] private TMP_InputField _inputfieldY;

        private Vector2 _value;
        
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            ExecuteInputConnection(1);
            ExecuteInputConnection(2);

            _value = PointerValue.GetVector2(Inputs[0]);

            PointerValue.GetFloat(Inputs[1], ref _value.x);
            PointerValue.GetFloat(Inputs[2], ref _value.y);

            _inputfieldX.text = _value.x.ToString();
            _inputfieldY.text = _value.y.ToString();
        }
    }
}
