using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using UnityEngine;

namespace RNE.Template.Node
{
    public class BlurNode : NodeWithPreview
    {
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            Color[] colors = PointerValue.GetColorArray(Inputs[0]);
            if (colors != null)
            {
                Color[] blur = new Color[colors.Length];
                for (int y = 0, i = 0; y < PreviewTexture.Resolution; y++)
                {
                    for (int x = 0; x < PreviewTexture.Resolution; x++, i++)
                    {
                        int c = i;

                        int l = x - 1;
                        int r = x + 1;
                        int b = y - 1;
                        int t = y + 1;

                        l = l < 0 ? 1 : l;
                        r = r >= PreviewTexture.Resolution ? PreviewTexture.Resolution - 1 : r;
                        b = b < 0 ? 1 : b;
                        t = t >= PreviewTexture.Resolution ? PreviewTexture.Resolution - 1 : t;

                        int lI = y * PreviewTexture.Resolution + l;
                        int rI = y * PreviewTexture.Resolution + l;
                        int bI = b * PreviewTexture.Resolution + x;
                        int tI = t * PreviewTexture.Resolution + x;
                        int bLI = b * PreviewTexture.Resolution + l;
                        int bRI = b * PreviewTexture.Resolution + r;
                        int tLI = t * PreviewTexture.Resolution + l;
                        int tRI = t * PreviewTexture.Resolution + r;

                        blur[i] = new Color(
                            (colors[c].r + colors[lI].r + colors[rI].r + colors[bI].r + colors[tI].r + colors[bLI].r + colors[bRI].r + colors[tLI].r + colors[tRI].r) / 9.0f,
                            (colors[c].g + colors[lI].g + colors[rI].g + colors[bI].g + colors[tI].g + colors[bLI].g + colors[bRI].g + colors[tLI].g + colors[tRI].g) / 9.0f,
                            (colors[c].b + colors[lI].b + colors[rI].b + colors[bI].b + colors[tI].b + colors[bLI].b + colors[bRI].b + colors[tLI].b + colors[tRI].b) / 9.0f, 
                            1);
                    }
                }

                Outputs[0].GetComponent<ColorArrayOutputPointer>().Values = blur;
                SetPreview(PreviewTexture.Generate(blur));
            }
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<ColorArrayOutputPointer>().Reset();
        }
    }
}
