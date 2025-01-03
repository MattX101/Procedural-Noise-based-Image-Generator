using UnityEngine.UI;
using UnityEngine;
using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;

namespace RNE.Template.Node
{
    public class InvertNode : RuntimeNodeEditor.Node.Node
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
                    colors[i].r = 1.0f - colors[i].r;
                    colors[i].g = 1.0f - colors[i].g;
                    colors[i].b = 1.0f - colors[i].b;
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
