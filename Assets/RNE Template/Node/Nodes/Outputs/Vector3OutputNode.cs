using RNE.Template.Node.Pointer.Value;
using TMPro;
using UnityEngine;

namespace RNE.Template.Node
{
    public class Vector3OutputNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField] private TMP_InputField _inputfieldX;
        [SerializeField] private TMP_InputField _inputfieldY;
        [SerializeField] private TMP_InputField _inputfieldZ;
        
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            ExecuteInputConnection(1);
            ExecuteInputConnection(2);
            ExecuteInputConnection(3);
            
            Vector3 v = PointerValue.GetVector3(Inputs[0]);

            PointerValue.GetFloat(Inputs[1], ref v.x);
            PointerValue.GetFloat(Inputs[2], ref v.y);
            PointerValue.GetFloat(Inputs[3], ref v.z);

            _inputfieldX.text = v.x.ToString();
            _inputfieldY.text = v.y.ToString();
            _inputfieldZ.text = v.z.ToString();
        }
    }
}
