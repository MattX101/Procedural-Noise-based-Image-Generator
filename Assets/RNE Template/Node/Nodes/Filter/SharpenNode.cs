using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using UnityEngine;

namespace RNE.Template.Node
{
    public class SharpenNode : NodeWithPreview
    {
        [SerializeField]
        private ComputeShader _shader;

        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            ComputeBuffer buffer = PointerValue.GetTexture(Inputs[0]);

            if (buffer != null)
            {
                _shader.SetInt("resX", ProjectData.Resolution);
                _shader.SetInt("resY", ProjectData.Resolution);

                int kernel = _shader.FindKernel("Sharpen");
                _shader.SetBuffer(kernel, "colors", buffer);
                _shader.Dispatch(
                    kernel,
                    Mathf.CeilToInt(ProjectData.Resolution / 32.0f),
                    Mathf.CeilToInt(ProjectData.Resolution / 32.0f),
                    1);

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
