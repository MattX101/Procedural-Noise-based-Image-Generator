using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using UnityEngine;

namespace RNE.Template.Node
{
    public class InvertNode : NodeWithPreview
    {
        [SerializeField]
        private ComputeShader _shader;

        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            ComputeBuffer buffer = PointerValue.GetTexture(Inputs[0]);

            if (buffer != null)
            {
                int kernel = _shader.FindKernel("Invert");
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
