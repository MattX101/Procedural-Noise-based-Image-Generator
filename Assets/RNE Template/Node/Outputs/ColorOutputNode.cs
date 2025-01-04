using RNE.Template.Node.Pointer.Value;
using UnityEngine;
using UnityEngine.UI;

namespace RNE.Template.Node
{
    public class ColorOutputNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField] private RawImage _image;

        protected override void CodeToExecute()
        {
             ExecuteInputConnection(0);
            _image.color = PointerValue.GetColor(Inputs[0]);
        }
    }
}
