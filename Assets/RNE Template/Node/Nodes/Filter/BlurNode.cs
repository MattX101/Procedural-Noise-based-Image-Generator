using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using UnityEngine;

namespace RNE.Template.Node
{
    public class BlurNode : NodeWithPreview
    {
        [SerializeField]
        private ComputeShader _shader;

        private ComputeBuffer _inputBuffer, _outputBuffer;
        private int _kernel;

        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            _inputBuffer = PointerValue.GetTexture(Inputs[0]);
            if (_inputBuffer == null)
            {
                return;
            }

            _outputBuffer = _inputBuffer;

            _kernel = _shader.FindKernel("BlurColor");
            _shader.SetInt("width", ProjectData.Resolution);
            _shader.SetInt("height", ProjectData.Resolution);
            _shader.SetBuffer(_kernel, "sourceColor", _inputBuffer);
            _shader.SetBuffer(_kernel, "filterColor", _outputBuffer);
            _shader.Dispatch(
                _kernel,
                Mathf.CeilToInt(ProjectData.Resolution / 32.0f),
                Mathf.CeilToInt(ProjectData.Resolution / 32.0f),
                1);

            SetPreview(_outputBuffer);

            Outputs[0].GetComponent<TextureOutputPointer>().Buffer = _outputBuffer;
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<TextureOutputPointer>().Reset();
        }

        private void OnDestroy()
        {
            _inputBuffer.Release();
            _outputBuffer.Release();
        }
    }
}
