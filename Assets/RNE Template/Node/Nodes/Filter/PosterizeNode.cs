using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using UnityEngine;

namespace RNE.Template.Node
{
    public class PosterizeNode : NodeWithPreview
    {
        [SerializeField]
        private ComputeShader _shader;

        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            ComputeBuffer buffer = PointerValue.GetTexture(Inputs[0]);

            if (buffer != null)
            {
                _shader.SetFloats("step", 1.0f / (Elements.sliders[0].value - 1));

                int kernel = _shader.FindKernel("Posterize");
                _shader.SetBuffer(kernel, "colors", buffer);
                _shader.Dispatch(kernel, Mathf.CeilToInt(buffer.count / 1024.0f), 1, 1);

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
