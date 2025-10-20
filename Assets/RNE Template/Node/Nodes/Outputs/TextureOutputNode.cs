namespace RNE.Template.Node
{
    public class TextureOutputNode : NodeWithPreview
    {
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);
        }
    } 
}
