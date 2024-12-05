using RNE.Template.Node.Pointer;

namespace RNE.Template.Node
{
    public class BoolInputNode : RuntimeNodeEditor.Node.Node
    {
        protected override void CodeToExecute()
        {
            Outputs[0].GetComponent<BoolOutputPointer>().Value = Elements.buttons[0].isOn;
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<BoolOutputPointer>().Reset();
        }
    }
}
