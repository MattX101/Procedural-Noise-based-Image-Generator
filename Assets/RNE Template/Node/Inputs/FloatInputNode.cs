using RNE.Template.Node.Pointer;
using RuntimeNodeEditor.Node.UI.Functions;

namespace RNE.Template.Node
{
    public class FloatInputNode : RuntimeNodeEditor.Node.Node
    {
        protected override void CodeToExecute()
        {
            Outputs[0].GetComponent<FloatOutputPointer>().Value =
                Elements.inputFields[0].text.Length != 0
                ? InputFieldToFloat.Get(Elements.inputFields[0].text)
                : 0.0f;
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<FloatOutputPointer>().Reset();
        }
    }
}
