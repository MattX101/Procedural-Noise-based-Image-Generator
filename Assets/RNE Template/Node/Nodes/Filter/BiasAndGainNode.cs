using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.Curves;
using UnityEngine;

namespace RNE.Template.Node
{
    public class BiasAndGainNode : NodeWithPreview
    {
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            ComputeBuffer buffer = PointerValue.GetTexture(Inputs[0]);

            if (buffer != null)
            {
                BiasAndGainGPU.ModifyImage(ref buffer, Elements.sliders[0].value, Elements.sliders[1].value);

                Color[] colors = new Color[buffer.count];
                buffer.GetData(colors);
                SetPreview(buffer);

                Outputs[0].GetComponent<TextureOutputPointer>().Buffer = buffer;
            }
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<TextureOutputPointer>().Reset();
        }
    }
}
