using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.Colors.Coloring;
using UnityEngine;

namespace RNE.Template.Node
{
    public class ColoringNode : NodeWithPreview
    {
        private ComputeBuffer _colorBuffer;

        protected override void Init()
        {
            _colorBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float) * 4);
        }

        protected override void CodeToExecute()
        {
            if (Inputs[0].ConnectedOutputPointer == null)
            {
                return;
            }
            
            Init();

            ExecuteInputConnection(0);

            if (!Inputs[1].ConnectedOutputPointer)
            {
                Coloring.Color(ref _colorBuffer, PointerValue.GetNoise(Inputs[0]), Color.white);
            }
            else
            {
                ExecuteInputConnection(1);

                Coloring.Color(
                    ref _colorBuffer,
                    PointerValue.GetNoise(Inputs[0]),
                    PointerValue.GetColor(Inputs[1])
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
