using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node;
using UnityEngine;

namespace RNE.Template.Node
{
    public class Vector3OutputNode : EndNode
    {
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

            Elements.SetInputField(Elements.inputFields[0], v.x.ToString());
            Elements.SetInputField(Elements.inputFields[1], v.y.ToString());
            Elements.SetInputField(Elements.inputFields[2], v.z.ToString());
        }
    }
}
