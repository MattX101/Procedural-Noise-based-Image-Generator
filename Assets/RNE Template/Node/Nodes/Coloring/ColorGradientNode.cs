using RNE.Template.Node.Pointer;
using RNE.Template.UI;
using UnityEngine;

namespace RNE.Template.Node
{
    public class ColorGradientNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField]
        private UIColorGradient _uiColorGradient;

        protected override void CodeToExecute()
        {
            Outputs[0].GetComponent<ColorGradientOutputPointer>().Value = _uiColorGradient.Gradient;
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<ColorGradientOutputPointer>().Reset();
        }
    }
}
