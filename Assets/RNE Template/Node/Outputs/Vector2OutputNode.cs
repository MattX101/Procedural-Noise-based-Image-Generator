using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node;
using UnityEngine;

namespace RNE.Template.Node
{
    public class Vector2OutputNode : EndNode
    {
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            ExecuteInputConnection(1);
            ExecuteInputConnection(2);
        }

        protected override void DataToGetAndSet()
        {
            Vector2 v = Vector2.zero;

            v = PointerValue.GetVector2(Inputs[0]);

            PointerValue.GetFloat(Inputs[1], ref v.x);
            PointerValue.GetFloat(Inputs[2], ref v.y);

            Elements.SetInputField(Elements.inputFields[0], v.x.ToString());
            Elements.SetInputField(Elements.inputFields[1], v.y.ToString());
        }
    }
}
