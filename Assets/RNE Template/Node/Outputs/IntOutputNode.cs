using RNE.Template.Node.Pointer.Value;

namespace RNE.Template.Node
{
    public class IntOutputNode : RuntimeNodeEditor.Node.Node
    {
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);
            Elements.SetInputField(Elements.inputFields[0], PointerValue.GetInt(Inputs[0]).ToString());
        }
    }
}
