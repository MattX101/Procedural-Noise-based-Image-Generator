using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using UnityEngine;

namespace RNE.Template.Node
{
    public class InvertNode : NodeWithPreview
    {
        [SerializeField]
        private ComputeShader _shader;

        private ComputeBuffer _textureBuffer;
        private int _kernel;

        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            _textureBuffer = PointerValue.GetTexture(Inputs[0]);
            if (_textureBuffer == null)
            {
                return;
            }

            _kernel = _shader.FindKernel("InvertColor");
            _shader.SetBuffer(_kernel, "sourceColor", _textureBuffer);
            _shader.Dispatch(_kernel, Mathf.CeilToInt(_textureBuffer.count / 1024.0f), 1, 1);

            SetPreview(_textureBuffer);

            Outputs[0].GetComponent<TextureOutputPointer>().Buffer = _textureBuffer;
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<TextureOutputPointer>().Reset();
        }

        private void OnDestroy()
        {
            _textureBuffer.Release();
        }
    }
}
