using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node;

namespace RNE.Template.Node
{
    public class IntOutputNode : EndNode
    {
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);
            Elements.SetInputField(Elements.inputFields[0], PointerValue.GetInt(Inputs[0]).ToString());
        }
    }
}
