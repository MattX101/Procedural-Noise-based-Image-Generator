using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.Curves;
using UnityEngine;
using UnityEngine.UI;

namespace RNE.Template.Node
{
    public class BiasAndGainNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField]
        private RawImage _image;

        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            Color[] colors = PointerValue.GetColorArray(Inputs[0]);
            if (colors != null)
            {
                for (int i = 0; i < colors.Length; i++)
                {
                    colors[i].r = Gain.Calculate(Bias.Calculate(colors[i].r, Elements.sliders[0].value), Elements.sliders[1].value);
                    colors[i].g = Gain.Calculate(Bias.Calculate(colors[i].g, Elements.sliders[0].value), Elements.sliders[1].value);
                    colors[i].b = Gain.Calculate(Bias.Calculate(colors[i].b, Elements.sliders[0].value), Elements.sliders[1].value);
                    colors[i].a = 1;
                }

                Outputs[0].GetComponent<ColorArrayOutputPointer>().Values = colors;
                _image.texture = PreviewTexture.Generate(colors);
            }
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<ColorArrayOutputPointer>().Reset();
        }
    }
}
