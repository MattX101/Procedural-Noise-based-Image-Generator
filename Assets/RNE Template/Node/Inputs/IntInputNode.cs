using RNE.Template.Node.Pointer;
using RuntimeNodeEditor.Node.UI.Functions;

namespace RNE.Template.Node
{
    public class IntInputNode : RuntimeNodeEditor.Node.Node
    {
        protected override void CodeToExecute()
        {
            Outputs[0].GetComponent<IntOutputPointer>().Value =
                Elements.inputFields[0].text.Length != 0
                ? InputFieldToInt.Get(Elements.inputFields[0].text)
                : 0;
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<IntOutputPointer>().Reset();
        }
    }
}
