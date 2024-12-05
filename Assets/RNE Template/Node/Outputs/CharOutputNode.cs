using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node;

namespace RNE.Template.Node
{
    public class CharOutputNode : EndNode
    {
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);
        }

        protected override void DataToGetAndSet()
        {
            Elements.SetInputField(Elements.inputFields[0], PointerValue.GetChar(Inputs[0]).ToString());
        }
    }
}
