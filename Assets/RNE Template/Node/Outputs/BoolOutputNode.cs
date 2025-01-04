using RNE.Template.Node.Pointer.Value;

namespace RNE.Template.Node
{
    public class BoolOutputNode : RuntimeNodeEditor.Node.Node
    {
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);
            Elements.SetBoolean(Elements.buttons[0], PointerValue.GetBool(Inputs[0]));
        }
    }
}
