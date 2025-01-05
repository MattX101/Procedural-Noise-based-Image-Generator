using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using UnityEngine;

namespace RNE.Template.Node
{
    public class PosterizeNode : NodeWithPreview
    {
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            Color[] colors = PointerValue.GetColorArray(Inputs[0]);
            if (colors != null)
            {
                float step = 1.0f / (Elements.sliders[0].value - 1);

                for (int i = 0; i < colors.Length; i++)
                {
                    colors[i] = new Color(
                        Mathf.Round(colors[i].r / step) * step,
                        Mathf.Round(colors[i].g / step) * step,
                        Mathf.Round(colors[i].b / step) * step, 
                        1);
                }

                Outputs[0].GetComponent<ColorArrayOutputPointer>().Values = colors;
                SetPreview(PreviewTexture.Generate(colors));
            }
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<ColorArrayOutputPointer>().Reset();
        }
    }
}
