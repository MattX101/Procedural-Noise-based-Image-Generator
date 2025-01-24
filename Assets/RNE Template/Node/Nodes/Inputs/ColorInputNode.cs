using RuntimeNodeEditor.UI.Canvas.Node.UI;
using RNE.Template.Node.Pointer;
using UnityEngine;

namespace RNE.Template.Node
{
    public class ColorInputNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField]
        private UIColourPicker _colourPicker;

        protected override void CodeToExecute()
        {
            Color color = _colourPicker.CalcualteColor();

            Outputs[0].GetComponent<FloatOutputPointer>().Value = (int)(color.r * 255);
            Outputs[1].GetComponent<FloatOutputPointer>().Value = (int)(color.g * 255);
            Outputs[2].GetComponent<FloatOutputPointer>().Value = (int)(color.b * 255);

            Outputs[3].GetComponent<ColorOutputPointer>().Value =
                new Color(
                    color.r,
                    color.g,
                    color.b);
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<FloatOutputPointer>().Reset();
            Outputs[1].GetComponent<FloatOutputPointer>().Reset();
            Outputs[2].GetComponent<FloatOutputPointer>().Reset();
            Outputs[3].GetComponent<ColorOutputPointer>().Reset();
        }
    }
}
