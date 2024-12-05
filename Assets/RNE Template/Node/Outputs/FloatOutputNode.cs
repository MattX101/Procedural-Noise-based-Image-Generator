using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node;

namespace RNE.Template.Node
{
    public class FloatOutputNode : EndNode
    {
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);
        }

        protected override void DataToGetAndSet()
        {
            Elements.SetInputField(Elements.inputFields[0], PointerValue.GetFloat(Inputs[0]).ToString());
        }
    }
}
