using RNE.Template.Node.Pointer.Value;

namespace RNE.Template.Node
{
    public class CellularProfileNode : RuntimeNodeEditor.Node.Node
    {
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);
            if (Inputs[0].ConnectedOutputPointer)
            {
                Elements.SetInputField(Elements.inputFields[0], PointerValue.GetFloat(Inputs[0]).ToString());
            }
        }
    }
}
