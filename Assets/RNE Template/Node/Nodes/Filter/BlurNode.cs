using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using UnityEngine;

namespace RNE.Template.Node
{
    public class BlurNode : NodeWithPreview
    {
        [SerializeField]
        private ComputeShader _shader;

        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            ComputeBuffer buffer = PointerValue.GetTexture(Inputs[0]);

            if (buffer != null)
            {
                _shader.SetInt("resX", PreviewTexture.Resolution);
                _shader.SetInt("resY", PreviewTexture.Resolution);

                int kernel = _shader.FindKernel("Blur");
                _shader.SetBuffer(kernel, "colors", buffer);
                _shader.Dispatch(
                    kernel, 
                    Mathf.CeilToInt(PreviewTexture.Resolution / 32.0f), 
                    Mathf.CeilToInt(PreviewTexture.Resolution / 32.0f), 
                    1);

                Color[] colors = new Color[buffer.count];
                buffer.GetData(colors);
                SetPreview(PreviewTexture.Generate(colors));

                Outputs[0].GetComponent<TextureOutputPointer>().Buffer = buffer;
            }
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<TextureOutputPointer>().Reset();
        }
    }
}
