using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using UnityEngine;
using Utils.Colors.Coloring;

namespace RNE.Template.Node
{
    public class GradientColoringNode : NodeWithPreview
    {
        protected override void CodeToExecute()
        {
            ComputeBuffer buffer = new ComputeBuffer(PreviewTexture.Length, sizeof(float) * 4);
            Color[] preview = new Color[PreviewTexture.Length];

            if (!Inputs[0].ConnectedOutputPointer && !Inputs[1].ConnectedOutputPointer)
            {
                for (int i = 0; i < preview.Length; i++)
                {
                    preview[i] = Color.black;
                }
            }
            else if (!Inputs[0].ConnectedOutputPointer && Inputs[1].ConnectedOutputPointer)
            {
                ExecuteInputConnection(1);
                for (int i = 0; i < preview.Length; i++)
                {
                    preview[i] = Color.black;
                }
            }
            else
            {
                if (Inputs[0].ConnectedOutputPointer && !Inputs[1].ConnectedOutputPointer)
                {
                    ExecuteInputConnection(0);
                    buffer = PointerValue.GetNoise(Inputs[0]);
                }
                else
                {
                    ExecuteInputConnection(0);
                    ExecuteInputConnection(1);

                    Coloring.GradientColoringGPU(
                        ref buffer,
                        PointerValue.GetNoise(Inputs[0]),
                        PointerValue.GetColorGradient(Inputs[1])
                    );
                }

                buffer.GetData(preview);
            }

            SetPreview(PreviewTexture.Generate(preview));

            Outputs[0].GetComponent<TextureOutputPointer>().Buffer = buffer;
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<TextureOutputPointer>().Reset();
        }
    }
}
