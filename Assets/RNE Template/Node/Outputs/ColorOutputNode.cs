using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node;
using UnityEngine;
using UnityEngine.UI;

namespace RNE.Template.Node
{
    public class ColorOutputNode : EndNode
    {
        [SerializeField] private RawImage _image;

        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);
        }

        protected override void DataToGetAndSet()
        {
            _image.color = PointerValue.GetColor(Inputs[0]);
        }
    }
}
