using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using UnityEngine;

namespace RNE.Template.Node
{
    public class NOTNode : RuntimeNodeEditor.Node.Node
    {
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);
        }

        protected override void DataToGetAndSet()
        {
            bool a = PointerValue.GetBool(Inputs[0]);
            Outputs[0].GetComponent<BoolOutputPointer>().Value = !a;

            Elements.SetBoolean(Elements.buttons[0], a);
            Elements.SetBoolean(Elements.buttons[1], !a);
        }

        protected override void CodeToReset()
        {
            Debug.Log("nay");
            Outputs[0].GetComponent<BoolOutputPointer>().Reset();
        }
    }
}
