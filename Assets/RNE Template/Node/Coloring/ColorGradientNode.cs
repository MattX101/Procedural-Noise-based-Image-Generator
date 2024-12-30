using RNE.Template.Node.Pointer;
using RNE.Template.UI;
using UnityEngine;

namespace RNE.Template.Node
{
    public class ColorGradientNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField]
        private UIColourGradient _uiColourGradient;

        protected override void CodeToExecute()
        {
            Outputs[0].GetComponent<ColorGradientOutputPointer>().Value = _uiColourGradient.Gradient;
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<ColorGradientOutputPointer>().Reset();
        }
    }
}
