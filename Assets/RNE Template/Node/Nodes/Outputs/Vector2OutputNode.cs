using RNE.Template.Node.Pointer.Value;
using TMPro;
using UnityEngine;

namespace RNE.Template.Node
{
    public class Vector2OutputNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField] private TMP_InputField _inputfieldX;
        [SerializeField] private TMP_InputField _inputfieldY;
        
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            ExecuteInputConnection(1);
            ExecuteInputConnection(2);

            Vector2 v = PointerValue.GetVector2(Inputs[0]);

            PointerValue.GetFloat(Inputs[1], ref v.x);
            PointerValue.GetFloat(Inputs[2], ref v.y);

            _inputfieldX.text = v.x.ToString();
            _inputfieldY.text = v.y.ToString();
        }
    }
}
