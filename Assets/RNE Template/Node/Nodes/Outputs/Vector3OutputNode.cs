using RNE.Template.Node.Pointer.Value;
using UnityEngine;
using TMPro;

namespace RNE.Template.Node
{
    public class Vector3OutputNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField] private TMP_InputField _inputfieldX;
        [SerializeField] private TMP_InputField _inputfieldY;
        [SerializeField] private TMP_InputField _inputfieldZ;

        private Vector3 _value;
        
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            ExecuteInputConnection(1);
            ExecuteInputConnection(2);
            ExecuteInputConnection(3);
            
            _value = PointerValue.GetVector3(Inputs[0]);

            PointerValue.GetFloat(Inputs[1], ref _value.x);
            PointerValue.GetFloat(Inputs[2], ref _value.y);
            PointerValue.GetFloat(Inputs[3], ref _value.z);

            _inputfieldX.text = _value.x.ToString();
            _inputfieldY.text = _value.y.ToString();
            _inputfieldZ.text = _value.z.ToString();
        }
    }
}
