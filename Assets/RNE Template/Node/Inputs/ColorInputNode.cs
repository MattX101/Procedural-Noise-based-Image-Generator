using RNE.Template.Node.Pointer;
using UnityEngine;

namespace RNE.Template.Node
{
    public class ColorInputNode : RuntimeNodeEditor.Node.Node
    {
        protected override void CodeToExecute()
        {
            Outputs[0].GetComponent<FloatOutputPointer>().Value = Elements.sliders[0].value;
            Outputs[1].GetComponent<FloatOutputPointer>().Value = Elements.sliders[1].value;
            Outputs[2].GetComponent<FloatOutputPointer>().Value = Elements.sliders[2].value;

            Outputs[3].GetComponent<ColorOutputPointer>().Value =
                new Color(
                    Elements.sliders[0].value,
                    Elements.sliders[1].value,
                    Elements.sliders[2].value);
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
