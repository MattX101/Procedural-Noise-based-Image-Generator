using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node;

namespace RNE.Template.Node
{
    public class BoolOutputNode : EndNode
    {
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);
            Elements.SetBoolean(Elements.buttons[0], PointerValue.GetBool(Inputs[0]));
        }
    }
}
