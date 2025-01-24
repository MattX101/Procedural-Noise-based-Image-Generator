using RNE.Template.Node.Pointer;

namespace RNE.Template.Node
{
    public class CharInputNode : RuntimeNodeEditor.Node.Node
    {
        protected override void CodeToExecute()
        {
            Outputs[0].GetComponent<CharOutputPointer>().Value =
                Elements.inputFields[0].text.Length != 0 ?
                Elements.inputFields[0].text[0] : 
                ' ';
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<CharOutputPointer>().Reset();
        }
    }
}
