using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.Colors.Coloring;
using UnityEngine;

namespace RNE.Template.Node
{
    public class ColoringNode : NodeWithPreview
    {
        protected override void CodeToExecute()
        {
            ComputeBuffer colorsBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float) * 4);
            Color[] preview = new Color[ProjectData.Length];

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
                    Coloring.ColoringGPU(ref colorsBuffer, PointerValue.GetNoise(Inputs[0]), Color.white);
                }
                else
                {
                    ExecuteInputConnection(0);
                    ExecuteInputConnection(1);

                    Coloring.ColoringGPU(
                        ref colorsBuffer,
                        PointerValue.GetNoise(Inputs[0]), 
                        PointerValue.GetColor(Inputs[1])
                    );
                }

                colorsBuffer.GetData(preview);
            }

            SetPreview(colorsBuffer);

            Outputs[0].GetComponent<TextureOutputPointer>().Buffer = colorsBuffer;
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<TextureOutputPointer>().Reset();
        }
    }
}
