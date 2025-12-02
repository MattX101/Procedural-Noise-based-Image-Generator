using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.Colors.Coloring;
using UnityEngine;

namespace RNE.Template.Node
{
    public class GradientColoringNode : NodeWithPreview
    {
        private ComputeBuffer _colorBuffer;

        protected override void Init()
        {
            _colorBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float) * 4);
        }

        protected override void CodeToExecute()
        {
            if ((Inputs[0].ConnectedOutputPointer && Inputs[1].ConnectedOutputPointer) == false)
            {
                return;
            }

            if (_colorBuffer.count != ProjectData.Length)
            {
                Init();
            }

            ExecuteInputConnection(0);

            if (Inputs[0].ConnectedOutputPointer && !Inputs[1].ConnectedOutputPointer)
            {
                _colorBuffer = PointerValue.GetNoise(Inputs[0]);
            }
            else
            {
                ExecuteInputConnection(1);

                Coloring.GradientColoringGPU(
                    ref _colorBuffer,
                    PointerValue.GetNoise(Inputs[0]),
                    PointerValue.GetColorGradient(Inputs[1])
                );
            }

            SetPreview(_colorBuffer);
            Outputs[0].GetComponent<TextureOutputPointer>().Buffer = _colorBuffer;
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<TextureOutputPointer>().Reset();
        }

        void OnDestroy()
        {
            _colorBuffer.Release();
        }
    }
}
